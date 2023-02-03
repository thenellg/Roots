using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour{
public static Level instance;
[Tooltip("When the players Y value drops below this, their position will restart")]
public float resetHeight;
public bool levelComplete;
public List<Objective> scores;
public bool resetData;
void Awake(){
instance = this;
scores = new List<Objective>(FindObjectsOfType(typeof(Objective)) as Objective[]);
PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"_scoreCount",scores.Count);
}
void Start(){
Load();
}

void Update(){
//Restart if fall off stage
if(Player.instance.transform.position.y<resetHeight)Player.instance._Reset();
if(resetData){resetData=false;WipeData();}
}

public void WipeData(){
levelComplete = false;
PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"_complete",0);
for(int i=0; i<scores.Count;i++){
scores[i].collected = false;
//if score is uncollected
PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"_score_"+i,0);
}
}
public void Load(){
levelComplete = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"_complete")>0;
for(int i=0; i<scores.Count;i++){
//if score is uncollected
scores[i].collected = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"_score_"+i)==1;
scores[i].GetComponent<SpriteRenderer>().sprite = scores[i].collected?scores[i].spr_collected:scores[i].spr_uncollected;
}

}

public void Save(){
//Level Complete?
PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"_complete",levelComplete?1:0);
for(int i=0; i<scores.Count;i++){
//if score is uncollected
if(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"_score_"+i)==0){
PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"_score_"+i,scores[i].collected?1:0);
}
}//for

}

}
