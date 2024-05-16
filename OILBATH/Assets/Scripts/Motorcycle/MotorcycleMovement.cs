using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleMovement : MonoBehaviour
{
    [SerializeField] float speed = 7f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float boostSpeed = 50f;
    [SerializeField] float destroySpeed = 8f;
    [SerializeField] float boostDischargeSpeed = 1f;
    [SerializeField] float boostRechargeSpeed = 1f;
    [SerializeField] float maxBoost = 10f;
    Rigidbody2D rb;
    PlayerControls playerControls;
    bool boostPressed;
    float currentBoost;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        currentBoost = maxBoost;

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

        if (boostPressed && currentBoost > 0)
        {
            rb.AddForce(rb.velocity.normalized * boostSpeed, ForceMode2D.Force);
            currentBoost -= boostDischargeSpeed * Time.deltaTime;
        }
        else if (!boostPressed && currentBoost < maxBoost)
        {
            currentBoost += boostRechargeSpeed * Time.deltaTime;
        }

        if (currentBoost < 0)
        {
            currentBoost = 0;
        }
        if (currentBoost > maxBoost)
        {
            currentBoost = maxBoost;
        }
    }

    private void OnDestroy()
    {
        playerControls.Player.Disable();
    }

    public float GetDestroySpeed() => destroySpeed;
    public bool GetBoostPressed() => boostPressed;
    public float GetMaxBoost() => maxBoost;
    public float GetCurrentBoost() => currentBoost;
}
