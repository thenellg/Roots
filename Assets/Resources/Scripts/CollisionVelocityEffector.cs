using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

public class CollisionVelocityEffector : MonoBehaviour{
public enum Type{
OnCollision,
WhileColliding,
OnTrigger,
WhileTriggering
}
public Type CollisionType;
public VelocityEffector effects;
public List<GameObject> passengers;
public List<AudioClip> sounds;
void Update(){
if(CollisionType==Type.WhileColliding||CollisionType==Type.WhileTriggering){
foreach(GameObject go in passengers){
effects.Apply(go.GetComponent<Rigidbody2D>());
if(GetComponent<AudioSource>()!=null){
GetComponent<AudioSource>().clip = sounds[Random.Range(0,sounds.Count)];
GetComponent<AudioSource>().Play();
}
if(go==Player.instance.gameObject)Player.instance.velocityEffector = effects;
}
}
}
void OnTriggerEnter2D(Collider2D collision){
if(CollisionType==Type.OnTrigger||CollisionType==Type.WhileTriggering){
Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
if(!passengers.Contains(collision.gameObject))if(collision.gameObject.GetComponent<Rigidbody2D>())passengers.Add(collision.gameObject);
if(CollisionType==Type.OnTrigger){
effects.Apply(collision.GetComponent<Rigidbody2D>());
if(GetComponent<AudioSource>()!=null){
GetComponent<AudioSource>().clip = sounds[Random.Range(0,sounds.Count)];
GetComponent<AudioSource>().Play();
}
}

}
}
void OnTriggerExit2D(Collider2D collision){
if(CollisionType==Type.OnTrigger||CollisionType==Type.WhileTriggering){
if(passengers.Contains(collision.gameObject))passengers.Remove(collision.gameObject);
}
}
void OnCollisionEnter2D(Collision2D collision){
if(CollisionType==Type.OnCollision||CollisionType==Type.WhileColliding){

//Check to see if passenger is on top
RaycastHit2D[] results = Physics2D.RaycastAll(transform.position+new Vector3(.1f-transform.localScale.x/2,.1f+transform.localScale.y/2,0),new Vector2(1,0), transform.localScale.x*.8f);
bool isOnTop = false;
foreach(RaycastHit2D hit in results){
if(hit.rigidbody==collision.rigidbody)isOnTop = true;
}

if(isOnTop){
if(!passengers.Contains(collision.gameObject))passengers.Add(collision.gameObject);
if(CollisionType==Type.OnCollision){
effects.Apply(collision.rigidbody);
if(GetComponent<AudioSource>()!=null){
GetComponent<AudioSource>().clip = sounds[Random.Range(0,sounds.Count)];
GetComponent<AudioSource>().Play();
}
}

}

}
}
void OnCollisionExit2D(Collision2D collision){
if(CollisionType==Type.OnCollision||CollisionType==Type.WhileColliding){
if(passengers.Contains(collision.gameObject))passengers.Remove(collision.gameObject);
}
}

}
[System.Serializable]public class VelocityEffector{
public Linked<Vector2,bool> multiplier;
public Linked<Vector2,bool> adder;
public Linked<Linked<float,bool>,Linked<float,bool>> setter;
public Linked<Vector2,bool> max;
public Linked<Vector2,bool> min;

public void Apply(Rigidbody2D rb){
//Apply Effects
if(multiplier.linkedValue)rb.velocity = rb.velocity.Multiply(multiplier.value);
if(adder.linkedValue)rb.velocity += adder.value;
rb.velocity = new Vector2(setter.value.linkedValue?setter.value.value:rb.velocity.x,setter.linkedValue.linkedValue?setter.linkedValue.value:rb.velocity.y);

//Clamp Maximums
if(max.linkedValue){
rb.velocity = new Vector2(rb.velocity.x>max.value.x?max.value.x:rb.velocity.x,rb.velocity.y);
rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y>max.value.y?max.value.y:rb.velocity.y);
}

//Clamp Minimums
if(min.linkedValue){
rb.velocity = new Vector2(rb.velocity.x<min.value.x?min.value.x:rb.velocity.x,rb.velocity.y);
rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y<min.value.y?min.value.y:rb.velocity.y);
}
}
}
