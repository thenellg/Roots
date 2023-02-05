using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour{
GameObject talkSymbol;
public ExtraFunctions.TransformData talkSymbolTransform;
public Conversation conversation;
public bool onCollide;
public AudioClip popUpSFX;
public AudioClip popDownSFX;
public AudioSource SFXplayer;

void Start(){
if(GetComponent<CapsuleCollider2D>()==null)gameObject.AddComponent<CapsuleCollider2D>();
if(!GetComponent<CapsuleCollider2D>().isTrigger)GetComponent<CapsuleCollider2D>().isTrigger = true;
}
void Update(){
if(onCollide||Player.instance.controls.Get<Control>("Use").up){
if(talkSymbol!=null&&!transform.Find("Conversation")){
GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Dialogue Box"));
SFXplayer.PlayOneShot(popUpSFX);
go.GetComponent<DialogueBox>().conversation = conversation;
go.transform.parent = transform;
go.name = "Conversation";
}
}
}

void OnTriggerEnter2D(Collider2D collision){
if(collision.gameObject==Player.instance.gameObject){
if(talkSymbol==null){
talkSymbol = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Sprite"));
SpriteRenderer sr = talkSymbol.GetComponent<SpriteRenderer>();
if(!onCollide)sr.sprite = Resources.Load<Sprite>("Sprites/TalkSymbol");
talkSymbol.transform.SetParent(transform);
talkSymbolTransform.ApplyLocal(talkSymbol.transform);
}
}
}
void OnTriggerExit2D(Collider2D collision){
if(collision.gameObject==Player.instance.gameObject){
Destroy(talkSymbol);
}
//If conversation exists
if(onCollide){
Transform convo = transform.Find("Conversation");
if(convo!=null){
Destroy(convo.gameObject);
SFXplayer.PlayOneShot(popDownSFX);
}
}

}

}
