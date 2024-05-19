using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] int boostDestroyPoints = 1;
    [SerializeField] int selfDestructDestroyPoints = 1;
    [SerializeField] int maxStreak = 1;
    [SerializeField] float streakTime = 1f;
    int score;
    int streakMultiplier = 1;
    float timer;
    bool streakStarted;
    public delegate void BoostDestroyDelegate();
    public static BoostDestroyDelegate boostDestroy;
    public delegate void SelfDestructDestroyDelegate();
    public static SelfDestructDestroyDelegate selfDestructDestroy;

    private void Awake()
    {
        boostDestroy += BoostDestroy;
        selfDestructDestroy += SelfDestructDestroy;
    }

    private void Update()
    {
        if (streakStarted)
        {
            timer += Time.deltaTime;
        }

        if (timer >= streakTime)
        {
            ResetStreak();
        }
    }

    void BoostDestroy()
    {
        streakStarted = true;
        timer = 0f;
        score += boostDestroyPoints * streakMultiplier;

        if (streakMultiplier < maxStreak)
        {
            streakMultiplier++;
        }

        ScoreUI.updateScore?.Invoke();
    }

    void SelfDestructDestroy()
    {
        streakStarted = true;
        timer = 0f;
        score += selfDestructDestroyPoints * streakMultiplier;

        if (streakMultiplier < maxStreak)
        {
            streakMultiplier++;
        }

        ScoreUI.updateScore?.Invoke();
    }

    void ResetStreak()
    {
        streakMultiplier = 1;
        timer = 0f;
        streakStarted = false;

        ScoreUI.updateScore?.Invoke();
    }

    private void OnDestroy()
    {
        boostDestroy -= BoostDestroy;
        selfDestructDestroy -= SelfDestructDestroy;
    }

    public int GetScore() => score;
    public int GetStreakMultiplier() => streakMultiplier;
}
