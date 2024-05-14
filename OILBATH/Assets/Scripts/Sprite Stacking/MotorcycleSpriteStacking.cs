using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleSpriteStacking : SpriteStacking
{
    [SerializeField] float rotationSpeed = 1;
    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    override protected void SpriteStackRotation()
    {
        float horizontal = playerControls.Player.Movement.ReadValue<Vector2>().x;
        float vertical = playerControls.Player.Movement.ReadValue<Vector2>().y;
        
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (horizontal != 0 || vertical != 0)
            {
                float targetAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

                spriteRenderers[i].transform.rotation = Quaternion.Slerp(spriteRenderers[i].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnDestroy()
    {
        playerControls.Player.Disable();
    }
}
