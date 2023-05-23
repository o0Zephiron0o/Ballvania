using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    [SerializeField] private bool _isPersistent = true;
    public bool IsPersistent => _isPersistent;

    [SerializeField] private string _key;
    public string Key => _key;

    private bool _applied;

    protected virtual void OnEnable()
    {
        if (SaveSystem.Instance != null)
        {
            //SaveSystem.Instance.RegisterSaver(this);
        }
    }

    protected virtual void OnDisable()
    {
        if (SaveSystem.Instance != null)
        {
            //SaveSystem.Instance.UnregisterSaver(this);
        }
    }

    public SaveRecord BuildSaveRecord()
    {
        SaveRecord record = new SaveRecord(_key, BuildRecordDataJson(), _isPersistent);
        return record;
    }

    protected abstract string BuildRecordDataJson();
    public virtual void TryApplyData(string s)
    {
        if (!_applied && !string.IsNullOrEmpty(s))
        {
            ApplyDataFromJson(s);
        }

        _applied = true;
    }

    public void ResetSaver()
    {
        _applied = false;
    }

    protected abstract void ApplyDataFromJson(string s);

}

public abstract class Saver<TSaveData, TSavable> : Saver where TSaveData : class where TSavable : ISavable<TSaveData>
{
    [SerializeField] private TSavable _savable;

    protected override void ApplyDataFromJson(string s)
    {
        ApplyData(JsonUtility.FromJson<TSaveData>(s));
    }
    protected virtual void ApplyData(TSaveData loadedData)
    {
        _savable.ApplySaveData(loadedData);
    }

    protected override string BuildRecordDataJson()
    {
        TSaveData saveData = BuildRecordData();
        if (saveData == null)
        {
            return null;
        }
        return JsonUtility.ToJson(saveData);
    }

    protected virtual TSaveData BuildRecordData()
    {
        return _savable.GetSaveData();
    }
}
