using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

public class Player : MonoBehaviour{
public static Player instance;
public ControlScheme controls;
public float walkSpeed;
public float runSpeed;
public float swingSpeed;
public VelocityEffector velocityEffector;
public Vector2 testVector;
public Vector2Curve ledgeJumpAnimation;
float speed{get{return (controls.Get<Control>("Run")?runSpeed:walkSpeed)*disconnectImpedance;}}
public float jumpHeight{get{return JumpHeight*disconnectImpedance;}}
public float JumpHeight;
[Tooltip("When the players Y value drops below this, their position will restart")]

public float maxRopeLength;
public float currentDisconnectDist;
public float maxDisconnectDist;
public float disconnectImpedance{get{return ((maxDisconnectDist-currentDisconnectDist)/maxDisconnectDist)+.25f;}}
Vector3 lastPosition;
public int dirface;
[HideInInspector]public LatchPoint startLatch;
[HideInInspector]public LatchPoint lastLatch;
public string state;
#region Reference
[HideInInspector]public Rigidbody2D rb;
[HideInInspector]public DistanceJoint2D dj;
#endregion Reference

List<Collider2D> collisions = new List<Collider2D>();
public bool grabRopeByMaxDistance;
bool touchingGround{get{return collisions.Count>0;}}
public LatchPoint latch;

public void Start(){
instance = this;
rb = GetComponent<Rigidbody2D>();
dj = GetComponent<DistanceJoint2D>();
dj.enabled = false;
lastPosition = transform.position;
}

public void Update(){
if(rb.velocity.x!=0)dirface = rb.velocity.x>0?1:-1;
switch(state){
#region default
case "":
rb.velocity = new Vector2(controls.Get<ControlVector2>("Move").x*speed,rb.velocity.y);
if(controls.Get<Control>("Jump").down&&touchingGround)rb.AddForce(new Vector2(0,jumpHeight));
if(controls.Get<Control>("Unlatch").up&&latch!=null)latch.Unlatch();

#region Ledge Grab
//Check feet
if(Physics2D.Raycast(transform.position+new Vector3(0,-.5f,0),new Vector3(dirface, 0,0),.6f,LayerMask.GetMask("Ground"))){
//Check Hands
if(!Physics2D.Raycast(transform.position+new Vector3(0,.5f,0),new Vector3(dirface,0,0),.6f,LayerMask.GetMask("Ground"))){
state = "Ledge Grab";
rb.gravityScale = 0;
rb.velocity = Vector2.zero;
}
}
#endregion Ledge Grab

//Grab Rope
if(controls.Get<Control>("Hold Rope").down){
dj.distance = (transform.position-latch.endRope.position).magnitude;
dj.connectedAnchor = latch.endRope.position;
dj.enabled = true;
state = "holding rope";
}

break;
#endregion default
#region holding rope
case "holding rope":
//Swing or walk
if(touchingGround){
rb.velocity = new Vector2(controls.Get<ControlVector2>("Move").x*speed,rb.velocity.y);
if(controls.Get<Control>("Jump").down&&touchingGround)rb.AddForce(new Vector2(0,jumpHeight));
}else{
rb.velocity += controls.Get<ControlVector2>("Move").x*((Vector2)transform.position-dj.connectedAnchor).normalized.Rotated(90)*swingSpeed;
}
//Acend/Decend rope
dj.distance -= controls.Get<ControlVector2>("Move").y*.01f;

//Release Rope
if((controls.Get<Control>("Unlatch").up&&latch!=null)||controls.Get<Control>("Hold Rope").up||(grabRopeByMaxDistance&&latch.totalLength<maxRopeLength)){
ReleaseRope();
if(controls.Get<Control>("Unlatch").up)latch.Unlatch();
}
#region Ledge Grab
//Check feet
if(Physics2D.Raycast(transform.position+new Vector3(0,-.5f,0),new Vector3(dirface, 0,0),.6f,LayerMask.GetMask("Ground"))){
//Check Hands
if(!Physics2D.Raycast(transform.position+new Vector3(0,.5f,0),new Vector3(dirface,0,0),.6f,LayerMask.GetMask("Ground"))){
state = "Ledge Grab";
rb.gravityScale = 0;
rb.velocity = Vector2.zero;
}
}
#endregion Ledge Grab
break;
#endregion holding rope
#region ledgeGrab
case "Ledge Grab":
if(controls.Get<Control>("Jump")||controls.Get<ControlVector2>("Move").y.up>0){ 
StartCoroutine(LedgeJump(.1f));
}
if(controls.Get<Control>("Crouch")||controls.Get<ControlVector2>("Move").x.up==dirface*-1||controls.Get<ControlVector2>("Move").y.up<0){
state = "";
rb.gravityScale = 1;
}
break;
#endregion ledgeGrab
}
if(velocityEffector!=null)velocityEffector.Apply(rb);
velocityEffector = null;
//Measure distance player travels while disconnected
if(latch==null){
currentDisconnectDist += (transform.position-lastPosition).magnitude;
if(currentDisconnectDist>maxDisconnectDist){
_Reset();
}
}
lastPosition = transform.position;
}

void OnDrawGizmos(){

Gizmos.DrawRay(transform.position+new Vector3(0,-.5f,0),new Vector3(dirface,0,0));
Gizmos.DrawRay(transform.position+new Vector3(0,.5f,0),new Vector3(dirface,0,0));

}
void ReleaseRope(){ 
dj.enabled = false;
state = "";
}
IEnumerator LedgeJump(float speed){
float time = 0;
while(time<ledgeJumpAnimation.time){
rb.velocity = ledgeJumpAnimation.Evaluate(time).Multiply(new Vector2(dirface,1));
time += speed;
yield return null;
}
state = "";
rb.gravityScale = 1;
}
public void FullReset(){
rb.velocity = Vector3.zero;
transform.position = startLatch.transform.position;
if(latch!=null)latch.Unlatch();
startLatch.Latch();
}
public void _Reset(){
rb.velocity = Vector3.zero;
transform.position = lastLatch.transform.position;
if(latch!=null)latch.Unlatch();
lastLatch.Latch();
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
