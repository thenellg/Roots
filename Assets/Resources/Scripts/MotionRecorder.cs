using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionRecorder : MonoBehaviour{
public bool Record;
bool RecordLast;
public List<bool> runs;
List<List<Vector2>> position;
int runIndex;
public List<Color> colors;
public int colorVariation = 3;
void Start(){
runs = new List<bool>();
position = new List<List<Vector2>>();
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
runs.Add(false);
position.Add(new List<Vector2>());
runIndex++;
}
position[runIndex].Add(transform.position);
}
RecordLast = Record;
}

void OnDrawGizmos(){
for(int i=0; i<runs.Count;i++){
if(runs[i]){

for(int j=1; j<position[i].Count-1;j++){
Gizmos.color = colors[i];
Gizmos.DrawLine(position[i][j-1],position[i][j]);
}
}
}
}

}
