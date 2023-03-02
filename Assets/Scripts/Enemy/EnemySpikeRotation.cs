using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikeRotation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _pivotObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_pivotObject.transform.position, new Vector3(0, 0, 1), _speed * Time.deltaTime);
    }
}
