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
public float producedSignal;
[HideInInspector]public ExtraFunctions.Linked<float,bool> outOverride;
public List<Signal> ins = new List<Signal>();

void OnDrawGizmos(){
foreach(Signal signal in ins){
Gizmos.color = signal.value>0?Color.green:Color.red;
Gizmos.DrawLine(transform.position,signal.transform.position);
}
}

}
