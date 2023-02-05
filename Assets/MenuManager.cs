using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public int sceneIndex;

    public void loadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void newGame()
    {
        PlayerPrefs.DeleteAll();
        loadScene();
    }
}
