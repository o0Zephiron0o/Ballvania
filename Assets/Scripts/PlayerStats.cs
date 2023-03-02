using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float _maxHp;
    [SerializeField] float _currentHp;

    void Start()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        
        //invoke
        //?.invoke will check if its null or not before invoking
        updateHealth?.Invoke(_currentHp, _maxHp);
    }

    public delegate void UpdateHealth(float currentHealth, float maxHealth);

    public event UpdateHealth updateHealth;

    

}
