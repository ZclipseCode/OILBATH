using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] cars;
    [SerializeField] int totalCars = 10;
    //[SerializeField] int totalObjectives = 35;
    //bool initiated;
    //public static List<Objective> objectives = new List<Objective>();
    [SerializeField] List<Objective> objectives = new List<Objective>();
    public delegate void CarReadyDelegate();
    public static CarReadyDelegate carReady;
    public delegate void AddObjectiveDelegate(Objective objective);
    public static AddObjectiveDelegate addObjective;

    private void Awake()
    {
        carReady += CarReady;
        addObjective += AddObjective;
    }

    private void Start()
    {
        for (int i = 0; i < totalCars; i++)
        {
            CarReady();
        }
    }

    //private void Update()
    //{
    //    if (!initiated && objectives.Count == totalObjectives)
    //    {
    //        for (int i = 0; i < totalCars; i++)
    //        {
    //            CarReady();
    //        }

    //        initiated = true;
    //    }
    //}

    void CarReady()
    {
        int objectiveIndex = Random.Range(0, objectives.Count);
        int carIndex = Random.Range(0, cars.Length);

        Instantiate(cars[carIndex], objectives[objectiveIndex].transform.position, Quaternion.identity);
    }

    void AddObjective(Objective objective)
    {
        objectives.Add(objective);
    }

    private void OnDestroy()
    {
        carReady -= CarReady;
        addObjective -= AddObjective;

        objectives = new List<Objective>();
    }
}
