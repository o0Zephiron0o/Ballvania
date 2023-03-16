using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float _maxHp;
    public float MaxHp => _maxHp;

    [SerializeField] float _currentHp;
    public float currentHP => _currentHp;

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    void Start()
    {
        if (GameManager.Instance.HasSavedHealth)
        {
            _currentHp = GameManager.Instance.CurrentHealth;
            updateHealth?.Invoke(_currentHp, _maxHp);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        
        //invoke
        //?.invoke will check if its null or not before invoking
        updateHealth?.Invoke(_currentHp, _maxHp);
        GameManager.Instance.RecordPlayerHealth(this);
    }

    public delegate void UpdateHealth(float currentHealth, float maxHealth);

    public event UpdateHealth updateHealth;

    

}
