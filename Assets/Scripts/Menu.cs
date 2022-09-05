using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Menu Opener")]
    public KeyCode openKey;
    public GameObject openMenu;


    void Update()
    {
        if(openKey != KeyCode.None && openMenu != null)
        {
            if(Input.GetKeyDown(openKey))
                PauseResumeGame();
        }
    }

    public void PauseResumeGame()
    {
        if(openMenu.activeInHierarchy)
        {
            Time.timeScale = 1;
            openMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            openMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeToScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeToScene(int sceneId)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
    }

    public async void DeleteAllData()
    {
        await GameController.DeleteSaves();
        Time.timeScale = 1;
        ChangeToScene("Game");
    }
}
