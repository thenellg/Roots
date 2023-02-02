using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

public class Player : MonoBehaviour{
public static Player instance;
public ControlScheme controls;
public float walkSpeed;
public float runSpeed;
float speed{get{return controls.Get<Control>("Run")?runSpeed:walkSpeed;}}
public float jumpHeight;
[Tooltip("When the players Y value drops below this, their position will restart")]

public float distanceFromRoot;
public float constrainedDistance;
Vector3 startPos;
public string state;
[Header("Test")]
public PolarVector2 plyrVector;
public PolarVector2 velocityVector;
public PolarVector2 adjustedVector;
#region Reference
Rigidbody2D rb;
#endregion Reference

List<Collider2D> collisions = new List<Collider2D>();
bool touchingGround{get{return collisions.Count>0;}}
public LatchPoint latch;

public void Start(){
instance = this;
rb = GetComponent<Rigidbody2D>();
startPos = transform.position;
}
public void Update(){

switch(state){
case "":
rb.velocity = new Vector2(controls.Get<ControlFloat>("Move")*speed,rb.velocity.y);
if(controls.Get<Control>("Jump").down&&touchingGround)rb.AddForce(new Vector2(0,jumpHeight));
if(controls.Get<Control>("Unlatch").up&&latch!=null)latch.Unlatch();

GameObject plyr = transform.Find("plyr").gameObject;
GameObject velocity = plyr.transform.Find("velocity").gameObject;
GameObject adjVel = plyr.transform.Find("adjusted velocity").gameObject;
GameObject BG = transform.Find("BG").gameObject;
BG.transform.localScale = Vector2.one*(plyrVector.magnitude*2);
plyr.transform.localPosition = plyrVector.rectangular;
velocity.transform.localPosition = velocityVector.rectangular;
adjustedVector = new PolarVector2(plyrVector.degrees+(90*(Mathf.Sign(plyrVector.degrees-velocityVector.degrees)>0?-1:1)),1);
adjVel.transform.localPosition = adjustedVector.rectangular;

//Grab Rope
if(controls.Get<Control>("Hold Rope").down){
constrainedDistance = (transform.position-latch.endRope.position).magnitude;
/*
latch.endRope.GetComponent<Rope>().hold = new GameObject("Hold");
GameObject go = latch.endRope.GetComponent<Rope>().hold;
go.transform.parent = latch.endRope;
go.transform.localPosition = Vector3.zero;
go.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
go.AddComponent<DistanceJoint2D>().connectedBody = rb;
*/
}
if(controls.Get<Control>("Hold Rope")){
if((transform.position-latch.endRope.position).magnitude>constrainedDistance){
transform.position = latch.endRope.position+((transform.position-latch.endRope.position).normalized*constrainedDistance);

}
}
if(controls.Get<Control>("Hold Rope").up){
//Destroy(latch.endRope.GetComponent<Rope>().hold);
//Destroy(latch.endRope.gameObject.GetComponent<DistanceJoint2D>());
//Destroy(latch.endRope.gameObject.GetComponent<Rigidbody2D>());
}
break;

}

}

public void reset(){
rb.velocity = Vector3.zero;
transform.position = startPos;
latch.Unlatch();
}

public void OnCollisionStay2D(Collision2D collision){
if(!collisions.Contains(collision.collider)&&(((Vector2)transform.position)-collision.contacts[0].point).y>.75f){
collisions.Add(collision.collider);
}
}
public void OnCollisionExit2D(Collision2D collision){
if(collisions.Contains(collision.collider))collisions.Remove(collision.collider);
}

}
