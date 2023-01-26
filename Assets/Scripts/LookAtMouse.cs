using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Vector2 lookDirection;
    private float lookAngle;


    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);    
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}
