using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalProducer : MonoBehaviour{
public float value;
public float switchOffTime;
float c_switchOffTime;
public bool active;
public enum Type{
PlayerUse,
PlayerCollide
}
public Type TriggerOn;
Signal signal;

public bool playerTouching;

void Start(){
signal = GetComponent<Signal>();
}

void Update(){
if(active){
if(c_switchOffTime==0){
c_switchOffTime = switchOffTime;
}
if(c_switchOffTime>0){
c_switchOffTime -= Time.deltaTime;
if(c_switchOffTime<=0){
active = false;
c_switchOffTime=0;
}
}
}
signal.producedSignal = active?value:0;
if(Player.instance.controls.Get<Control>("Use").up&&TriggerOn==Type.PlayerUse&&playerTouching){
active = !active;
}

}

void OnTriggerEnter2D(Collider2D collision){
if(collision.transform==Player.instance.transform){
playerTouching = true;
if(TriggerOn==Type.PlayerCollide){
active = true;
}

}
}
void OnTriggerExit2D(Collider2D collision){
if(collision.transform==Player.instance.transform){
playerTouching = false;
if(TriggerOn==Type.PlayerCollide){
active = false;
}
}
}
}
