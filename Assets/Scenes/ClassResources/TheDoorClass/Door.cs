using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] string _key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.key = _key;

        SaveSystem.Instance.ChangeScene(sceneIndex);
    }
}
