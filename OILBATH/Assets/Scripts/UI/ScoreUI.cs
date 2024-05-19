using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Score scoreScript;

    private void Awake()
    {
        Score.boostDestroy += UpdateScore;
        Score.selfDestructDestroy += UpdateScore;

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = scoreScript.GetScore().ToString();
    }

    private void OnDestroy()
    {
        Score.boostDestroy -= UpdateScore;
        Score.selfDestructDestroy -= UpdateScore;
    }
}
