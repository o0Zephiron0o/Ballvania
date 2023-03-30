using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSystem : MonoBehaviour
{
    private void OnEnable()
    {
        if (SaveSystem.Instance != null)
        {
            SaveSystem.Instance.OnSaveDataApplied += SpawnPlayer;
        }
    }

    private void OnDisable()
    {
        if (SaveSystem.Instance != null)
        {
            SaveSystem.Instance.OnSaveDataApplied -= SpawnPlayer;
        }
    }


    void SpawnPlayer()
    {
        GameManager.Instance.SpawnPlayer();
        Debug.Log("spawn player");
    }


}
