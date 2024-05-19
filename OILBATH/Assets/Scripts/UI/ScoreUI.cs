using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] Score scoreScript;
    public delegate void UpdateScoreDelegate();
    public static UpdateScoreDelegate updateScore;

    private void Awake()
    {
        updateScore += UpdateScore;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = scoreScript.GetScore().ToString();
        multiplierText.text = $"Mult: {scoreScript.GetStreakMultiplier()}";
    }

    private void OnDestroy()
    {
        updateScore -= UpdateScore;
    }
}
