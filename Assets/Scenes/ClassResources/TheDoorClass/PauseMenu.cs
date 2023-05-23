using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    [SerializeField] GameObject _pauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SaveSystem.Instance.SaveGameToCurrentSlot(SceneManager.GetActiveScene().name, ExitAfterSaved);
    }

    private void ExitAfterSaved()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
}
