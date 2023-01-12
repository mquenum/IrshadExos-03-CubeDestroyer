using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    void OnMouseDown()
    {
        // Deactivate gameObject
        gameObject.SetActive(false);
    }
}
