using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public int sceneIndex;
    public buttonAudioPlayer SFX;

    public void loadGame()
    {
        SFX.playSelect();
        Invoke("loadScene", SFX.select.length);
    }

    public void loadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void newGame()
    {
        PlayerPrefs.DeleteAll();
        loadGame();
    }

}
