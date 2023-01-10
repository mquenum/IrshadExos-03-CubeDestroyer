using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiadSpawner : MonoBehaviour
{
    [SerializeField] private Transform _objectToSpawn;
    [SerializeField] private float _distanceToCam = 10.0f;
    [SerializeField] private int _maxNumberOfObject;
    [SerializeField] private float _spawnSpeed = 1.0f;
    [SerializeField] private float _cubeSpeed = 10.0f;

    private int _objCounter;
    private Transform _listFirstObject;
    private List<Transform> _objects = new List<Transform>();
    private float _time = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnSpeed <= 0f) return;

        _time += Time.deltaTime;

        if (_time >= 1f / _spawnSpeed && _objCounter < _maxNumberOfObject)
        {
            // On r�cup�re l'objet qu'on fait apparaitre
            Transform spawnedObject = Instantiate(_objectToSpawn);
            Setup(spawnedObject);

            // On l'ajoute � la pool
            _objects.Add(spawnedObject);

            // On augmente le compteur d'objet
            _objCounter++;
            _time = 0f;
        }
        else if (_time >= 1f / _spawnSpeed)
        {
            // Quand on a atteint la limite d'objet
            // On r�cup�re le premier objet de la liste
            _listFirstObject = _objects[0];

            Setup(_listFirstObject);

            // On met � jour la liste
            _objects.RemoveAt(0);
            _objects.Add(_listFirstObject);
            _time = 0f;
        }
        
        // loop thru _objects list to make it all go towards the camera
        for (int i = 0; i < _objects.Count; i++)
        {
            _objects[i].transform.position = Vector3.MoveTowards(_objects[i].transform.position, Camera.main.transform.position, _cubeSpeed * Time.deltaTime);
        }

        // launch destroy on click function
        DetectObj();
    }

    private void DetectObj()
    {
        // on click
        if (Input.GetMouseButtonDown(0))
        {
            // raycast creation
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // if the detected object has the "Opponent" tag
                if (hit.collider.gameObject.CompareTag("Opponent")){
                    // get the object detected by raycast
                    GameObject obj = hit.collider.gameObject;
                    // get the index of detected object in _objects 
                    int indexOfObj = _objects.IndexOf(hit.collider.transform);
                    // remove the detected object from the list
                    _objects.RemoveAt(indexOfObj);
                    // destroy the detected object
                    Destroy(obj);
                    // decrement the _objCounter so as opponent keeps coming at us
                    _objCounter--;
                }
            }
        }
    }

    private void Setup(Transform transformToSetup)
    {
        // Placer l'objet
        Vector3 worldPosition = ComputePosition();
        transformToSetup.position = worldPosition;

        // Changer la couleur de l'objet    
        transformToSetup.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    private Vector3 ComputePosition()
    {
        Vector3 localPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), _distanceToCam);
        // make the starting point of our spawn point the same as the obj (instead of 0 0 0);
        Vector3 position = transform.TransformPoint(Camera.main.ScreenToWorldPoint(localPosition));

        return position;
    }
}
