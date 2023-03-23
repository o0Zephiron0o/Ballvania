using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SaveGameData
{   //dictionary key,value
    private Dictionary<string, SaveRecord> _dict = new Dictionary<string, SaveRecord>();

    [SerializeField]//[JsonProperty]
    private List<SaveRecord> _list = new List<SaveRecord>();

    public string SceneName;

    public void HandleBeforeSerialize()
    {
        UpdateListFromDict();
    }

    public void HandleAfterDeserialize()
    {
        UpdateDictFromList();
        foreach (var record in _list)
        {
            record.SetPersistent(true);
        }
    }

    private void UpdateListFromDict()
    {
        _list.Clear();
        foreach (var kvp in _dict)
        {
            _list.Add(kvp.Value);
        }
    }

    private void UpdateDictFromList()
    {
        _dict = new Dictionary<string, SaveRecord>();
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i] == null)
            {
                continue;
            }
            _dict.Add(_list[i].Key, _list[i]);
        }
    }

    public string GetData(string key)
    {
        return _dict.ContainsKey(key) ? _dict[key].Data : null;
    }

    public void SetData(SaveRecord saveRecord)
    {
        if (saveRecord == null) return;
        if (string.IsNullOrWhiteSpace(saveRecord.Data)) return;

        if (_dict.ContainsKey(saveRecord.Key))
        {
            _dict[saveRecord.Key].Data = saveRecord.Data;
        }
        else
        {
            _dict.Add(saveRecord.Key, saveRecord);
        }

    }

    public SaveGameData GetPersistentData()
    {
        SaveGameData persistentData = new SaveGameData();
        persistentData.SceneName = SceneName;
        persistentData._dict = _dict.Where(element => element.Value.IsPersistent)
            .ToDictionary(element => element.Key, element => element.Value);

        return persistentData;
    }
}
