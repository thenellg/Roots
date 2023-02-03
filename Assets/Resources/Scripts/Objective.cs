using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour{
public bool mainObjective;
public Sprite spr_uncollected;
public Sprite spr_collected;
public bool collected;
public int index;
void Start(){
index = Level.instance.scores.Count-1;
}
void OnTriggerEnter2D(Collider2D collision){
if(collision.transform==Player.instance.transform){
if(mainObjective)Level.instance.levelComplete = true;
collected = true;
if(GetComponent<AudioSource>()!=null)GetComponent<AudioSource>().Play();
Destroy(GetComponent<SpriteRenderer>());
Destroy(GetComponent<BoxCollider2D>());
}
}
}
