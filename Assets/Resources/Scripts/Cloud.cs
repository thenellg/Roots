using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour{
public int direction;
public float speed;
public List<Sprite> sprites;

void Start(){
Sprite sprite = sprites[Mathf.RoundToInt(Random.Range(0,sprites.Count-1))];
GetComponent<Image>().sprite = sprite;
GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width/100,sprite.rect.height/100);
speed = Random.Range(.003f,.015f);
}

void Update(){
transform.position += new Vector3(direction*speed,0,0);
if((transform.position-Player.instance.transform.position).magnitude>20)Destroy(gameObject);
}

}
