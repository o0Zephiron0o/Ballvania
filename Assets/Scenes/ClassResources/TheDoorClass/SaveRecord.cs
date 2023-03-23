using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveRecord
{
    [SerializeField] private string _key;
    public string Key => _key;

    public string Data;

    private bool _isPersistent;
    public bool IsPersistent => _isPersistent;

    public SaveRecord(string key, string data, bool isPersistent)
    {
        _key = key;
        Data = data;
        _isPersistent = isPersistent;
    }

    public void SetPersistent(bool value)
    {
        _isPersistent = value;
    }
}
