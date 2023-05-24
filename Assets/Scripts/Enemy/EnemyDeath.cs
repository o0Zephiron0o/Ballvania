using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject destructible;
    [SerializeField] GameObject deathParticle;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {

            Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity);

            Destroy(destructible);
        }
    }
}
