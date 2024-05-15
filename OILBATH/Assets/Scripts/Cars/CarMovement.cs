using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] CarObjectiveFinder objectiveFinder;
    Transform objective;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (objective != null)
        {
            Move();
        }
    }

    void Move()
    {
        Vector2 distance = objective.position - transform.position;
        Vector2 moveDirection = distance.normalized;
        Vector2 targetVelocity = moveDirection * maxSpeed;
        Vector2 velocityChange = targetVelocity - rb.velocity;

        rb.AddForce(velocityChange * speed, ForceMode2D.Force);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (objective != null && collision.transform == objective.transform && !objectiveFinder.GetSearching())
    //    {
    //        objective = null;
    //        objectiveFinder.SetSearching(true);
    //    }
    //}

    public void SetObjective(Transform value) => objective = value;
}
