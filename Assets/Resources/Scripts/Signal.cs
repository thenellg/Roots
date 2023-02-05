using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour{
public float value{get{
if(outOverride.linkedValue)return outOverride.value;
float rtn = producedSignal;
foreach(Signal node in ins){
if(node.value>rtn)rtn = node.value;
}
return rtn;
}}
public float Value;
public float lastValue;
public float threshold;
public float producedSignal;
[HideInInspector]public ExtraFunctions.Linked<float,bool> outOverride;
public List<Signal> ins = new List<Signal>();
public Sprite spr_active;
public Sprite spr_inactive;
public AudioClip snd_active;
public AudioClip snd_inactive;
public SpriteRenderer spriteRenderer;
void Update(){
if(spriteRenderer!=null){
spriteRenderer.sprite = (value>=threshold)?spr_active:spr_inactive;
}

if((lastValue<threshold&&value>=threshold)||(lastValue>=threshold&&value<threshold)){
if(GetComponent<AudioSource>()!=null){
if((lastValue<threshold&&value>=threshold)){
GetComponent<AudioSource>().clip = snd_active;
}else{
GetComponent<AudioSource>().clip = snd_inactive;
}
GetComponent<AudioSource>().Play();
}

}
lastValue = value;
}
void OnDrawGizmos(){
foreach(Signal signal in ins){
Gizmos.color = signal.value>0?Color.green:Color.red;
Gizmos.DrawLine(transform.position,signal.transform.position);
}
}

}
