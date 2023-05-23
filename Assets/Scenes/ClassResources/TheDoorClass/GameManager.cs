using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISavable<GameManagerSaveData>
{
    private static GameManager instance;

    public static GameManager Instance => instance;

    public string key = "TopKey";

    [SerializeField] GameObject player;

    private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    private bool _hasSavedHealth;
    public bool HasSavedHealth => _hasSavedHealth;





    private int _maxDash;
    public int MaxDash => _maxDash;

    private bool _canStick;
    public bool CanStick => _canStick;

    private bool _hasSavedProgression;
    public bool HasSavedProgression => _hasSavedProgression;



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
        _currentHealth = player.CurrentHP;
        _hasSavedHealth = true;
    }

    public void RecordPlayerPowerUps(PlayerController player)
    {
        _maxDash = player.MaxDash;
        _canStick = player.CanStick;
        _hasSavedProgression = true;
    }




    public void SpawnPlayer()
    {
        SpawnPoints[] spawnList = FindObjectsOfType<SpawnPoints>();

        foreach(SpawnPoints spawnPoint in spawnList)
        {
            Debug.Log("Got in foreach");

            if(key == spawnPoint.spawnKey)
            {
                Instantiate(player, spawnPoint.transform.position,Quaternion.identity);
                Debug.Log("player got spawned");

            }
        }


        
    }

    public void ApplySaveData(GameManagerSaveData saveData)
    {
        _currentHealth = saveData.playerHealth;
        _canStick = saveData.canStick;
        _maxDash = saveData.maxDash;
        _hasSavedHealth = saveData.hasSavedHealth;
        _hasSavedProgression = saveData.hasSavedProgression;
        key = saveData.key;
    }

    public GameManagerSaveData GetSaveData()
    {
        GameManagerSaveData saveData = new GameManagerSaveData();

        saveData.playerHealth = _currentHealth;
        saveData.canStick = _canStick;
        saveData.maxDash = _maxDash;
        saveData.hasSavedHealth = _hasSavedHealth;
        saveData.hasSavedProgression = _hasSavedProgression;
        saveData.key = key;

        return saveData;
    }
}
