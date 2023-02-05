using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestsFunctions;

public class SpriteManager : MonoBehaviour{
public List<Sprite> sprites = new List<Sprite>();
public List<ListWrapper<Sprite>> animations;
float frame;
public IEnumerator PlayAnimation(int animation,float speed){
frame = 0;
while(frame<animations[animation].Count-1){
GetComponent<SpriteRenderer>().sprite = animations[animation][Mathf.RoundToInt(frame)];
frame += speed*Time.deltaTime;
yield return null;
}
GetComponent<SpriteRenderer>().sprite = animations[animation][0];
}
public void SetRandom(){
GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Count)];
}
}
