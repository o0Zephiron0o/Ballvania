using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSystem : MonoBehaviour
{

    void Start()
    {
        GameManager.Instance.SpawnPlayer();
    }


}
