using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpriteStacking : SpriteStacking
{
    [SerializeField] CarMovement movement;

    override protected void SpriteStackRotation()
    {
        Transform objective = movement.GetObjective();

        if (objective != null)
        {
            Vector2 directionToObjective = (objective.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(directionToObjective.y, directionToObjective.x) * Mathf.Rad2Deg;

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                spriteRenderers[i].transform.rotation = Quaternion.Slerp(spriteRenderers[i].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public SpriteRenderer[] GetSpriteRenderers() => spriteRenderers;
}
