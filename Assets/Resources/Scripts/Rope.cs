using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour{
public Transform Base;
public Transform Connection;
public List<Sprite> sprites;
public float segmentLength;
public float destroyTimer = -1;

public Transform endRope{
get{
Rope rope;
if(Connection.TryGetComponent(out rope)){
return rope.endRope;
}
if(Connection.name=="Player"){
return transform;
}
return null;
}
}

public List<GameObject> segments = new List<GameObject>();
void Start(){
Base = transform.parent;
}
void Update(){
Vector2 PtB;
#region Destroy Rope
if(Connection==null){
//Timer
if(destroyTimer<=0)destroyTimer = segments.Count;
destroyTimer -= .1f;

//Remove Segments one by one
while(segments.Count>destroyTimer){
Destroy(segments[segments.Count-1]);
segments.RemoveAt(segments.Count-1);
}

//if no segments left, destroy rope
if(segments.Count<1){
Rope rope;
if(Base.TryGetComponent(out rope))rope.Destroy();
Destroy(gameObject);
}

}else{
#endregion Destroy Rope
PtB = Connection.position-transform.position;
int num = Mathf.RoundToInt(PtB.magnitude/segmentLength);

//Add Segments as player walks away
while(segments.Count<num){
GameObject go = new GameObject("RopeSegment");
go.transform.parent = transform;
SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
sr.sprite = sprites[0];
segments.Add(go);
}

//Remove Segments as player gets closer
while(segments.Count>num){
Destroy(segments[segments.Count-1]);
segments.RemoveAt(segments.Count-1);
}

//Reposition/Rotate Segments
for(int i=0; i<segments.Count;i++){
segments[i].transform.localPosition = PtB.normalized*((i+.5f)*segmentLength);
segments[i].transform.localEulerAngles = new Vector3(0,0,Mathf.Atan2(PtB.y,PtB.x)*Mathf.Rad2Deg);
}

//If this rope is player end
if(Connection.name=="Player"){
PtB = Connection.position-transform.position;
//Check if something's interrupting rope
RaycastHit2D hit = Physics2D.Raycast(transform.position+(Vector3)PtB.normalized, PtB,PtB.magnitude);
if(hit.collider!=null){
if(!(hit.collider.name=="Player"||hit.collider.name=="NoWallStick")){
GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Root"));
go.transform.position = hit.point;
go.transform.parent=transform;
go.GetComponent<Rope>().Connection = Connection;
Connection = go.transform;
}
}

//Check if something's interrupting parent rope
if(Base.GetComponent<Rope>()!=null){
hit = Physics2D.Raycast(Base.transform.position+(Player.instance.transform.position-Base.transform.position).normalized, Player.instance.transform.position-Base.transform.position);
if(hit.collider!=null){
if(hit.collider.name=="Player"||hit.collider.name=="NoWallStick"){
Base.GetComponent<Rope>().Connection = Connection;
Destroy(gameObject);
}
}
}

}

}


}

public void Destroy(){
Rope rope;
if(Connection==null){
Connection = null;
}else{
if(Connection.TryGetComponent(out rope)){
rope.Destroy();
}else{
Connection = null;
}
}

}

}
