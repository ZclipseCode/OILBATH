using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public delegate void PlayMusicDelegate(AudioClip track, float fadeDuration);
    public static PlayMusicDelegate playMusic;
    public delegate void PlaySFXDelegate(AudioClip sfx);
    public static PlaySFXDelegate playSfx;
    public delegate void StopMusicDelegate(float fadeDuration);
    public static StopMusicDelegate stopMusic;
    public delegate void SetMusicVolumeDelegate(float volume);
    public static SetMusicVolumeDelegate setMusicVolume;
    public delegate void SetSfxVolumeDelegate(float volume);
    public static SetSfxVolumeDelegate setSfxVolume;
    public delegate float GetMusicVolumeDelegate();
    public static GetMusicVolumeDelegate getMusicVolume;
    public delegate float GetSfxVolumeDelegate();
    public static GetSfxVolumeDelegate getSfxVolume;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    float musicVolume;
    float sfxVolume;

    static AudioController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playMusic += PlayMusic;
        playSfx += PlaySfx;
        stopMusic += StopMusic;
        setMusicVolume += SetMusicVolume;
        setSfxVolume += SetSfxVolume;
        getMusicVolume += GetMusicVolume;
        getSfxVolume += GetSfxVolume;

        musicSource.loop = true;
        musicVolume = musicSource.volume;
        sfxVolume = sfxSource.volume;
    }

    public void PlayMusic(AudioClip track, float fadeDuration)
    {
        StartCoroutine(FadeMusic(0, musicVolume, fadeDuration));
        musicSource.clip = track;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip sfx)
    {
        //sfxSource.clip = sfx;
        //sfxSource.Play();



        sfxSource.PlayOneShot(sfx);



        //AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.volume = sfxVolume;
        //audioSource.clip = sfx;
        //audioSource.Play();

        //StartCoroutine(DestroyAfterPlaying(audioSource));
    }

    //IEnumerator DestroyAfterPlaying(AudioSource audioSource)
    //{
    //    yield return new WaitWhile(() => audioSource.isPlaying);

    //    Destroy(audioSource);
    //}

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume;
    }

    void StopMusic(float fadeDuration)
    {
        StartCoroutine(FadeMusic(musicSource.volume, 0, fadeDuration));
    }

    IEnumerator FadeMusic(float initialVolume, float goalVolume, float fadeDuration)
    {
        float currentTime = 0;
        if (fadeDuration == 0)
        {
            fadeDuration = 0.01f;
        }

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(initialVolume, goalVolume, currentTime / fadeDuration);
            musicSource.volume = newVolume;

            yield return null;
        }

        if (musicSource.volume <= 0)
        {
            musicSource.Stop();
        }
    }

    public float GetMusicVolume() => musicVolume;
    public float GetSfxVolume() => sfxVolume;

    private void OnDestroy()
    {
        playMusic -= PlayMusic;
        playSfx -= PlaySfx;
        stopMusic -= StopMusic;
        setMusicVolume -= SetMusicVolume;
        setSfxVolume -= SetSfxVolume;
        getMusicVolume -= GetMusicVolume;
        getSfxVolume -= GetSfxVolume;
    }
}