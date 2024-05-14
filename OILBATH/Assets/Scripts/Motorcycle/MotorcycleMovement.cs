using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float maxSpeed = 1f;
    Rigidbody2D rb;
    PlayerControls playerControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontal = playerControls.Player.Movement.ReadValue<Vector2>().x;
        float vertical = playerControls.Player.Movement.ReadValue<Vector2>().y;

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;
        Vector2 targetVelocity = moveDirection * maxSpeed;
        Vector2 velocityChange = targetVelocity - rb.velocity;

        rb.AddForce(velocityChange * speed, ForceMode2D.Force);
    }

    private void OnDestroy()
    {
        playerControls.Player.Disable();
    }
}
