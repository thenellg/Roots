using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalDevice : MonoBehaviour{
public float Out;
public float threshold;
public float producedSignal;
public bool active;
float lastSignal;
public enum Type{
Toggle,
Inverter/*,
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
/*
case Type.LogicGate:

break;
case Type.Delay:

break;
*/
}
signal.outOverride.linkedValue = false;
lastSignal = signal.value;
}
}
