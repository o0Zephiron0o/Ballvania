using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManagerSaveData
{

    public float playerHealth;
    public bool canStick;
    public int maxDash;

    public bool hasSavedHealth;
    public bool hasSavedProgression;

    public string key;
}
