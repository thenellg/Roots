using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour{
public List<Sprite> sprite;

void Start(){
GetComponent<SpriteRenderer>().sprite = sprite[Random.Range(0,sprite.Count)];
}
}
