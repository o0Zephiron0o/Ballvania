using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] SpriteRenderer _SR;

    private float _flashCount;
    private float _flashTimer = 1.0f;

    private bool _flashing = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _flashing = false;
            _SR.material.SetFloat("_FlashStrength", 0.0f);
            _flashTimer = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _flashing = true;
        }


        if(_flashing == true)
        {
            _flashTimer -= Time.deltaTime * 3;

            _SR.material.SetFloat("_FlashStrength", Mathf.PingPong(_flashTimer,1));
        }


    }
}
