using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private void OnEnable()
    {
        //CarSpawner.addObjective?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car"))
        {
            CarObjectiveFinder objectiveFinder = collision.gameObject.GetComponentInChildren<CarObjectiveFinder>();

            if (objectiveFinder != null)
            {
                collision.gameObject.GetComponent<CarMovement>().SetObjective(null);
                objectiveFinder.SetSearching(true);
            }
        }
    }
}
