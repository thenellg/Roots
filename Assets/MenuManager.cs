using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public int sceneIndex;
    public buttonAudioPlayer SFX;

    public bool onStartScreen = true;
    public GameObject startScreen;
    public GameObject mainMenu;

    private void Update()
    {
        if(onStartScreen && Input.anyKey)
        {
            startScreen.SetActive(false);
            mainMenu.SetActive(true);
            onStartScreen = false;
        }
    }

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
