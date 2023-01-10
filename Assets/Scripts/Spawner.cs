using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    /*[SerializeField] int _nbObjectBySec = 10;*/
    [SerializeField] float _timer = 3.0f;
    /*[SerializeField] float _spawnCircleRadius = 1.0f;*/
    [SerializeField] private int _maxNumberOfObject = 10;
    [SerializeField] private float _distanceFromCamera = 5.0f;
    [SerializeField] private float _cubeSpeed = 10.0f;

    private List<GameObject> _objects;
    private int _counter = 0;
    private GameObject instPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _objects = new List<GameObject>();
        // launches function (1st parameter) at ²given _time (2nd parameter) and relaunches it every given _time
        // (3rd parameter, here one second divided by the number of obj by second)
        InvokeRepeating("ObjSpawn", 0.0f, _timer);
    }

    private void Update()
    {
        /*for (int i = 0; i < _objects.Count; i++)
        {
            _objects[i].transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, _cubeSpeed);
        }*/

        _objects[0].transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, _cubeSpeed);
    }

    private void ObjSpawn()
    {
        // exit function if no nb object set or negative nb
        /*if (_nbObjectBySec <= 0f) return;*/

        if (_obj)
        {
            // obj creation
            InstObj(_obj);
        }
    }

    private Vector3 GetPosition() {
        // we want to spawn the obj within a sphere bounds,
        // set the position inside the sphere radius multiplied my the desired radius
        Vector3 localPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), _distanceFromCamera);
        // make the starting point of our spawn point the same as the obj (instead of 0 0 0);
        Vector3 position = transform.TransformPoint(Camera.main.ScreenToWorldPoint(localPosition));
        return position;
    }

    private void InstObj(GameObject obj)
    {
        Vector3 position = GetPosition();

        if (_counter < _maxNumberOfObject)
        {
            instPrefab = GameObject.Instantiate(obj, position, Quaternion.identity);
            // set a random color to the obj (HSV = Hue Saturation Value)
            instPrefab.GetComponent<Renderer>().material.color = Random.ColorHSV();
            //instPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _objects.Add(instPrefab);
            _counter++;
        }
        else
        {
            // get the list first obj
            GameObject _firstObject = _objects[0];
            // set the new position of that first object
            _firstObject.transform.position = position;
            // set a random color to the obj (HSV = Hue Saturation Value)
            _firstObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
            // pop the list at item at index 0
            _objects.RemoveAt(0);
            // add the newly position first object et the end of list
            _objects.Add(_firstObject);
        }
        
    }

    private void MoveObj(GameObject obj)
    {
        obj.transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, _cubeSpeed);
    }
}
