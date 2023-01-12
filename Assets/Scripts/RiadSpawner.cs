using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float _timer = 1.0f;
    [SerializeField] private float _distanceFromCam = 10.0f;
    [SerializeField] private int _numberToSpawn = 5;

    private List<GameObject> _disabledObjects = new List<GameObject>();
    private float _time = 0f;

    // Awake is always init even it object is disabled
    // OnEnabled: when the object is enabled
    // Start
    // Update
    // OnDiabled: when the object is disabled
    // OnDestroyed: when obj is destroyed

    // Start is called before the first frame update
    void Start()
    {
        // Creation of Pool
        CreateObjPool(_numberToSpawn);
    }

    private void CreateObjPool(int numOfObj)
    {
        // create object numOfObj times
        for (int i = 0; i < numOfObj; i++)
        {
            // New object
            GameObject spawnObj = SpawnObj();
            // disable instantiated object
            spawnObj.SetActive(false);
        }
    }

    private GameObject SpawnObj()
    {
        Vector3 position = ComputeRandomPosition();
        GameObject obj = Instantiate(_objectToSpawn, position, Quaternion.identity);

        return obj;
    }

    public void AddToPool(GameObject objToAdd)
    {
        _disabledObjects.Add(objToAdd);
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _timer)
        {
            _time = 0f;

            // if no obj to spawn, wait until next cycle
            if (_disabledObjects.Count == 0) return;

            // get 1st obj from list
            GameObject objToEnable = _disabledObjects[0];

            // set random position
            objToEnable.transform.position = ComputeRandomPosition();

            // remove from pool
            _disabledObjects.RemoveAt(0);

            // activate the gameObject
            objToEnable.SetActive(true);
        }
    }

    Vector3 ComputeRandomPosition()
    {
        Vector3 localPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), _distanceFromCam);
        // make the starting point of our spawn point the same as the obj (instead of 0 0 0);
        //Vector3 position = transform.TransformPoint(_cam.ScreenToWorldPoint(localPosition));
        // cleaner way
        Vector3 position = Camera.main.ScreenToWorldPoint(localPosition);

        return position;
    }
}
