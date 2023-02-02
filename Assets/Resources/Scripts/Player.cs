using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

//Grab Rope
if(controls.Get<Control>("Hold Rope").down){
constrainedDistance = (transform.position-latch.endRope.position).magnitude;
}
if(controls.Get<Control>("Hold Rope")){
latch.endRope.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
latch.endRope.gameObject.AddComponent<DistanceJoint2D>().connectedBody = rb;
}
if(controls.Get<Control>("Hold Rope").up){
Destroy(latch.endRope.gameObject.GetComponent<DistanceJoint2D>());
Destroy(latch.endRope.gameObject.GetComponent<Rigidbody2D>());
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
