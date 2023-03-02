using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider healthBarSlider;

    private void OnEnable()
    {
        playerStats.updateHealth += OnUpdateHealth;
    }

    private void OnDisable()
    {
        playerStats.updateHealth -= OnUpdateHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpdateHealth(float currentHealth, float maxHealth)
    {
        healthBarSlider.value = currentHealth;
    }

}
