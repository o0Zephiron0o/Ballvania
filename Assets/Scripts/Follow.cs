using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] float xOffset = 1;
    [SerializeField] float yOffset = 1;

    private void Start()
    {
        //target = GameObject.Find("PlayerBall");
    }


    void Update()
    {
        if(target == null)
        {
            target = FindObjectOfType<PlayerStats>()?.gameObject;
        }
        
        gameObject.transform.position = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset, gameObject.transform.position.z);
    }
}
