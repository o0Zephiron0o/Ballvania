using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpikeFalling : MonoBehaviour
{
    //Debug
    [SerializeField] private bool isPlayer = false;


    private Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            rb.isKinematic = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            rb.isKinematic = true;

    }
}
