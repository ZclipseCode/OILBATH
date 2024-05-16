using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] GameObject explosion;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            MotorcycleMovement motorcycleMovement = collision.gameObject.GetComponent<MotorcycleMovement>();

            if (motorcycleMovement.GetBoostPressed() && motorcycleMovement.GetCurrentBoost() > 0f && collision.relativeVelocity.magnitude >= motorcycleMovement.GetDestroySpeed())
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public Transform GetObjective() => objective;
    public void SetObjective(Transform value) => objective = value;
}
