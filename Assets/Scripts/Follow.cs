using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void Start()
    {
        target = GameObject.Find("PlayerBall");
    }


    void Update()
    {
        gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
    }
}
