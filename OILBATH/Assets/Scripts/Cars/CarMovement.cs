using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject explosion;
    float maxSpeed;
    Transform objective;
    Rigidbody2D rb;
    float difficulty = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        difficulty = Difficulty.difficulty;
        maxSpeed = speed + difficulty;
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

        if (distance.magnitude < 0.1f)
        {
            CarSpawner.carReady?.Invoke();
            Destroy(gameObject);
        }

        Vector2 moveDirection = distance.normalized;
        Vector2 targetVelocity = moveDirection * maxSpeed;
        Vector2 velocityChange = targetVelocity - rb.velocity;

        rb.AddForce(velocityChange * (speed + difficulty), ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            MotorcycleMovement motorcycleMovement = collision.gameObject.GetComponent<MotorcycleMovement>();

            if (motorcycleMovement.GetBoostPressed() && motorcycleMovement.GetCurrentBoost() > 0f && collision.relativeVelocity.magnitude >= motorcycleMovement.GetDestroySpeed())
            {
                Score.boostDestroy?.Invoke();
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public Transform GetObjective() => objective;
    public void SetObjective(Transform value) => objective = value;
}
