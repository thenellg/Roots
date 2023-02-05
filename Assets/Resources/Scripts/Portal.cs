using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour{
public string scene;
bool playerTouching;
public Sprite spr_PortalLevelComplete;
[Header("Score Display")]
Transform score;
public Sprite spr_uncollected;
public Sprite spr_collected;
public Sprite spr_levelComplete;
public Sprite spr_levelNotComplete;
public Vector3 scoresOffset;
public Vector3 scoreSpacing;
public Vector3 scoreSize;
public int scorewidth;

void Start(){
score = transform.Find("Scores");
int count = PlayerPrefs.GetInt(scene+"_scoreCount");
for(int i=0; i<count;i++){
GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Sprite"));
go.transform.SetParent(score);
go.transform.localPosition = scoresOffset+new Vector3((i%scorewidth)*scoreSpacing.x,Mathf.RoundToInt(i/scorewidth)*scoreSpacing.y*-1,0);
go.transform.localScale = scoreSize;
go.GetComponent<SpriteRenderer>().sprite = (PlayerPrefs.GetInt(scene+"_score_"+i)==1)?spr_collected:spr_uncollected;
}
if(PlayerPrefs.GetInt(scene+"_complete")==1)GetComponent<SpriteRenderer>().sprite = spr_PortalLevelComplete;
}
void Update(){

if(Player.instance.controls.Get<Control>("Use").up&&playerTouching){
Level.instance.Save();
SceneManager.LoadScene(scene);
}

}
void OnTriggerEnter2D(Collider2D collision){
if(collision.transform==Player.instance.transform){
playerTouching = true;
}
}
void OnTriggerExit2D(Collider2D collision){
if(collision.transform==Player.instance.transform){
playerTouching = false;
}
}

}
