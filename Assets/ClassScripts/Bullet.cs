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
        _rb.velocity = new Vector2(_projectileSpeed, _rb.velocity.y);   
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
