using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider healthBarSlider;

    bool _subscribed;

    private void Update()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }

        if (!_subscribed && playerStats != null)
        {
            playerStats.updateHealth += OnUpdateHealth;
            _subscribed = true;
            OnUpdateHealth(playerStats.currentHP, playerStats.MaxHp);
        }
    }

    private void OnDestroy()
    {
        if (_subscribed && playerStats != null)
        {
            playerStats.updateHealth -= OnUpdateHealth;
            _subscribed = false;
        }
    }


    public void OnUpdateHealth(float currentHealth, float maxHealth)
    {
        healthBarSlider.value = currentHealth;
    }

}
