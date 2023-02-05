using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonAudioPlayer : MonoBehaviour
{
    public AudioSource SFXPlayer;
    public AudioClip select;
    public AudioClip hover;

    public void playHover()
    {
        SFXPlayer.PlayOneShot(hover);
    }

    public void playSelect()
    {
        SFXPlayer.PlayOneShot(select);
    }
}
