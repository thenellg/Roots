using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalDevice : MonoBehaviour{
public float Out;
public float threshold;
public float producedSignal;
public bool active;
public List<ExtraFunctions.Linked<Signal,bool>> ins;
float lastSignal;
public enum Type{
Toggle,
Inverter,
AndGate/*,
LogicGate,
Delay
*/
}
public Type type;
Signal signal;
public enum LogicGate{
True,
False,
DontCare
}

void Start(){
signal = GetComponent<Signal>();
}

void Update(){
signal.outOverride.linkedValue = false;
switch(type){
case Type.Toggle:
if(signal.value>=threshold&&lastSignal<threshold){
active = !active;
}
signal.outOverride.value = active?producedSignal:0;
break;
case Type.Inverter:
if(signal.value>0){signal.outOverride.value=0;}else{signal.outOverride.value=producedSignal;}
break;
case Type.AndGate:
active = true;
string str = "";
foreach(ExtraFunctions.Linked<Signal,bool> In in ins){
//if(In.linkedValue?!(In.value.value < threshold):(In.value.value < threshold))
if(In.value.value < threshold)active = false;
str += In+" "+In.value;
}

signal.producedSignal = active?producedSignal:0;
break;
/*
case Type.Delay:

break;
*/
}
signal.outOverride.linkedValue = false;
lastSignal = signal.value;
}
}
