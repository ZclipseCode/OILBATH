using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOffense : MonoBehaviour
{
    [SerializeField] CarMovement movement;
    [SerializeField] CarSpriteStacking spriteStacking;
    [SerializeField] GameObject explosion;
    Transform player;
    SpriteRenderer[] spriteRenderers;
    bool selfDestructInitiated;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movement.SetObjective(player);
        spriteRenderers = spriteStacking.GetSpriteRenderers();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !selfDestructInitiated)
        {
            selfDestructInitiated = true;
            StartCoroutine(SelfDestruct());
        }
    }

    IEnumerator SelfDestruct()
    {
        int blinks = 5;
        float blinkRate = 0.5f;

        for (int i = 0; i < blinks; i++)
        {
            foreach (SpriteRenderer sr in  spriteRenderers)
            {
                sr.color = new Color(255f, 0f, 0f);
            }

            yield return new WaitForSeconds(0.2f);

            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.color = new Color(255f, 255f, 255f);
            }

            yield return new WaitForSeconds(blinkRate);

            blinkRate /= 2;
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
