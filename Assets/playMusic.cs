using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusic : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip loop;

    public AudioSource music;

    private void Start()
    {
        music.PlayOneShot(intro);
        Invoke("audioLoop", intro.length);
    }
    public void audioLoop()
    {
        music.clip = loop;
        music.loop = true;
        music.Play();
    }

}
