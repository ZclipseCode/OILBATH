using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSmoke : MonoBehaviour
{
    [SerializeField] GameObject smoke;
    [SerializeField] MotorcycleMovement movement;

    private void Update()
    {
        if (movement.GetIsBoosting())
        {
            smoke.SetActive(true);
        }
        else
        {
            smoke.SetActive(false);
        }
    }
}
