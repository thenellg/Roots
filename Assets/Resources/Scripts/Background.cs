using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour{
public GameObject[] Hills;
public float hillsWidth;
public float cloudSpawnTime;
float cloudTimer;
void Start(){
Hills = new GameObject[5];
Hills[0] = transform.Find("Hills0").gameObject;
Hills[1] = transform.Find("Hills1").gameObject;
Hills[2] = transform.Find("Hills2").gameObject;
Hills[3] = transform.Find("Hills3").gameObject;
Hills[4] = transform.Find("Hills4").gameObject;
}
void Update(){
int hillOffset = (int)((Player.instance.transform.position.x-transform.position.x)/hillsWidth);
Hills[0].transform.position = new Vector3(hillsWidth*(hillOffset-2),Hills[0].transform.position.y,Hills[0].transform.position.z);
Hills[1].transform.position = new Vector3(hillsWidth*(hillOffset-1),Hills[0].transform.position.y,Hills[0].transform.position.z);
Hills[2].transform.position = new Vector3(hillsWidth*(hillOffset),Hills[0].transform.position.y,Hills[0].transform.position.z);
Hills[3].transform.position = new Vector3(hillsWidth*(hillOffset+1),Hills[0].transform.position.y,Hills[0].transform.position.z);
Hills[4].transform.position = new Vector3(hillsWidth*(hillOffset+2),Hills[0].transform.position.y,Hills[0].transform.position.z);
cloudTimer -= Time.deltaTime;
if(cloudTimer<0){
cloudTimer = cloudSpawnTime;
CreateCloud();
}

}

void CreateCloud(){
GameObject cloud = Instantiate(Resources.Load<GameObject>("Prefabs/Cloud"));
int dir = (Random.value>0.5f)?1:-1;
cloud.transform.SetParent(transform);
cloud.transform.position = new Vector3(Player.instance.transform.position.x+(-15*dir),Random.Range(1.5f,6));
cloud.GetComponent<Cloud>().direction = dir;
}
}