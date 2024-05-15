using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car"))
        {
            collision.gameObject.GetComponent<CarMovement>().SetObjective(null);
            collision.gameObject.GetComponentInChildren<CarObjectiveFinder>().SetSearching(true);
        }
    }
}
