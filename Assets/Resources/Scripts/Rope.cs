using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

public class Rope : MonoBehaviour{
public Transform Base;
public Transform Connection;
public List<Sprite> sprites;
public GameObject hold;
public float segmentLength;
public Vector2 direction{get{return (Player.instance.transform.position-transform.position).normalized;}}

public float length{get{return (Base.position-Connection.position).magnitude;}}
[HideInInspector]public float totalLengthCalculator;
public float totalLength{
get{
Rope rope;
if(Connection.TryGetComponent(out rope)){
rope.totalLengthCalculator = totalLengthCalculator+length;
return rope.totalLength;
}
if(Connection.name=="Player"){
return totalLengthCalculator+length;
}
return 0;
}
}
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
if(segments[segments.Count-1]!=null){
Destroy(segments[segments.Count-1]);
segments.RemoveAt(segments.Count-1);
}
}

//if no segments left, destroy rope
if(segments.Count<1){
Rope rope;
if(Base.TryGetComponent(out rope))rope.Destroy();
Destroy(gameObject);
}

}else{
#endregion Destroy Rope
//Get line between rope base and rope end
PtB = Connection.position-transform.position;
int num = Mathf.RoundToInt(PtB.magnitude/segmentLength);

#region Rope Segments
//Add Segments as player walks away
bool rootsGrowing = false;
while(segments.Count<num){
rootsGrowing = true;
if(!Player.instance.latch.SFXplayer.isPlaying){
if(Player.instance.latch.SFXplayer.clip!=Player.instance.latch.snd_RootsGrowing)Player.instance.latch.SFXplayer.clip = Player.instance.latch.snd_RootsGrowing;
Player.instance.latch.SFXplayer.Play();
}
GameObject go = new GameObject("RopeSegment");
go.transform.parent = transform;
SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
sr.sprite = sprites[(segments.Count)%(sprites.Count)];
sr.sortingLayerName = "Player";
sr.sortingOrder = 2;
segments.Add(go);
go.transform.localScale = Vector3.one*.05f;
//Rope Max Distance
if(Player.instance.latch.totalLength>Player.instance.maxRopeLength){

}
}
//Stop Sound Effect
if(Player.instance.latch!=null){
if(!rootsGrowing&&Player.instance.latch.SFXplayer.isPlaying)Player.instance.latch.SFXplayer.Pause();
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
#endregion Rope Segments

#region this rope is player end
if(Connection.name=="Player"){
PtB = Connection.position-transform.position;

//Check if something's interrupting rope
//Add New Rope
RaycastHit2D hit = Physics2D.Raycast(transform.position+(Vector3)PtB.normalized, PtB,PtB.magnitude,LayerMask.GetMask("Ground"));
if(hit.collider!=null){
if(!(hit.collider.name=="Player"||hit.collider.name=="NoWallStick")){
GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Root"));
go.name += Time.time;
go.transform.position = hit.point;
go.transform.parent=transform;
go.GetComponent<Rope>().Connection = Connection;
Connection = go.transform;

}
}

//Check if something's interrupting parent rope
//Remove End Rope
if(Base.GetComponent<Rope>()!=null){
hit = Physics2D.Raycast(Base.transform.position+(Player.instance.transform.position-Base.transform.position).normalized, Player.instance.transform.position-Base.transform.position,LayerMask.GetMask("Ground"));
if(hit.collider!=null){
if(hit.collider.name=="Player"||hit.collider.name=="NoWallStick"){
//Player is holding rope
Transform hold = transform.Find("Hold");
if(hold!=null){
hold.parent = Base.transform;
hold.localPosition = Vector3.zero;
}
//Pass Distance joint off to next up the chain
Base.GetComponent<Rope>().Connection = Connection;
Destroy(gameObject);

}
}
}

}
#endregion this rope is player end
}


}
void OnDrawGizmos(){ 
}
public void Calculate(){
if(Connection.name=="Player"){
Player.instance.dj.connectedAnchor = transform.position;
Player.instance.dj.distance = (Player.instance.transform.position-transform.position).magnitude;

float tempFloat = Player.instance.latch.distance-Player.instance.latch.totalLength;
if(tempFloat<0){
Player.instance.grabRopeByMaxDistance = true;
Player.instance.dj.enabled = true;
}else{ 
if(Player.instance.grabRopeByMaxDistance){
Player.instance.grabRopeByMaxDistance = false;
Player.instance.dj.enabled = false;
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
