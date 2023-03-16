using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;

    public string key = "TopKey";

    [SerializeField] GameObject player;

    private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    private bool _hasSavedHealth;
    public bool HasSavedHealth => _hasSavedHealth;

    protected virtual void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

    }

    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    public void RecordPlayerHealth(PlayerStats player)
    {
        _currentHealth = player.currentHP;
        _hasSavedHealth = true;
    }


    public void SpawnPlayer()
    {
        SpawnPoints[] spawnList = FindObjectsOfType<SpawnPoints>();

        foreach(SpawnPoints spawnPoint in spawnList)
        {
            if(key == spawnPoint.spawnKey)
            {
                Instantiate(player, spawnPoint.transform.position,Quaternion.identity);


            }
        }


        
    }

}
