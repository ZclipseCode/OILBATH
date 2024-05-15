using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteStacking : MonoBehaviour
{
    // sprite layers must increment by one

    [SerializeField] protected SpriteRenderer[] spriteRenderers;
    [SerializeField] protected float rotationSpeed = 20;

    private void Start()
    {
        float offset = 0f;

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].transform.position = new Vector3(
                spriteRenderers[i].transform.position.x,
                spriteRenderers[i].transform.position.y + offset,
                spriteRenderers[i].transform.position.z);

            offset += 0.05f;
        }
    }

    private void Update()
    {
        SpriteStackRotation();
    }

    virtual protected void SpriteStackRotation()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 100f));
        }
    }
}
