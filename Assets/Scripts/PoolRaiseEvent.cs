using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolRaiseEvent : MonoBehaviour
{
    public RiadSpawner spawner;

    // Called when the object is disabled
    void OnDisabled()
    {
        spawner.AddToPool(gameObject);
    }
}
