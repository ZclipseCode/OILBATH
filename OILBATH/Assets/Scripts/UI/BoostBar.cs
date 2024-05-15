using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    [SerializeField] MotorcycleMovement movement;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] Image bar;
    float maxBoost;

    private void Start()
    {
        maxBoost = movement.GetMaxBoost();
    }

    private void Update()
    {
        float currentBoost = movement.GetCurrentBoost();
        UpdateBoostBar(currentBoost);
    }

    void UpdateBoostBar(float currentBoost)
    {
        float boostPercentage = currentBoost / maxBoost;
        int index = Mathf.FloorToInt(boostPercentage * (sprites.Count - 1));
        index = Mathf.Clamp(index, 0, sprites.Count - 1);

        if (index >= 0 && index < sprites.Count)
        {
            bar.sprite = sprites[index];
        }
    }
}
