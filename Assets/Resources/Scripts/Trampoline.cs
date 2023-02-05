using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour{
public float Strength;

void OnCollisionEnter2D(Collision2D collision){
Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
if(rb != null){
Vector2 angle = new Vector2(Mathf.Cos((transform.eulerAngles.z+90)*Mathf.Deg2Rad),Mathf.Sin((transform.eulerAngles.z+90)*Mathf.Deg2Rad));
rb.velocity = angle*Strength;
GetComponent<AudioManager>().PlayRandom();
GetComponent<SpriteManager>().StartCoroutine(GetComponent<SpriteManager>().PlayAnimation(0,10));
}
}

void OnDrawGizmos(){
Vector2 angle = new Vector2(Mathf.Cos((transform.eulerAngles.z+90)*Mathf.Deg2Rad),Mathf.Sin((transform.eulerAngles.z+90)*Mathf.Deg2Rad));
Gizmos.color = Color.yellow;
Gizmos.DrawRay(transform.position,angle*(Strength/2));
}
}
