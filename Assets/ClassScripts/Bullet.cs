using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _projectileSpeed;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Invoke("Despawn", 5);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = transform.up * _projectileSpeed;
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
