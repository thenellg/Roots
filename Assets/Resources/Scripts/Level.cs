using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour{
public static Level instance;
public float resetHeight;

void Start(){
instance = this;
}

void Update(){
//Restart if fall off stage
if(Player.instance.transform.position.y<resetHeight)Player.instance._Reset();
}
}
