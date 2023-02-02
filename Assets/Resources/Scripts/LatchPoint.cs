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
public float totalLength{
get{
if(rope!=null){
return rope.GetComponent<Rope>().totalLength;
}
return 0;
}
}
void Start(){
if(isSpawnPoint){
Player.instance.transform.position = transform.position;
Player.instance.startLatch = this;
Latch();
}
}
void OnTriggerEnter2D(Collider2D collision){
if(collision.gameObject==Player.instance.gameObject&&Player.instance.latch==null){
Latch();
}
}
public void Latch(){
rope = Instantiate(Resources.Load<GameObject>("Prefabs/Root"));
rope.transform.parent = transform;
rope.transform.localPosition = Vector3.zero;
rope.GetComponent<Rope>().Connection = Player.instance.transform;
Player.instance.latch = this;
Player.instance.lastLatch = this;
Player.instance.currentDisconnectDist = 0;
}
public void Unlatch(){
Player.instance.latch = null;
rope.GetComponent<Rope>().Destroy();
}
}
