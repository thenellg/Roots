using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatchPoint : MonoBehaviour{
GameObject rope;
public bool isSpawnPoint;

public Transform endRope{
get{
if(rope!=null){
return rope.GetComponent<Rope>().endRope;
}
return null;
}
}
void Start(){
if(isSpawnPoint){
Player.instance.transform.position = transform.position;
Latch();
}
}
void OnTriggerEnter2D(Collider2D collision){
if(collision.gameObject==Player.instance.gameObject&&Player.instance.latch==null){
Latch();
}
}
void Latch(){
rope = Instantiate(Resources.Load<GameObject>("Prefabs/Root"));
rope.transform.parent = transform;
rope.transform.localPosition = Vector3.zero;
rope.GetComponent<Rope>().Connection = Player.instance.transform;
Player.instance.latch = this;
}
public void Unlatch(){
Player.instance.latch = null;
rope.GetComponent<Rope>().Destroy();
}
}
