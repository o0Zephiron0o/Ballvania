using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{

	private const string saveKeyBase = "Save";

	private List<Saver> _savers = new List<Saver>();

	private SaveGameData _saveGameData = new SaveGameData();

	private int _currentSlot;

	private PersistentDataPathDataStorer _storer = new PersistentDataPathDataStorer();


	private static SaveSystem instance;

	public static SaveSystem Instance => instance;

	public delegate void SaveDataApplied();
	public event SaveDataApplied OnSaveDataApplied;


    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }


    //public void RegisterSaver(Saver saver)
    //{
    //    if (saver == null || _savers.Contains(saver))
    //    {
    //        return;
    //    }

    //    if (string.IsNullOrWhiteSpace(saver.Key))
    //    {
    //        return;
    //    }
    //    _savers.Add(saver);
    //}

    //public void UnregisterSaver(Saver saver)
    //{
    //    _savers.Remove(saver);
    //}

	public void NewGame(int sceneIndex)
    {
		SaveSystem.Instance.ClearSavedGameData();
		StartCoroutine(LoadSceneCoroutine(sceneIndex));
	}

	private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
		SceneManager.LoadScene(sceneIndex);

		yield return null;

		ApplySaveGameData();
	}

	public void ChangeScene(int sceneIndex)
    {
		StartCoroutine(LoadSceneCoroutine(sceneIndex));
	}

    public void LoadGameFromSlot(int slotNumber, bool loadScene = true)
    {
        if (!HasSaveGameInSlot(slotNumber))
        {
            Debug.LogError("No save in slot: " + slotNumber);
            return;
        }

        StartCoroutine(LoadFromSlot(slotNumber, loadScene));
    }

    private IEnumerator LoadFromSlot(int slotNumber, bool loadScene = true)
    {
        _currentSlot = slotNumber;

        string saveGameJson = _storer.RetrieveData(GetSaveKey(slotNumber));
        _saveGameData = JsonUtility.FromJson<SaveGameData>(saveGameJson);
        _saveGameData.HandleAfterDeserialize();

        if (loadScene)
        {
            var locationName = _saveGameData.SceneName;
            SceneManager.LoadScene(locationName);
        }

		yield return null;

        ApplySaveGameData();

        yield break;
    }

	public void SaveGameToCurrentSlot(string sceneGuid, Action savedCallback)
	{
		SaveGameToSlot(_currentSlot, sceneGuid, savedCallback);
	}

	public void SaveGameToSlot(int slotNumber, string sceneName, Action savedCallback)
	{
		StartCoroutine(SaveToSlotCoroutine(slotNumber, sceneName, savedCallback));
	}

	private IEnumerator SaveToSlotCoroutine(int slotNumber, string sceneName, Action savedCallback)
	{
		RecordSaveGameData(sceneName);

		SaveGameData persistentGameData = _saveGameData.GetPersistentData();
		persistentGameData.HandleBeforeSerialize();
		string saveDataJson = JsonUtility.ToJson(persistentGameData);

		yield return _storer.StoreData(GetSaveKey(slotNumber), this, saveDataJson);

		savedCallback?.Invoke();
	}

	public void ClearSavedGameData()
	{
		_saveGameData = new SaveGameData();
	}

	/// <summary>
	/// Records the current scene's savers' data in to the SaveSystem's
	/// internal saved game data cache.
	/// </summary>
	/// <returns>The saved game data.</returns>
	/// <param name="sceneName">Scene name.</param>
	public SaveGameData RecordSaveGameData(string sceneName)
	{
		if (!string.IsNullOrEmpty(sceneName))
		{
			_saveGameData.SceneName = sceneName;
		}

		for (int i = 0; i < _savers.Count; i++)
		{
			try
			{
				Saver saver = _savers[i];
				_saveGameData.SetData(saver.BuildSaveRecord());
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}
		return _saveGameData;
	}

	/// <summary>
	/// Applies the saved game data to the savers in the current scene.
	/// </summary>
	/// <param name="saveGameData">Save game data.</param>
	public void ApplySaveGameData(SaveGameData saveGameData)
	{
		Debug.Log("apply save data");

		_savers = new List<Saver>();
		_savers.AddRange(FindObjectsOfType<Saver>());

		if (_savers.Count > 0)
		{
			for (int i = _savers.Count - 1; i >= 0; i--) // A saver may remove itself from list during apply.
			{
				try
				{
					if (0 <= i && i < _savers.Count)
					{
						Saver saver = _savers[i];

						Debug.Log(saver.gameObject.name);

						if (saver != null)
						{
							string saverDataString = saveGameData.GetData(saver.Key);
							saver.TryApplyData(saverDataString);
						}
					}
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
			}
		}

		OnSaveDataApplied?.Invoke();
	}

	/// <summary>
	/// Applies the most recently recorded saved game data.
	/// </summary>
	public void ApplySaveGameData()
	{
		ApplySaveGameData(_saveGameData);
	}

	/// <summary>
	/// Check whether there is a saved game in the specified slot.
	/// </summary>
	/// <returns><c>true</c>, if there is a saved game in the specified slot, <c>false</c> otherwise.</returns>
	/// <param name="slotNumber">Slot number.</param>
	public bool HasSaveGameInSlot(int slotNumber)
	{
		return _storer.HasData(GetSaveKey(slotNumber));
	}

	private string GetSaveKey(int slotNumber)
	{
		return saveKeyBase + slotNumber;
	}

}
