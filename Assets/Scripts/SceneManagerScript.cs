using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SelectionScene()
    {
        SceneManager.LoadScene(1);
    }

    public void MapSelection()
    {
        SceneManager.LoadScene(2);
    }

    public void GameScene()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitScene()
    {
        Application.Quit();
    }
}
