using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    // static is to use the same instance of a variable when it's called from this class
    // (better memory management)
    static private Camera _cam;

    // Start is called before the first frame update
    void Start()
    {
        // get the camera if we don't already have one
        if (!_cam)
        {
            _cam = Camera.main;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // use translation to move cubes toward the camera (negative value of -_cam.tranform.forward)
        transform.Translate(-_cam.transform.forward * _speed * Time.deltaTime);

        // InverseTransformPoint = converts world space to local space (opposite of Transform.TransformPoint)
        // to use if we are dealing with direction vectors rather than positions
        // if the object z axis is negative, then the object has move pass the camera position...
        if (_cam.transform.InverseTransformPoint(transform.position).z < -5.0f)
        {
            // ... and then we deactivate the object
            gameObject.SetActive(false);
        }
    }
}
