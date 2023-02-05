using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionRecorder : MonoBehaviour{
public bool Record;
bool RecordLast;
public List<Recording> recordings;
int runIndex;
public List<Color> colors;
public int colorVariation = 3;
void Start(){
recordings = new List<Recording>();
runIndex = -1;

for(int r=0; r<colorVariation;r++){
for(int b=0; b<colorVariation;b++){
for(int g=0; g<colorVariation;g++){
colors.Add(new Color(r*(1f/colorVariation),b*(1f/colorVariation),g*(1f/colorVariation)));
}
}
}

}
void Update(){
if(Input.GetKeyUp(KeyCode.R))Record = !Record;
if(Record){
if(!RecordLast){
recordings.Add(new Recording());
runIndex++;
}
recordings[runIndex].position.Add(transform.position);
}
RecordLast = Record;
}

void OnDrawGizmos(){
for(int i=0; i<recordings.Count;i++){
if(recordings[i].view){

for(int j=1; j<recordings[i].position.Count-1;j++){
Gizmos.color = colors[i];
Gizmos.DrawLine(recordings[i].position[j-1],recordings[i].position[j]);
}
}
}
}

[System.Serializable]public class Recording{ 
public bool view = true;
public List<Vector2> position = new List<Vector2>();
}
}
