using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject parent;

    public void Destroy()
    {
        Destroy(parent);
    }
}
