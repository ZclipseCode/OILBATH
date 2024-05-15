using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleMovement : MonoBehaviour
{
    [SerializeField] float speed = 7f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float boostSpeed = 50f;
    [SerializeField] float destroySpeed = 8f;
    Rigidbody2D rb;
    PlayerControls playerControls;
    bool boostPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    private void FixedUpdate()
    {
        Move();
        Boost();
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

    void Boost()
    {
        if (playerControls.Player.Boost.ReadValue<float>() > 0)
        {
            boostPressed = true;
        }
        else
        {
            boostPressed = false;
        }

        if (boostPressed)
        {
            rb.AddForce(rb.velocity.normalized * boostSpeed, ForceMode2D.Force);
        }
    }

    private void OnDestroy()
    {
        playerControls.Player.Disable();
    }

    public float GetDestroySpeed() => destroySpeed;
    public bool GetBoostPressed() => boostPressed;
}
