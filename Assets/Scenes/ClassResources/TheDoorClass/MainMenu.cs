using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        SaveSystem.Instance.NewGame(1);
    }

    public void Continue()
    {
        SaveSystem.Instance.LoadGameFromSlot(0);
    }
}
