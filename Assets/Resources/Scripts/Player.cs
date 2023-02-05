using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

public class Player : MonoBehaviour{
#region Variables
#region Movement
public float walkSpeed;
public float runSpeed;
public float swingSpeed;
public float airSpeed;
public Vector2 maxSpeed;
public float jumpHeight{get{return JumpHeight*disconnectImpedance;}}
public float JumpHeight;
[Tooltip("An effect that can be set by other scripts to modify player velocity")]
public VelocityEffector velocityEffector;
//The final calculated speed
float speed{get{return (controls.Get<Control>("Run")?runSpeed:walkSpeed)*disconnectImpedance;}}
[Tooltip("Effects the velocity of the player during pulling self up ledge")]
public Vector2Curve ledgeJumpAnimation;
#endregion Movement
#region Rope
public float maxRopeLength{get{return latch.distance;}}
public float currentDisconnectTime;
public float maxDisconnectTime;
[HideInInspector]public float currentRopeLength;
public float disconnectImpedance{get{return ((maxDisconnectTime-currentDisconnectTime)/maxDisconnectTime)+.25f;}}
//Level spawn point
[HideInInspector]public LatchPoint startLatch;
//last point player latched onto
[HideInInspector]public LatchPoint lastLatch;
//current point player is latched onto, is null if player is not latched
public LatchPoint latch;
//Defines if the player is "Grabbing the rope" because they've reach the max distance from latch point
public bool grabRopeByMaxDistance;
#endregion Rope
#region Reference
[HideInInspector]public Rigidbody2D rb;
[HideInInspector]public DistanceJoint2D dj;
[HideInInspector]public SpriteRenderer sr;
    #endregion Reference
#region Animation
    public Animator playerAnim;
    public Transform Sprite;
#endregion Animation
#region Misc
    public static Player instance;
public ControlScheme controls;
//the position of the player on the last frame
Vector3 lastPosition;
//the direction the player is facing (-1 left, 1 right)
public int dirface;
//Determines the behaviour of the player
public string state;
//used to determine if player is touching the ground
List<Collider2D> collisions = new List<Collider2D>();
bool touchingGround{get{return collisions.Count>0;}}
#endregion Misc
#endregion Variables

public void Awake(){ 
instance = this;
}

public void Start(){
//Get references
rb = GetComponent<Rigidbody2D>();
dj = GetComponent<DistanceJoint2D>();
//initialize variable values
dj.enabled = false;
lastPosition = transform.position;
}

public void Update(){
//determine direction player is facing
if(rb.velocity.x!=0)dirface = rb.velocity.x>0?1:-1;
if(latch!=null)latch.endRope.GetComponent<Rope>().Calculate();
//Animation
        if (rb.velocity.x > 0f)
            Sprite.localScale = new Vector3(-Mathf.Abs(Sprite.localScale.x), Sprite.localScale.y, Sprite.localScale.z);
        else if (rb.velocity.x < 0f)
            Sprite.localScale = new Vector3(Mathf.Abs(Sprite.localScale.x), Sprite.localScale.y, Sprite.localScale.z);

        if (rb.velocity.y < 0 && !touchingGround)
            playerAnim.SetTrigger("Fall");
        else if (touchingGround && (rb.velocity.x > 0.01f || rb.velocity.x < -0.01f))
            playerAnim.SetTrigger("Walk");
        else if (touchingGround)
            playerAnim.SetTrigger("notMoving");
        
//State Machine
switch (state){
#region default
case "":
//Input managment(move, jump, detach)
if(touchingGround){
rb.velocity = new Vector2(controls.Get<ControlVector2>("Move").x*speed,rb.velocity.y);
}else{
Vector2 tempV2 = new Vector2(controls.Get<ControlVector2>("Move").x*airSpeed,0);
rb.velocity += new Vector2(
(Mathf.Abs(rb.velocity.x+tempV2.x)<=maxSpeed.x)||(Mathf.Sign(rb.velocity.x)!=Mathf.Sign(tempV2.x))?tempV2.x:0,
(Mathf.Abs(rb.velocity.y+tempV2.y)<=maxSpeed.y)||(Mathf.Sign(rb.velocity.y)!=Mathf.Sign(tempV2.y))?tempV2.y:0
);
}

if(controls.Get<Control>("Jump").down&&touchingGround){
    Invoke("jump", 0.1f);
    playerAnim.SetTrigger("Jump");
}
if (controls.Get<Control>("Unlatch").up&&latch!=null)latch.Unlatch();

#region Ledge Grab
//Check feet
if(Physics2D.Raycast(transform.position+new Vector3(0,-.5f,0),new Vector3(dirface, 0,0),.6f,LayerMask.GetMask("Ground"))){
//Check Hands
if(!Physics2D.Raycast(transform.position+new Vector3(0,.5f,0),new Vector3(dirface,0,0),.6f,LayerMask.GetMask("Ground"))){
state = "Ledge Grab";
rb.gravityScale = 0;
rb.velocity = Vector2.zero; 
playerAnim.SetTrigger("Hang");
}
}
#endregion Ledge Grab
#region Grab Rope
if(controls.Get<Control>("Hold Rope").down){
dj.enabled = true;
state = "holding rope";
}
#endregion Grab Rope

break;
#endregion default
#region holding rope
case "holding rope":
if(controls.Get<Control>("Hold Rope")){
//Acend/Decend rope
dj.distance -= controls.Get<ControlVector2>("Move").y*.2f;
#region Swing or walk
if(touchingGround){
rb.velocity = new Vector2(controls.Get<ControlVector2>("Move").x*speed,rb.velocity.y);
if(controls.Get<Control>("Jump").down&&touchingGround)rb.AddForce(new Vector2(0,jumpHeight));
}else{
rb.velocity += controls.Get<ControlVector2>("Move").x*((Vector2)transform.position-dj.connectedAnchor).normalized.Rotated(90)*swingSpeed;
}
#endregion Swing or walk
}
if(controls.Get<Control>("Hold Rope").up){
dj.enabled = false;
}

//Release Rope
//If player is latched and press unlatch button      OR  player releases grab rope button   OR player was grabbing rope because exceeded max length, and no longer exceeds max length
if((controls.Get<Control>("Unlatch").up&&latch!=null)||controls.Get<Control>("Hold Rope").up||(grabRopeByMaxDistance&&latch.totalLength<maxRopeLength)){
ReleaseRope();
//If unlatch button, then detach
if(controls.Get<Control>("Unlatch").up)latch.Unlatch();
}

#region Ledge Grab
//Check feet
if(Physics2D.Raycast(transform.position+new Vector3(0,-.5f,0),new Vector3(dirface, 0,0),.6f,LayerMask.GetMask("Ground"))){
//Check Hands
if(!Physics2D.Raycast(transform.position+new Vector3(0,.5f,0),new Vector3(dirface,0,0),.6f,LayerMask.GetMask("Ground"))){
ReleaseRope();
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
//Apply Velocity Effector
if(velocityEffector!=null){
velocityEffector.Apply(rb);
}
#region Measure time player travels while disconnected
if(latch==null){
//currentDisconnectDist += (transform.position-lastPosition).magnitude;
currentDisconnectTime += Time.deltaTime;
if(currentDisconnectTime>maxDisconnectTime){
_Reset();
}
}
lastPosition = transform.position;
#endregion Measure distance player travels while disconnected


velocityEffector = null;
}
public void jump()
{
        rb.AddForce(new Vector2(0, jumpHeight));
}

void OnDrawGizmos(){
dj = GetComponent<DistanceJoint2D>();
Gizmos.DrawRay(dj.connectedAnchor,((Vector2)transform.position-dj.connectedAnchor).normalized*dj.distance);
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
//Full Reset Resets the level, _Reset just resets to last checkpoint
rb.velocity = Vector3.zero;
transform.position = startLatch.transform.position;
if(latch!=null)latch.Unlatch();
startLatch.Latch();
}
public void _Reset(){
GetComponent<MotionRecorder>().Record = false;
//Full Reset Resets the level, _Reset just resets to last checkpoint
rb.velocity = Vector3.zero;
transform.position = lastLatch.transform.position;
if(latch!=null)latch.Unlatch();
lastLatch.Latch();
}
#region Get touchingGround
public void OnCollisionStay2D(Collision2D collision){
if(!collisions.Contains(collision.collider)&&(((Vector2)transform.position)-collision.contacts[0].point).y>.75f&&collision.collider.gameObject.layer==LayerMask.NameToLayer("Ground")){
collisions.Add(collision.collider);
}
}
public void OnCollisionExit2D(Collision2D collision){
if(collisions.Contains(collision.collider))collisions.Remove(collision.collider);
}
#endregion Get touchingGround

}
