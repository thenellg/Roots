using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour{
#region References
GameObject platform;
Transform path;
Signal signal;
#endregion References
public bool passable;
public bool cycle;
public float speed;
public float waitTime;
float currentWaitTime;
bool reverse;
int stop;
public List<GameObject> passengers;
public float startSurfaceArc;
void Start(){
platform = transform.Find("Platform").gameObject;
path = transform.Find("Path");
platform.transform.position = path.GetChild(0).position;
stop = 1;
startSurfaceArc = platform.GetComponent<PlatformEffector2D>().surfaceArc;
signal = GetComponent<Signal>();
}

void Update(){
//Pass through
if(passable){
if(Player.instance.controls.Get<ControlVector2>("Move").y<0){
platform.GetComponent<PlatformEffector2D>().surfaceArc = 0;
}else{
platform.GetComponent<PlatformEffector2D>().surfaceArc = startSurfaceArc;
}
}else{ 
platform.GetComponent<PlatformEffector2D>().surfaceArc = 360;
}

//Move
if(path.childCount>0){
if(currentWaitTime<=0){
platform.transform.position += (path.GetChild(stop).position-platform.transform.position).normalized*(signal.value>=signal.threshold?speed:0);
//Move whatever's riding on the platform with the platform
foreach(GameObject go in passengers){
//go.GetComponent<Rigidbody2D>().velocity = (path.GetChild(stop).position-platform.transform.position).normalized*speed;
go.transform.position += (path.GetChild(stop).position-platform.transform.position).normalized*speed;
}

if((path.GetChild(stop).position-platform.transform.position).magnitude<speed){
//platform.transform.position = path.GetChild(stop).position;
currentWaitTime = waitTime;
stop += reverse?-1:1;
//Reached end of path
if(stop>path.childCount-1){
if(cycle){
stop = 0;
}else{
stop -= 2;
reverse = !reverse;
}
}
//Reached beggining of path
if(stop<0){
stop = 1;
reverse = !reverse;
}

}
}else{ 
currentWaitTime -= Time.deltaTime;
}
}

}

void OnCollisionEnter2D(Collision2D collision){

if(!passengers.Contains(collision.gameObject)){

RaycastHit2D[] results = Physics2D.RaycastAll(platform.transform.position+new Vector3(.1f-platform.transform.localScale.x/2,.1f+platform.transform.localScale.y/2,0),new Vector2(1,0), platform.transform.localScale.x*.8f);
bool isOnTop = false;
foreach(RaycastHit2D hit in results){
if(hit.rigidbody==collision.rigidbody)isOnTop = true;
}
if(isOnTop){
passengers.Add(collision.gameObject);
}
}
}

void OnCollisionExit2D(Collision2D collision){
if(passengers.Contains(collision.gameObject)){
passengers.Remove(collision.gameObject);
}
}

void OnDrawGizmos(){

if(platform==null)platform = transform.Find("Platform").gameObject;
if(path==null)path = transform.Find("Path");
if(path!=null){
Gizmos.color = Color.yellow;
Transform firstTf = null;
Transform lastTf = null;
foreach(Transform tf in path){
if(lastTf==null){
firstTf = tf;
}else{
Gizmos.DrawLine(lastTf.position,tf.position);
}
lastTf = tf;
}
if(firstTf!=null&&cycle)Gizmos.DrawLine(firstTf.position,lastTf.position);
}
}

}
