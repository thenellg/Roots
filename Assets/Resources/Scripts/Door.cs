using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{
Signal signal;
public float signalThreshold;
public bool closeOnPassthrough;

void Start(){
signal = GetComponent<Signal>();
}
void Update(){
GetComponent<BoxCollider2D>().enabled = signal.value<signalThreshold;
}

}
