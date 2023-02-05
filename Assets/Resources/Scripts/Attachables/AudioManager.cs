using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{
public List<AudioClip> clips;
AudioSource audioSource;
int currentClip;
bool playSequence;
bool loop;
void Start(){
audioSource = gameObject.AddComponent<AudioSource>();
}
void Update(){
if(playSequence){
if(!audioSource.isPlaying){
advanceClip();
audioSource.clip = clips[currentClip];
audioSource.Play();
}
}
}

public void PlayRandom(bool interupt=false){
audioSource.clip = clips[Mathf.RoundToInt(Random.Range(0,clips.Count))];
if(!audioSource.isPlaying||interupt)audioSource.Play();
}
public void PlayNext(bool interupt=false){
audioSource.clip = clips[currentClip];
currentClip++;
if(!audioSource.isPlaying||interupt)audioSource.Play();
}
public void PlaySequence(bool Loop=false){
playSequence = true;
loop = Loop;
}
public void RestartSequence(bool restartAndPlay=true,bool Loop=false){
currentClip = 0;
if(restartAndPlay)PlaySequence(Loop);
}
void advanceClip(){ 
currentClip++;
if(currentClip>clips.Count)currentClip = 0;
}
}
