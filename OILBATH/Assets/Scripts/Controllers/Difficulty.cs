using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    [SerializeField] float timeBetweenIncreases = 10f;
    [SerializeField] float difficultyIncrease = 0.25f;
    [SerializeField] float maxDifficulty = 5f;
    public static float difficulty;

    private void Awake()
    {
        difficulty = 0f;

        StartCoroutine(IncreaseDifficulty());
    }

    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(timeBetweenIncreases);

        difficulty += difficultyIncrease;

        if (difficulty < maxDifficulty)
        {
            StartCoroutine(IncreaseDifficulty());
        }
        else
        {
            difficulty = maxDifficulty;
        }
    }
}
