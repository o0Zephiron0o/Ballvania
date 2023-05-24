using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    [SerializeField] float _maxHp;
    public float MaxHp => _maxHp;

    [SerializeField] float _currentHp;
    public float CurrentHP => _currentHp;

    private bool _invincible = false;
    [SerializeField] float _invincibleDuration = 0.5f;
    private float _invincibleTimeCounter;

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

    private void Update()
    {
        if(CurrentHP <= 0)
        {
            Destroy(gameObject);

            GameManager.Instance.ReturnToMainMenu();
        }

        if(_invincible == true)
        {
            _invincibleTimeCounter += Time.deltaTime;
        }

        if(_invincibleTimeCounter >= _invincibleDuration)
        {
            _invincible = false ;
            _invincibleTimeCounter = 0;
        }


    }

    public void TakeDamage(float damage)
    {

        if(_invincible == false)
        {
            _currentHp -= damage;

            //invoke
            //?.invoke will check if its null or not before invoking
            updateHealth?.Invoke(_currentHp, _maxHp);
            GameManager.Instance.RecordPlayerHealth(this);
        }

        _invincible = true;
        
       
    }

    public delegate void UpdateHealth(float currentHealth, float maxHealth);

    public event UpdateHealth updateHealth;

    

}
