using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
public Transform player;
public float speed;
public GameObject background;
void Start(){
transform.parent = null;
//GameObject newBackground = Instantiate(background);
//newBackground.transform.parent = transform;
//newBackground.transform.localPosition = new Vector3();
}
void Update(){
transform.position += (player.position-transform.position).normalized*speed;
if((player.position-transform.position).magnitude<speed)transform.position = player.position;
transform.position = new Vector3(transform.position.x,transform.position.y,-10);
}
}
