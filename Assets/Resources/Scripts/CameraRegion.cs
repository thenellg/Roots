using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

public class CameraRegion : MonoBehaviour{
public Transform target;
public TransformData offset;
[Tooltip("How long this effect will last.")]
public float lifeSpan;
float currentLifeSpan;
[Tooltip("Camera region will be destroyed at end of life")]
public bool deleteOnEnd;
Vector3 prevPos;
Vector3 prevRot;
Vector3 prevScale;
Transform camOwner;

void Start(){
currentLifeSpan = lifeSpan;
}
void Update(){ 
if(camOwner!=null){
if(currentLifeSpan>0){
currentLifeSpan -= Time.deltaTime;
//End of life
if(currentLifeSpan<=0){
if(deleteOnEnd)Destroy(gameObject);
ReturnCamera();
}
}

}
}
void ReturnCamera(){
Transform camTF = Camera.main.transform;
camTF.parent = Player.instance.transform;
camTF.localPosition = prevPos;
camTF.localEulerAngles = prevRot;
camTF.localScale = prevScale;
currentLifeSpan = lifeSpan;
camOwner = null;
}
void OnTriggerEnter2D(Collider2D collision){
if(collision.transform==Camera.main.transform.parent){
camOwner = collision.transform;
Transform camTF = Camera.main.transform;
prevPos = camTF.localPosition;
prevRot = camTF.localEulerAngles;
prevScale = camTF.localScale;
camTF.parent = target;
camTF.localPosition = offset.position;
camTF.localEulerAngles = offset.rotation;
camTF.localScale = Vector3.one+offset.scale;
}
}
void OnTriggerExit2D(Collider2D collision){
if(collision.transform==camOwner){
ReturnCamera();
}
}
void OnDrawGizmos(){
BoxCollider2D collider = GetComponent<BoxCollider2D>();
Gizmos.color = Color.red;
Vector2 temp = collider.size.Multiply(transform.localScale/2);
Gizmos.DrawLine(transform.position+new Vector3(temp.x,temp.y),transform.position+new Vector3(0-temp.x,temp.y));
Gizmos.DrawLine(transform.position+new Vector3(temp.x,temp.y),transform.position+new Vector3(temp.x,0-temp.y));
Gizmos.DrawLine(transform.position+new Vector3(0-temp.x,0-temp.y),transform.position+new Vector3(temp.x,0-temp.y));
Gizmos.DrawLine(transform.position+new Vector3(0-temp.x,0-temp.y),transform.position+new Vector3(0-temp.x,temp.y));
}
}
