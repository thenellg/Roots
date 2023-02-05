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

float escapeFade;
GameObject escapeFadeGO;

void Awake(){
instance = this;
scores = new List<Objective>(FindObjectsOfType(typeof(Objective)) as Objective[]);
PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"_scoreCount",scores.Count);
}
void Start(){
Load();
}
public void FullReset(){
Load();
Player.instance.FullReset();
}
void Update(){
//Escape
escapeFade -= Time.deltaTime;
if(Player.instance.controls.Get<Control>("Menu_Back").up){
if(escapeFade>0){
Player.instance.FullReset();
}else{ 
escapeFade = 3;
escapeFadeGO = Instantiate(Resources.Load<GameObject>("Prefabs/UI/TMPro Text w Canvas"));
escapeFadeGO.transform.parent = Player.instance.transform;
escapeFadeGO = escapeFadeGO.transform.Find("Canvas").transform.Find("Text (TMP)").gameObject;
escapeFadeGO.GetComponent<TMPro.TextMeshProUGUI>().text = "Press Escape again to restart level";
escapeFadeGO.transform.localPosition = Vector3.zero;
}
}
if(escapeFade>0){
escapeFadeGO.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1,0,0,escapeFade/3);
}else if(escapeFadeGO!=null){
Destroy(escapeFadeGO);
}

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
