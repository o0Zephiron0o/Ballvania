using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable<TSaveData> where TSaveData : class
{
    void ApplySaveData(TSaveData saveData);
    TSaveData GetSaveData();
}
