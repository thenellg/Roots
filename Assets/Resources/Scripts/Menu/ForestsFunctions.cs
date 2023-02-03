using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ForestsFunctions{
public static class FF{
//string dpName = "";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
//string dpName = "";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
//string dpName = "";if(!FF.VariableDependancies.Contains(dpName))FF.VariableDependancies.Add(dpName);
public static List<string> ClassDependancies = new List<string>();
public static List<string> FunctionDependancies = new List<string>();
public static List<string> VariableDependancies = new List<string>();
public static string dependancies{get{
string rtn = "Classes:\n";
foreach(string str in ClassDependancies){rtn+=str+'\n';}
rtn += "Functions:\n";
foreach(string str in FunctionDependancies){rtn+=str+'\n';}
return rtn;
}}
static char[] _alphabet = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
public static char[] alphabet{get{
string dpName = "FF.alphabet";if(!FF.VariableDependancies.Contains(dpName))FF.VariableDependancies.Add(dpName);
return _alphabet;
}}
public static int Int_Largest = 2147483647;
public static Vector2Int Vector2_Largest = new Vector2Int(2147483647,2147483647);
public static Vector3Int Vector3_Largest = new Vector3Int(2147483647,2147483647);
#region GUI
public static float GUI_LineHeight = 16;
public static float GUI_ButtonHeight = 24;
public static float GUI_FoldoutIndent = 18;
#endregion GUI
public static Vector2 ScreenSize{get{return new Vector2(Screen.width,Screen.height);}}
public static Vector2 mousePositionRatio{get{return (Input.mousePosition.Strip('z').Divide(ScreenSize));}}
public static Vector2 MouseAxis{get{return new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));}}
public static Vector2 RandomRangeVector2(Vector2 min,Vector2 max){
return new Vector2(Random.Range(min.x,max.x),Random.Range(min.y,max.y));
}
public static Vector2Int RandomRangeVector2Int(Vector2Int min,Vector2Int max){
return new Vector2Int(Random.Range(min.x,max.x),Random.Range(min.y,max.y));
}
public static Vector3 RandomRangeVector3(Vector3 min,Vector3 max){
return new Vector3(Random.Range(min.x,max.x),Random.Range(min.y,max.y),Random.Range(min.z,max.z));
}
public static Vector3Int RandomRangeVector3Int(Vector3Int min,Vector3Int max){
return new Vector3Int(Random.Range(min.x,max.x),Random.Range(min.y,max.y),Random.Range(min.z,max.z));
}
public class PlayerGizmo : MonoBehaviour{
//On Camera, deselect PlayerGizmo from culling mask
//Create a new camera parented to current camera
/*Remove Audio Listener
*near clipping plane = 0
*clear flags depth only
*culling mask PlayerGizmo only
*Depth = 1
*/
GameObject _gameobject{get{return gameObject;}}
public delegate void PlayerGizmoEvent(PlayerGizmo gizmo);
public event PlayerGizmoEvent gizmoStart;
public event PlayerGizmoEvent gizmoUpdate;
public new TransformCurve animation;

public enum Type{
Text
}
Type type;
public float startTime;
public float showTime;
public float totalShowTime;
public Color color;
public List<Named<object>> miscData;
void Start(){
startTime = Time.time;
showTime = totalShowTime;
if(gizmoStart!=null)gizmoStart(this);
}
void Update(){
if(showTime>0){
showTime -= Time.deltaTime;
switch(type){
#region Type
case Type.Text:
TMPro.TextMeshProUGUI TMP = _gameobject.GetComponent<TMPro.TextMeshProUGUI>();
TMP.alpha = showTime/totalShowTime;
TMP.color = color;
break;
#endregion Type
}
if(showTime<0){
Destroy();
}
}
if(gizmoUpdate!=null)gizmoUpdate(this);
}
public void Destroy(){ 
GameObject.Destroy(_gameobject);
}
public static PlayerGizmo Create(Type type,Vector3 Position,float showTime=-1,string text="",FontData fontData=null){
GameObject go = new GameObject("Gizmo");
go.layer = LayerMask.NameToLayer("PlayerGizmo");
go.transform.position = Position;
go.AddComponent<Canvas>();
PlayerGizmo script = go.AddComponent<PlayerGizmo>();
FontData fd = fontData;
if(fontData==null){
fd = new FontData(12,Color.black);
}
switch(type){
#region Type
case Type.Text:
TMPro.TextMeshProUGUI TMP = go.AddComponent<TMPro.TextMeshProUGUI>();
TMP.text = text;
TMP.setFont(fd);
script.showTime = showTime;
break;
#endregion Type
}
return script;
}

}
#region Serialize
public static float[] Serialize(Vector2 value){return new float[]{value[0],value[1]};}
public static float[] Serialize(Vector3 value){return new float[]{value[0],value[1],value[2]};}
public static float[] Serialize(Vector4 value){return new float[]{value[0],value[1],value[2],value[3]};}
public static float[] Serialize(Color value){return new float[]{value[0],value[1],value[2],value[3]};}
#endregion Serialize
//public void RunOnce(delegate function,out bool Bool){}
}
public static class Extensions{
public static void PropertyFoldout(this Editor This,ref bool Bool,string Label,params string[] properties){
Bool = EditorGUILayout.Foldout(Bool,Label);
if(Bool){
EditorGUI.indentLevel++;
foreach(string property in properties){
EditorGUILayout.PropertyField(This.serializedObject.FindProperty(property));
}
EditorGUI.indentLevel--;
}
}
#region Vectors
#region Vector2
public static Vector2 Abs(this Vector2 V2){
string dpName = "Vector2.Abs";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2(Mathf.Abs(V2.x),Mathf.Abs(V2.y));
}
public static Vector2Int Abs(this Vector2Int V2){
string dpName = "Vector2Int.Abs";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2Int(Mathf.Abs(V2.x),Mathf.Abs(V2.y));
}
public static Vector2Int Multiply(this Vector2Int This,Vector2Int V2){
string dpName = "Vector2Int.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2Int(This.x*V2.x,This.y*V2.y);
}
public static Vector2 Multiply(this Vector2Int This,Vector2 V2){
string dpName = "Vector2Int.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2(This.x*V2.x,This.y*V2.y);
}
public static Vector2 Multiply(this Vector2 This,Vector2Int V2){
string dpName = "Vector2Int.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2(This.x*V2.x,This.y*V2.y);
}
public static Vector2 Multiply(this Vector2 This,Vector2 V2){
string dpName = "Vector2Int.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2(This.x*V2.x,This.y*V2.y);
}
public static Vector2 Divide(this Vector2 This,Vector2 V2){
string dpName = "Vector2Int.Divide";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2(This.x/V2.x,This.y/V2.y);
}
public static Vector2Int FloorToInt(this Vector2 V3){
string dpName = "Vector2.FloorToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2Int(Mathf.FloorToInt(V3.x),Mathf.FloorToInt(V3.y));
}
public static Vector2Int CeilToInt(this Vector2 V3){
string dpName = "Vector2.CeilToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2Int(Mathf.CeilToInt(V3.x),Mathf.CeilToInt(V3.y));
}
public static Vector2Int RoundToInt(this Vector2 V3){
string dpName = "Vector2.RoundToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector2Int(Mathf.RoundToInt(V3.x),Mathf.RoundToInt(V3.y));
}
public static Vector2 Clamp(this Vector2 V2,Vector2 min,Vector2 max){
string dpName = "Vector2.Clamp";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(Mathf.Clamp(V2.x,min.x,max.x),Mathf.Clamp(V2.y,min.y,max.y));
}
public static Vector3 To3(this Vector2 This,string blankDimension="z",float fillDimension=0){
string dpName = "Vector2.To3";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
switch(blankDimension.ToLower()){
case "x":return new Vector3(fillDimension,This.y,This.y);
case "y":return new Vector3(This.x,fillDimension,This.y);
case "z":return new Vector3(This.x,This.y,fillDimension);
}
return new Vector3(This.x,This.y,fillDimension);
}
public static Vector3 To3(this Vector2 This,char blankDimension,float fillDimension=0){
string dpName = "Vector2.To3";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
string BD = (""+blankDimension).ToLower();
switch(BD){
case "x":return new Vector3(fillDimension,This.y,This.y);
case "y":return new Vector3(This.x,fillDimension,This.y);
case "z":return new Vector3(This.x,This.y,fillDimension);
}
return new Vector3(This.x,This.y,fillDimension);
}
public static Vector3Int To3(this Vector2Int This,string blankDimension="z",int fillDimension=0){
string dpName = "Vector2Int.To3";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
switch(blankDimension.ToLower()){
case "x":return new Vector3Int(fillDimension,This.y,This.y);
case "y":return new Vector3Int(This.x,fillDimension,This.y);
case "z":return new Vector3Int(This.x,This.y,fillDimension);
}
return new Vector3Int(This.x,This.y,0);
}
public static Vector3Int To3(this Vector2Int This,char blankDimension,int fillDimension=0){
string dpName = "Vector2Int.To3";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
string BD = (""+blankDimension).ToLower();
switch(BD){
case "x":return new Vector3Int(fillDimension,This.y,This.y);
case "y":return new Vector3Int(This.x,fillDimension,This.y);
case "z":return new Vector3Int(This.x,This.y,fillDimension);
}
return new Vector3Int(This.x,This.y,0);
}
#endregion Vector2
#region Vector3
public static Vector2 Strip(this Vector3 v3,char dim){
string dpName = "Vector3.Strip";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
switch(dim){
case 'x':return new Vector2(v3.y,v3.z);
case 'y':return new Vector2(v3.x,v3.z);
case 'z':return new Vector2(v3.x,v3.y);
}
Debug.Log(dim+" is not a dimension of Vector3");
return Vector2.zero;
}
public static Vector3 Divide(this Vector3 V3,Vector3 denominator){
string dpName = "Vector3.Divide";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(V3.x/denominator.x,V3.y/denominator.y,V3.z/denominator.z);
}
public static Vector3Int Divide(this Vector3Int V3,Vector3Int denominator){
string dpName = "Vector3Int.Divide";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(V3.x/denominator.x,V3.y/denominator.y,V3.z/denominator.z);
}
public static Vector3Int Divide(this Vector3Int V3,Vector3 denominator){
string dpName = "Vector3Int.Divide";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.RoundToInt(V3.x/denominator.x),Mathf.RoundToInt(V3.y/denominator.y),Mathf.RoundToInt(V3.z/denominator.z));
}
public static Vector3 Multiply(this Vector3 This,Vector3 V3){
string dpName = "Vector3.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(This.x*V3.x,This.y*V3.y,This.z*V3.z);
}
public static Vector3 Multiply(this Vector3 This,Vector3 V3,out string debug){
string dpName = "Vector3.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
debug = "("+This.x+"*"+V3.x+"="+This.x*V3.x+","+This.y+"*"+V3.y+"="+This.y*V3.y+","+This.z+"*"+V3.z+"="+This.z*V3.z+")";
return new Vector3(This.x*V3.x,This.y*V3.y,This.z*V3.z);
}
public static Vector3 Modulus(this Vector3 V3,Vector3 denominator){
string dpName = "Vector3.Modulus(Vector3)";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(V3.x%denominator.x,V3.y%denominator.y,V3.z%denominator.z);
}
public static Vector3 Modulus(this Vector3 V3,float denominator){
string dpName = "Vector3.Modulus(float)";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(V3.x%denominator,V3.y%denominator,V3.z%denominator);
}
public static Vector3 Clamp(this Vector3 V3,Vector3 min,Vector3 max){
string dpName = "Vector3.Clamp";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(Mathf.Clamp(V3.x,min.x,max.x),Mathf.Clamp(V3.y,min.y,max.y),Mathf.Clamp(V3.z,min.z,max.z));
}
public static Vector3 InverseClamp(this Vector3 V3,Vector3 min,Vector3 max){
string dpName = "Vector3.InverseClamp";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(V3.x.InverseClamp(min.x,max.x),V3.y.InverseClamp(min.y,max.y),V3.z.InverseClamp(min.z,max.z));
}
public static Vector3 SetX(this Vector3 V3,float value){
string dpName = "Vector3.SetX";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
V3 = new Vector3(value,V3.y,V3.z);
return V3;
}
public static Vector3 SetY(this Vector3 V3,float value){
string dpName = "Vector3.SetY";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
V3 = new Vector3(V3.x,value,V3.z);
return V3;
}
public static Vector3 SetZ(this Vector3 V3,float value){
string dpName = "Vector3.SetZ";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
V3 = new Vector3(V3.x,V3.y,value);
return V3;
}
public static Vector2Int Strip(this Vector3Int v3,char dim){
string dpName = "Vector3.Strip";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
switch(dim){
case 'x':return new Vector2Int(v3.y,v3.z);
case 'y':return new Vector2Int(v3.x,v3.z);
case 'z':return new Vector2Int(v3.x,v3.y);
}
Debug.Log(dim+" is not a dimension of Vector3");
return Vector2Int.zero;
}
public static Vector3Int Multiply(this Vector3Int This,Vector3Int V3){
string dpName = "Vector3Int.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(This.x*V3.x,This.y*V3.y,This.z*V3.z);
}
public static Vector3 Multiply(this Vector3Int This,Vector3 V3){
string dpName = "Vector3Int.Multiply";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(This.x*V3.x,This.y*V3.y,This.z*V3.z);
}
public static Vector3Int Multiply(this Vector3Int This,float flt){
string dpName = "Vector3Int.MultiplyFloat";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(This.x*flt,This.y*flt,This.z*flt).FloorToInt();
}
public static Vector3Int FloorToInt(this Vector3 V3){
string dpName = "Vector3.FloorToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.FloorToInt(V3.x),Mathf.FloorToInt(V3.y),Mathf.FloorToInt(V3.z));
}
public static Vector3Int FloorToInt(this Vector3 V3,Vector3 interval){
string dpName = "Vector3.FloorToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.FloorToInt(Mathf.Floor(V3.x/interval.x)*interval.x),Mathf.FloorToInt(Mathf.Floor(V3.y/interval.y)*interval.y),Mathf.FloorToInt(Mathf.Floor(V3.z/interval.z)*interval.z));
}
public static Vector3Int CeilToInt(this Vector3 V3){
string dpName = "Vector3.CeilToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.CeilToInt(V3.x),Mathf.CeilToInt(V3.y),Mathf.CeilToInt(V3.z));
}
public static Vector3Int RoundToInt(this Vector3 V3){
string dpName = "Vector3.RoundToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.RoundToInt(V3.x),Mathf.RoundToInt(V3.y),Mathf.RoundToInt(V3.z));
}
public static Vector3Int RoundToInt(this Vector3 V3,Vector3 interval){
string dpName = "Vector3.RoundToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.RoundToInt(Mathf.Round(V3.x/interval.x)*interval.x),Mathf.RoundToInt(Mathf.Round(V3.y/interval.y)*interval.y),Mathf.RoundToInt(Mathf.Round(V3.z/interval.z)*interval.z));
}
public static Vector3 Round(this Vector3 V3){
string dpName = "Vector3.Round";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(Mathf.Round(V3.x),Mathf.Round(V3.y),Mathf.Round(V3.z));
}
public static Vector3 Round(this Vector3 V3,Vector3 interval){
string dpName = "Vector3.Round";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(Mathf.Round(V3.x/interval.x)*interval.x,Mathf.Round(V3.y/interval.y)*interval.y,Mathf.Round(V3.z/interval.z)*interval.z);
}
public static Vector3 Floor(this Vector3 V3){
string dpName = "Vector3.Floor";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(Mathf.Floor(V3.x),Mathf.Floor(V3.y),Mathf.Floor(V3.z));
}
public static Vector3Int Abs(this Vector3Int V3){
string dpName = "Vector3Int.Abs";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.Abs(V3.x),Mathf.Abs(V3.y),Mathf.Abs(V3.z));
}
public static Vector3 Sign0(this Vector3 V3){
string dpName = "Vector3.Sign0";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3(V3.x.Sign0(),V3.y.Sign0(),V3.z.Sign0());
}
public static Vector3Int SetX(this Vector3Int V3,int value){
string dpName = "Vector3Int.SetX";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
V3 = new Vector3Int(value,V3.y,V3.z);
return V3;
}
public static Vector3Int SetY(this Vector3Int V3,int value){
string dpName = "Vector3Int.SetY";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
V3 = new Vector3Int(V3.x,value,V3.z);
return V3;
}
public static Vector3Int SetZ(this Vector3Int V3,int value){
string dpName = "Vector3Int.SetZ";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
V3 = new Vector3Int(V3.x,V3.y,value);
return V3;
}
public static float[] Serialize(this Vector3 This){
string dpName = "Vector3.Serialize";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
float[] rtn = new float[3];
rtn[0] = This.x;
rtn[1] = This.y;
rtn[2] = This.z;
return rtn;
}
public static int[] Serialize(this Vector3Int This){
string dpName = "Vector3Int.Serialize";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
int[] rtn = new int[3];
rtn[0] = This.x;
rtn[1] = This.y;
rtn[2] = This.z;
return rtn;
}
public static float[][] Serialize(this List<Vector3> This){
string dpName = "List<Vector3>.Serialize";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
List<float[]> rtn = new List<float[]>();
for(int i=0; i<This.Count;i++){
rtn.Add(Serialize(This[i]));
}
return rtn.ToArray();
}
public static int[][] Serialize(this List<Vector3Int> This){
string dpName = "List<Vector3>.Serialize";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
List<int[]> rtn = new List<int[]>();
for(int i=0; i<This.Count;i++){
rtn.Add(Serialize(This[i]));
}
return rtn.ToArray();
}
public static List<Vector3> Deserialize(this float[][] This){
string dpName = "List<Vector3>.Deserialize";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
List<Vector3> rtn = new List<Vector3>();
for(int i=0; i<This.Length;i++){
if(This[i].Length<3){
Debug.Log("Array["+i+"] only contains "+This[i].Length+" elements! Must contain at least 3. Returning null");
return null;
}else{
rtn.Add(This[i].Deserialize<Vector3>());
}
}
return rtn;
}
public static List<Vector3Int> Deserialize(this int[][] This){
string dpName = "List<Vector3>.Deserialize";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
List<Vector3Int> rtn = new List<Vector3Int>();
for(int i=0; i<This.Length;i++){
if(This[i].Length<3){
Debug.Log("Array["+i+"] only contains "+This[i].Length+" elements! Must contain at least 3. Returning null");
return null;
}else{
rtn.Add(This[i].Deserialize<Vector3Int>());
}
}
return rtn;
}
public static bool Contains(this List<Vector3> This,Vector2 Value,char exclude='z'){
string dpName = "List<Vector3>.Contains";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
//string str = "";
foreach(Vector3 v3 in This){
//str += v3.Strip(exclude)+"\n";
if(v3.Strip(exclude)==Value){
//Debug.Log(str+"Contains "+Value);
return true;
}
}
//Debug.Log(str+"Does not Contain "+Value);
return false;
}
#region float/int to Vector
public static Vector3 Vector3(this float This){
return new Vector3(This,This,This);
}
public static Vector3 Vector3(this int This){
return new Vector3(This,This,This);
}
public static Vector3Int Vector3Int(this float This,string roundType="floor"){
string RoundType = roundType.ToLower();
switch(RoundType){
case "floor":
return new Vector3Int(Mathf.FloorToInt(This),Mathf.FloorToInt(This),Mathf.FloorToInt(This));
case "round":
return new Vector3Int(Mathf.RoundToInt(This),Mathf.RoundToInt(This),Mathf.RoundToInt(This));
case "ceil":
return new Vector3Int(Mathf.CeilToInt(This),Mathf.CeilToInt(This),Mathf.CeilToInt(This));
}
Debug.Log(roundType+" is not a valid round type. Use (Floor,Round,Ceil). Assuming round type Floor");
return new Vector3Int(Mathf.FloorToInt(This),Mathf.FloorToInt(This),Mathf.FloorToInt(This));
}
public static Vector3Int Vector3Int(this int This){
return new Vector3Int(This,This,This);
}
#endregion float/int to Vector
public static Vector3 Relative(this Vector3 This,Transform transform){
///Returns a vector3 relative to the rotation of a transform
string dpName = "Vector3.Relative";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return ((transform.right*This.x)+(transform.up*This.y)+(transform.forward*This.z));
}
public static Vector3 FlatRelative(this Vector3 This,Transform transform){
///Returns a vector3 relative to the rotation of a transform, disregarding the y axis
string dpName = "Vector3.FlatRelative";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return ((transform.right*This.x)+(new Vector3(0,This.y,0))+(transform.forward*This.z));
}

#endregion Vector3
#region Vector4
public static Vector4Int FloorToInt(this Vector4 V4){
string dpName = "Vector4.FloorToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.FloorToInt(V4.x),Mathf.FloorToInt(V4.y),Mathf.FloorToInt(V4.z));
}
public static Vector4Int RoundToInt(this Vector4 V4){
string dpName = "Vector4.RoundToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.RoundToInt(V4.x),Mathf.RoundToInt(V4.y),Mathf.RoundToInt(V4.z));
}
public static Vector4Int CeilToInt(this Vector4 V4){
string dpName = "Vector4.CeilToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new Vector3Int(Mathf.CeilToInt(V4.x),Mathf.CeilToInt(V4.y),Mathf.CeilToInt(V4.z));
}
#endregion Vector4
#endregion Vectors
#region GameObject/Transform
public static void CopyLocalToLocal(this Transform tf1,Transform tf2,bool position=true,bool rotation=true,bool localScale=true){
if(position)tf2.localPosition = tf1.localPosition;
if(rotation)tf2.localEulerAngles = tf1.localEulerAngles;
if(localScale)tf2.localScale = tf1.localScale;
}
public static void CopyToLocal(this Transform tf1,Transform tf2,bool position=true,bool rotation=true,bool localScale=true){
if(position)tf2.localPosition = tf1.position;
if(rotation)tf2.localEulerAngles = tf1.eulerAngles;
if(localScale)tf2.localScale = tf1.localScale;
}
public static void CopyTo(this Transform tf1,Transform tf2,bool position=true,bool rotation=true,bool localScale=true){
if(position)tf2.position = tf1.position;
if(rotation)tf2.eulerAngles = tf1.eulerAngles;
if(localScale)tf2.localScale = tf1.localScale;
}
public static void CopyLocalTo(this Transform tf1,Transform tf2,bool position=true,bool rotation=true,bool localScale=true){
if(position)tf2.position = tf1.localPosition;
if(rotation)tf2.eulerAngles = tf1.localEulerAngles;
if(localScale)tf2.localScale = tf1.localScale;
}
public static void RemoveComponent<T>(this GameObject go){
//Removes the component from a GameObject
if(go.GetComponent<T>()!=null)GameObject.Destroy(go.GetComponent<T>() as Object);
}
public static void DestroyChildren(this GameObject go){
///Destroys all children off of a GameObjects Transform
string dpName = "GameObject.DestroyChildren";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
foreach(Transform child in go.transform){
GameObject.Destroy(child.gameObject);
}
}
public static void DestroyChildren(this Transform go){
///Destroys all children of a Transform
string dpName = "Transform.DestroyChildren";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
foreach(Transform child in go){
GameObject.Destroy(child.gameObject);
}
}
public static void DestroyChildreninEditor(this GameObject go){
///Destroys all children off of a GameObjects Transform, usable in Editor
string dpName = "GameObject.DestroyChildreninEditor";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
List<GameObject> toDelete = new List<GameObject>();
while(go.transform.childCount>0){
GameObject.DestroyImmediate(go.transform.GetChild(0).gameObject);
}
}
public static T[] GetComponent<T>(this GameObject[] This){
//Get component[] from GameObject[]
T[] rtn = new T[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = This[i].GetComponent<T>();
}
return rtn;
}
public static Transform[] GetChildren(this Transform This){
Transform[] rtn = new Transform[This.childCount];
for(int i=0; i<This.childCount;i++){
rtn[i] = This.GetChild(i);
}
return rtn;
}
public static Transform[] GetAllChildren(this Transform This){
List<Transform> toCheck = new List<Transform>();
List<Transform> Checked = new List<Transform>();
toCheck.Add(This);
int wb = 0;
while(toCheck.Count>0){
if(!Checked.Contains(toCheck[0])){
toCheck.Add(toCheck[0].GetChildren());
Checked.Add(toCheck[0]);
}
toCheck.RemoveAt(0);
#region While Break
wb++;
if(wb>100){
Debug.Log("While Break");
break;
}
#endregion While Break
}

return Checked.ToArray();
}
public static GameObject GetChild(this GameObject This,string name){
//<summary>
//Returns any child within the GameObjects Family Tree(lower)
//</summary>
List<Transform> Checked = new List<Transform>();
List<Transform> toCheck = new List<Transform>();
toCheck.Add(This.transform);
int wb = 0;
while(toCheck.Count>0){ 
foreach(Transform tf in toCheck[0].transform){
if(tf.gameObject.name==name)return tf.gameObject;
if(!Checked.Contains(tf))Checked.Add(tf);
}
Checked.Add(toCheck[0].transform);
toCheck.RemoveAt(0);
#region While Break
wb++;
if(wb>100){
Debug.Log("Child "+name+" of "+This.name+" not found after searching 100 children. Returning null");
return null;
}
#endregion While Break
}
Debug.Log("Child "+name+" of "+This.name+" not found. Returning null");
return null;
}
public static GameObject[] GetChildren(this GameObject This,params string[] names){
//<summary>
//returns any children within the GameObjects Family Tree(lower) matching the list of given names
//</summary>
GameObject[] rtn = new GameObject[names.Length];
int foundCount = 0;
List<Transform> Checked = new List<Transform>();
List<Transform> toCheck = new List<Transform>();
toCheck.Add(This.transform);
int wb = 0;
while(toCheck.Count>0){
foreach(Transform tf in toCheck[0].transform.GetChildren()){
//check to see if gameObjects name matches any in given list
for(int n=0; n<names.Length;n++){
if(tf.gameObject.name==names[n]){
rtn[n] = tf.gameObject;
foundCount++;
if(foundCount==names.Length)return rtn;
}
}
//if transform is neither in the Checked or toCheck lists, add it to the toCheck List
if(!(toCheck.Contains(tf)||Checked.Contains(tf)))toCheck.Add(tf);
}
Checked.Add(toCheck[0].transform);
toCheck.RemoveAt(0);
#region While Break
wb++;
if(wb>100){
Debug.Log("Only "+foundCount+" children found after searching 100 children");
return rtn;
}
#endregion While Break
}
Debug.Log("Only "+foundCount+" children found");
return rtn;
}
public static GameObject[] GameObjects(this Transform[] This){
GameObject[] rtn = new GameObject[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = This[i].gameObject;
}
return rtn;
}

#endregion GameObject/Transform
#region Int/Float
public static float[] ToFloat(this int[] This){ 
float[] rtn = new float[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = (float)This[i];
}
return rtn;
}
public static int Cycle(this int This,int min,int max,bool overflow=false){
string dpName = "int.Cycle";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
//Works like Mathf.Clamp but pacmans the result instead
if(This<min)return overflow?max+(This%max):max;
if(This>max)return overflow?min+(This%max):min;
return This;
}
public static int[] FloorToInt(this float[] This){
string dpName = "float[].FloorToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
int[] rtn = new int[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = Mathf.FloorToInt(This[i]);
}
return rtn;
}
public static int[] RoundToInt(this float[] This){
string dpName = "float[].RoundToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
int[] rtn = new int[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = Mathf.RoundToInt(This[i]);
}
return rtn;
}
public static int[] CeilToInt(this float[] This){
string dpName = "float[].CeilToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
int[] rtn = new int[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = Mathf.CeilToInt(This[i]);
}
return rtn;
}
public static int ToInt(this bool This){return This?1:0;}
public static bool isBetween(this float This,float min,float max,bool inclusive=false){
string dpName = "float.isBetween";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return inclusive?(This>=min&&This<=max):(This>min&&This<max);
}
public static float Sign0(this float num){
string dpName = "float.Sign0";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
if(num==0)return 0;
return Mathf.Sign(num);
}
public static float Clamp(this float This,float min,float max){
return Mathf.Clamp(This,min,max);
}
public static float Clamp(this float This,float min,float max,out float difference){
difference = This-Mathf.Clamp(This,min,max);
return Mathf.Clamp(This,min,max);
}
public static float InverseClamp(this float This,float min,float max){
if(This>Mathf.Lerp(max,min,.5f)&&This<max){
return max;
}
if(This<Mathf.Lerp(max,min,.5f)&&This>min){
return min;
}
return This;
}
public static string toBase(this int This,int Base,string digits = ""){
//Takes a number and converts it to a different base
/* I.E
6.ToBase(2);
returns 110, because 110 is 6 in binary
*/
//Get largest number
string Digits = digits;
if(Digits=="")Digits = "0123456789abcdefghijklmnopqrstuvwxyz";
int weight = 1;

int wb = 0;
//get largest column
while(weight<This){
weight *= Base;
#region WB
wb++;
if(wb>100){
Debug.Log("While Break");
break;
}
#endregion WB
}

Debug.Log(weight);

bool firstDig = true;
int remainder = This;
string rtn = "";
wb = 0;
while(weight>0){
int curCol = 0;
int wb2=0;
Debug.Log(weight+" goes into "+remainder);
//get # of times current column goes into remainder
while(remainder>=weight){
curCol++;
remainder -= weight;
#region WB
wb2++;
if(wb2>100){
Debug.Log("While Break2");
break;
}
#endregion WB
}
Debug.Log(curCol+" times");
//if largest digit == 0, then don't add it
if(Digits[curCol]=='0'&&firstDig){
firstDig = false;
}else{
//add digit
rtn += Digits[curCol];
}
weight /= Base;
#region WB
wb++;
if(wb>100){
Debug.Log("While Break");
break;
}
#endregion WB
}

return rtn;
}
public static int fromBase(this string This,int Base,string digits = ""){
int rtn = 0;
string str = This.Reverse();
string Digits = digits;
if(Digits=="")Digits = "0123456789abcdefghijklmnopqrstuvwxyz";

int weight = 1;
for(int i=0; i<str.Length;i++){
//get digit value
rtn += weight*Digits.IndexOf(str[i]);
weight *= Base;

}

return rtn;
}
public static int UnBinary(this bool[] This,bool LeftToRight=true){
///Views an array of booleans as a binary number and converts it to an integer
string dpName = "bool[].UnBinary";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
int weight = 1;
int num = 0;
if(LeftToRight){
for(int i=0;i<This.Length;i++){
num += This[i]?weight:0;
weight *= 2;
}
}
return num;
}
public static float Larger(this float This,float compare){
/// <summary>
/// shorthand for (This>compare?This:compare)
/// </summary>
if(This>compare){
return This;
}
return compare;
}
public static float Smaller(this float This,float compare){
/// <summary>
/// shorthand for (This<compare?This:compare)
/// </summary>
if(This<compare){
return This;
}
return compare;
}
#endregion Int/Float
public static void setFont(this TMPro.TextMeshProUGUI TMP, FontData fontData){
string dpName = "TMPro.TextMeshProUGUI.setFont";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
TMP.color = fontData.color;
TMP.fontSize = fontData.size;
TMP.font = fontData.font;
TMP.alignment = fontData.alignment;
}
public static bool EqualsOneOf<T>(this T This,params T[] args){
foreach(T arg in args){
if(EqualityComparer<T>.Default.Equals(This, arg))return true;
}
return false;
}
#region T[]
public static string AsString<T>(this T[] This){
if(This.Length==0){
return "[]";
}else{
string str = "[";
foreach(T val in This){
str += val.ToString()+",";
}
str = str.Substring(0,str.Length-1)+"]";
return str;
}
}
public static T[,] To2D<T>(this T[] This,Vector2Int size){
//Converts a 1D array to a 2D Array
string dpName = "T[].To2D"; if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
//Y contains X
T[,] rtn = new T[size.y,size.x];
for(int y=0; y<size.y;y++){
for(int x=0; x<size.x;x++){
rtn[y,x] = This[(y*size.y)+x];
}
}
return rtn;
}
public static T[] To1D<T>(this T[,] This){
///Converts a 2D array to a 1D array
string dpName = "T[,].To1D"; if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
//Y contains X
T[] rtn = new T[This.GetLength(0)*This.GetLength(1)];
for(int y=0; y<This.GetLength(0);y++){
for(int x=0; x<This.GetLength(1);x++){
rtn[(y*This.GetLength(0))+x] = This[y,x];
}
}
return rtn;
}
public static T[][] Compare<T>(this T[] This,T[] value,T Default=default(T)){
///Takes 2 T[]'s and returns a T[][2] defining the differences
T[][] rtn = new T[This.Length][];
for(int i=0; i<This.Length;i++){
if(EqualityComparer<T>.Default.Equals(This[i],value[i])){
rtn[i][0] = Default;
rtn[i][1] = Default;
}else{ 
rtn[i][0] = This[i];
rtn[i][1] = value[i];
}
}
return rtn;
}
#endregion T[]
#region List<T>
public static T Last<T>(this List<T> This){
return This[This.Count-1];
}
public static T AddNew<T>(this List<T> list,T item){
///Adds a new element to this list if it's not already in it
string dpName = "List<>.AddNew";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
if(!list.Contains(item)){
list.Add(item);
return list.Last();
}
return default(T);
}
public static void Add<T>(this List<T> list,List<T> list2){
foreach(T item in list2){
list.Add((T)item);
}
}
public static void Add<T>(this List<T> list,T[] list2){
foreach(T item in list2){
list.Add((T)item);
}
}
public static bool RemoveContained<T>(this List<T> list,T item){
///Removes an element from this list if it contains it
string dpName = "List<>.RemoveContained";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
if(!list.Contains(item)){
list.Remove(item);
return true;
}
return false;
}
public static string AsString<T>(this List<T> This){
if(This.Count==0){
return "[]";
}else{
string str = "[";
foreach(T val in This){
str += val.ToString()+",";
}
str = str.Substring(0,str.Length-1)+"]";
return str;
}
}
public static List<List<T>> Compare<T>(this List<T> This,List<T> value,T Default=default(T)){
///Takes 2 List<T>'s and returns a List<List<T>> defining the differences
List<List<T>> rtn = new List<List<T>>();
for(int i=0; i<This.Count;i++){
rtn.Add(new List<T>());
if(EqualityComparer<T>.Default.Equals(This[i],value[i])){
rtn[i].Add(Default);
rtn[i].Add(Default);
}else{ 
rtn[i].Add(This[i]);
rtn[i].Add(value[i]);
}
}
return rtn;
}
public static List<U> To<T,U>(this List<T> This){
///converts List<T> to List<U>
List<U> rtn = new List<U>();
foreach(T val in This){
rtn.Add((U)(object)val);
}
return rtn;
}
#endregion List<T>
#region List<Named<>>
public static bool Contains<T>(this List<Named<T>> This,T value){
foreach(Named<T> item in This){
if(EqualityComparer<T>.Default.Equals(item.value,value))return true;
}
return false;
}
public static bool Contains<T>(this List<Named<T>> This,T value,out Named<T> Out){
foreach(Named<T> item in This){
if(EqualityComparer<T>.Default.Equals(item.value,value)){
Out = item;
return true;
}
}
Out = null;
return false;
}
public static bool ContainsName<T>(this List<Named<T>> This,string name){
foreach(Named<T> item in This){
if(item.name==name)return true;
}
return false;
}
public static bool ContainsName<T>(this List<Named<T>> This,string name,out Named<T> Out){
foreach(Named<T> item in This){
if(item.name==name){
Out = item;
return true;
}
}
Out = null;
return false;
}
public static int IndexOf<T>(this List<Named<T>> This,T value){
for(int i=0; i<This.Count;i++){
if(EqualityComparer<T>.Default.Equals(This[i].value,value))return i;
}
return -1;
}
public static int IndexOfName<T>(this List<Named<T>> This,string name){
for(int i=0; i<This.Count;i++){
if(This[i].name == name)return i;
}
return -1;
}
public static T Get<T>(this List<Named<T>> This,string name){
foreach(Named<T> obj in This){
if(obj.name==name)return obj;
}
Debug.Log(name+" not found! Returning default value");
return default(T);
}
#region De/Serialize List<Named<Vector3>>
//Serialize out names, out values
public static void Serialize(this List<Named<Vector3>> This,out string[] names,out float[][] values){
List<float[]> Values = new List<float[]>();
List<string> Names = new List<string>();
foreach(Named<Vector3> v3 in This){
Values.Add(v3.value.Serialize());
Names.Add(v3.name);
}
values = Values.ToArray();
names = Names.ToArray();
}
public static List<Named<Vector3>> Deserialize(this List<Named<Vector3>> This,string[] names,float[][] values){
List<Named<Vector3>> rtn = new List<Named<Vector3>>();
for(int i=0; i<names.Length;i++){
rtn.Add(new Named<Vector3>(names[i],values[i].Deserialize<Vector3>()));
}
return rtn;
}
#endregion De/Serialize List<Named<Vector3>>
#endregion List<Named<>>
#region Serialization
public static float[] Serialize(this Color This){
string dpName = "Color.Serialize";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return new float[]{This.r,This.g,This.b,This.a};
}
public static float[][] Serialize(this Texture2D This){
string dpName = "Texture2D.Serialize"; if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
Color[] pixels = This.GetPixels();
float[][] rtn = new float[pixels.Length+1][];
rtn[0] = new float[]{This.width,This.height};
for(int i=1; i<pixels.Length+1;i++){
rtn[i] = pixels[i-1].Serialize();
}
return rtn;
}
public static T Deserialize<T>(this float[] This){
string dpName = "float[].Deserialize()";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
System.Type type = typeof(T);
if(type==typeof(Vector2Int))return (T)((object)new Vector2Int(Mathf.FloorToInt(This[0]),Mathf.FloorToInt(This[1])));
if(type==typeof(Vector3Int))return (T)((object)new Vector3Int(Mathf.FloorToInt(This[0]),Mathf.FloorToInt(This[1]),Mathf.FloorToInt(This[2])));
if(type==typeof(Vector2))return (T)((object)new Vector2(This[0],This[1]));
if(type==typeof(Vector3))return (T)((object)new Vector3(This[0],This[1],This[2]));
if(type==typeof(Vector4))return (T)((object)new Vector4(This[0],This[1],This[2],This[3]));
if(type==typeof(Color))return (T)((object)new Color(This[0],This[1],This[2],This[3]));
return (T)(object)null;
}
public static T Deserialize<T>(this int[] This){
string dpName = "int[].Deserialize()";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
System.Type type = typeof(T);
if(type==typeof(Vector2Int))return (T)((object)new Vector2Int(This[0],This[1]));
if(type==typeof(Vector3Int))return (T)((object)new Vector3Int(This[0],This[1],This[2]));
if(type==typeof(Vector2))return (T)((object)new Vector2(This[0],This[1]));
if(type==typeof(Vector3))return (T)((object)new Vector3(This[0],This[1],This[2]));
if(type==typeof(Vector4))return (T)((object)new Vector4(This[0],This[1],This[2],This[3]));
if(type==typeof(Color))return (T)((object)new Color(This[0],This[1],This[2],This[3]));
return (T)(object)null;
}
public static List<T> DeserializeList<T>(this float[][] This){
string dpName = "float[][].DeserializeList()";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
System.Type type = typeof(T);
List<T> rtn = new List<T>();
foreach(float[] val in This){
rtn.Add(val.Deserialize<T>());
}
return rtn;
}
public static T[] DeserializeArray<T>(this float[][] This){
string dpName = "float[][].DeserializeArray()";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
System.Type type = typeof(T);
T[] rtn = new T[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = This[i].Deserialize<T>();
}
return rtn;
}
public static T Deserialize<T>(this float[][] This){
string dpName = "float[][].Deserialize<>"; if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
System.Type type=typeof(T);
#region Texture2D
if(type==typeof(Texture2D)){
Color[] pixels = new Color[This.Length];
Texture2D rtn = new Texture2D(Mathf.FloorToInt(This[0][0]),Mathf.FloorToInt(This[0][1]));
for(int i=1; i<This.Length;i++){
pixels[i-1] = This[i].Deserialize<Color>();
}
rtn.SetPixels(pixels);
return (T)((object)rtn);
}
#endregion Texture2D
return (T)((object)null);
}
#endregion Serialization
#region Texture
public static Color[,] GetPixels2D(this Texture2D This){
Color[] ThisPixel = This.GetPixels();
Color[,] rtn = new Color[This.height,This.width];
for(int y=0; y<This.height;y++){
for(int x=0; x<This.width;x++){

rtn[y,x] = ThisPixel[(y*This.height)+x];
}
}
return rtn;
}
public static Texture2D SetPixels(this Texture2D This,Color[,] pixels){
This.SetPixels(pixels.To1D());
return This;
}
public static Texture2D Solid(this Texture2D This,Color color){
Color[] pixels = This.GetPixels();
Color[] New = new Color[pixels.Length];
for(int i=0; i<pixels.Length;i++){
New[i] = color;
}
This.SetPixels(New);
Texture2D rtn = new Texture2D(This.width,This.height);
rtn.SetPixels(New);
rtn.Apply();
return rtn;
}
public static Texture2D Stamp(this Texture2D This,Texture2D stamp,Vector2Int position){
Vector2Int min = FF.Vector2_Largest;
Vector2Int max = FF.Vector2_Largest*-1;

Texture2D rtn = This;
//"Stamps" a texture on top of another texture
string dpName = "Texture.Stamp"; if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
Color[] ThisPixel = This.GetPixels();
Color[] StampPixel = stamp.GetPixels();
Vector2Int Dif = new Vector2Int(This.width,This.height)-new Vector2Int(stamp.width,stamp.height);
//foreach pixel in Stamp
for(int y=0; y<stamp.height;y++){
for(int x=0; x<stamp.width;x++){
ThisPixel[((y+position.y)*This.height)+(x+position.x)] = StampPixel[(y*stamp.height)+x];
//if(x+Dif.x>=0&&x+Dif.x<=This.width&&y+Dif.y>=0&&y+Dif.y<=This.height){
//ThisPixel[((x+position.x)*This.width)+(y+position.y)] = StampPixel[(x*stamp.width)+y];
//}
}
}
rtn.SetPixels(ThisPixel);
This = rtn;
return This;
}
#endregion Texture
#region string/char
public static string Reverse(this string This){
///Reverses a string, I.E. "Hello World".Reverse() = "dlroW olleH"
char[] ch = This.ToCharArray();
System.Array.Reverse(ch);
return new string(ch);
}
public static bool isDigit(this char ch){
string dpName = "char.isDigit";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
return (ch=='0'||ch=='1'||ch=='2'||ch=='3'||ch=='4'||ch=='5'||ch=='6'||ch=='7'||ch=='8'||ch=='9');
}
public static int alphaToInt(this char ch){
string dpName = "char.alphaToInt";if(!FF.FunctionDependancies.Contains(dpName))FF.FunctionDependancies.Add(dpName);
for(int i=0; i<FF.alphabet.Length;i++){if(FF.alphabet[i]==ch)return i;}
return -1;
}
public static string Compare(this string This,string value,char Default='*'){
///Compares 2 strings, and returns the difference
///I.E. "Hello World".Compare("Heiio World",'*') = "**ii*******"
string rtn = "";
for(int i=0; i<This.Length;i++){
rtn += (This[i]==value[i])?Default:value[i];
}
return rtn;
}
public static string CompareSideBySide(this string This,string value,char Default='*'){
///Same as string.Compare but shows the values being compared side by side

///I.E. "Hello World".Compare("Heiio World",'*')
///will return
///"Hello World
///Heiio World
///**ii*******"
string rtn = This+"\n"+value+"\n";
for(int i=0; i<This.Length;i++){
rtn += (This[i]==value[i])?Default:value[i];
}
return rtn;
}
#endregion string/char
public static float endTime(this AnimationCurve This){return This.keys[This.keys.Length-1].time;}
public static float startTime(this AnimationCurve This){return This.keys[0].time;}
//Untested
public static Keyframe[] Combine(this Keyframe[] This,Keyframe[] keys){
List<Keyframe> rtn = new List<Keyframe>();
for(int i=0; i<This.Length;i++){
//get all input frames that come before this frame
for(int j=0; j<keys.Length;j++){
if(keys[j].time<This[i].time){
rtn.Add(keys[j]);
}else{ 
break;
}
}
rtn.Add(This[i]);
}
return rtn.ToArray();
}
public static string AsString(this Keyframe[] This){
string rtn = "[";
foreach(Keyframe key in This){
rtn += "("+key.time+":"+key.value+"),";
}
rtn = rtn.Substring(0,rtn.Length-1);
rtn += "]";
return rtn;
}
}
#region Classes
public static class DBG{
public static List<string> log = new List<string>();
public static string Break = ",";
public static string InvisLog(params object[] arguments){
string rtn = "";
foreach(object argument in arguments)rtn += argument.ToString()+",";
rtn = rtn.Substring(0, rtn.Length-1);
log.Add(rtn);
return rtn;
}
public static string Log(params object[] arguments){
string rtn = "";
foreach(object argument in arguments)rtn += argument.ToString()+DBG.Break;
rtn = rtn.Substring(0, rtn.Length-1);
log.Add(rtn);
Debug.Log(rtn);
Break = ",";
return rtn;
}
}
[System.Serializable]public class TransformData{
[HideInInspector]public string name;
public Vector3 localPosition;
public Vector3 localRotation;
public Vector3 localScale;
//public Transform parent;
public TransformData[] children;
public void Apply(Transform tf,bool applyParent=false){
//if(applyParent)tf.parent = parent;
tf.localPosition = localPosition;
tf.localEulerAngles = localRotation;
tf.localScale = localScale;
}
public TransformData[] GetAllChildren(){
List<TransformData> rtn = new List<TransformData>();

List<TransformData> toCheck = new List<TransformData>();
List<TransformData> Checked = new List<TransformData>();
toCheck.Add(this);
int wb = 0;
while(toCheck.Count>0){
if(!Checked.Contains(toCheck[0])){
toCheck.Add(toCheck[0].children);
Checked.Add(toCheck[0]);
}
toCheck.RemoveAt(0);
#region While Break
wb++;
if(wb>100){
Debug.Log("While Break");
break;
}
#endregion While Break
}

return Checked.ToArray();

//return rtn.ToArray();
}
public TransformData(Vector3 Position){
localPosition = Position;
localRotation = Vector3.zero;
localScale = Vector3.one;
}
public TransformData(Vector3 Position,Vector3 Rotation,TransformData[] children=null){
localPosition = Position;
localRotation = Rotation;
localScale = Vector3.one;
this.children = children;
}
public TransformData(Vector3 Position,Vector3 Rotation,Vector3 Scale,TransformData[] children=null){
localPosition = Position;
localRotation = Rotation;
localScale = Scale;
this.children = children;
}
public TransformData(Transform tf){
name = tf.name;
localPosition = tf.localPosition;
localRotation = tf.localEulerAngles;
localScale = tf.localScale;
//parent = tf.parent;


children = new TransformData[tf.childCount];
for(int i=0; i<tf.childCount;i++){
children[i] = new TransformData(tf.GetChild(i));
}

}
public static implicit operator TransformData(Transform This){
return new TransformData(This);
}
}
#region Property Drawer
/*
[CustomPropertyDrawer(typeof(TransformData))]public class TransformData_PD : PropertyDrawer{
private SerializedProperty Position;
private SerializedProperty Rotation;
private SerializedProperty Scale;
private SerializedProperty Parent;
private SerializedProperty Children;
public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
TransformData targetObject = (TransformData)property.serializedObject.targetObject;
EditorGUI.BeginProperty(position,label,property);
Rect foldoutRect = new Rect(position.x,position.y,position.width,FF.GUI_LineHeight);
property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded,label);

if(property.isExpanded){
EditorGUI.indentLevel++;
Rect rect = new Rect(position.x,position.y+FF.GUI_LineHeight,position.width/2,FF.GUI_LineHeight);
EditorGUI.PropertyField(rect,Position,GUIContent.none,true);
EditorGUI.PropertyField(rect,Rotation,GUIContent.none,true);
EditorGUI.PropertyField(rect,Scale,GUIContent.none,true);
EditorGUI.PropertyField(rect,Parent,GUIContent.none,true);
System.Reflection.FieldInfo FI_Children = typeof(TransformData[]).GetField(property.propertyPath);
if(FI_Children!=null){
TransformData[] _Children = (TransformData[])FI_Children.GetValue(targetObject);
if(_Children.Length>0)EditorGUI.PropertyField(rect,Children,GUIContent.none,true);
}
EditorGUI.indentLevel--;
}

EditorGUI.EndProperty();
}

}
*/
#endregion Property Drawer
[System.Serializable]public class TransformCurve{
public Vector3Curve position;
public Vector3Curve rotation;
public Vector3Curve scale;
public float time;
public float endTime{get{
float rtn = position.endTime;
if(rotation.endTime<rtn)rtn = rotation.endTime;
if(scale.endTime<rtn)rtn = scale.endTime;
return rtn;
}}
public float startTime{get{
float rtn = position.startTime;
if(rotation.startTime>rtn)rtn = rotation.startTime;
if(scale.startTime>rtn)rtn = scale.startTime;
return rtn;
}}
public float totalTime{get{return endTime-startTime;}}
public void Apply(Transform target,float time){
target.localPosition = position.Evaluate(time);
target.localEulerAngles = rotation.Evaluate(time);
target.localScale = scale.Evaluate(time);
}
public void Add(Transform target,float time){
target.localPosition += position.Evaluate(time);
target.localEulerAngles += rotation.Evaluate(time);
target.localScale += scale.Evaluate(time);
}
public void Record(Transform target,float time){
position.AddKey(time,target.localPosition);
rotation.AddKey(time,target.localEulerAngles);
scale.AddKey(time,target.localScale);
}
public void Clear(){
position.Clear();
rotation.Clear();
scale.Clear();
}
#region Operators
public TransformCurve(){ 
position = new Vector3Curve();
rotation = new Vector3Curve();
scale = new Vector3Curve();
}
public TransformCurve(TransformCurve curve){
#region Position
Keyframe[][] keyframes = new Keyframe[3][];
position = new Vector3Curve();
for(int d=0; d<3;d++){
List<Keyframe> keys = new List<Keyframe>();
for(int i=0; i<curve.position.keys[d].Length;i++){
keys.Add(new Keyframe(curve.position.keys[d][i].value,curve.position.keys[d][i].time,curve.position.keys[d][i].inTangent,curve.position.keys[d][i].outTangent));
}
keyframes[d] = keys.ToArray();
}
position.keys = keyframes;
#endregion Position
#region Rotation
keyframes = new Keyframe[3][];
rotation = new Vector3Curve();
for(int d=0; d<3;d++){
List<Keyframe> keys = new List<Keyframe>();
for(int i=0; i<curve.rotation.keys[d].Length;i++){
keys.Add(new Keyframe(curve.rotation.keys[d][i].value,curve.rotation.keys[d][i].time,curve.rotation.keys[d][i].inTangent,curve.rotation.keys[d][i].outTangent));
}
keyframes[d] = keys.ToArray();
}
rotation.keys = keyframes;
#endregion Position
#region Scale
keyframes = new Keyframe[3][];
scale = new Vector3Curve();
for(int d=0; d<3;d++){
List<Keyframe> keys = new List<Keyframe>();
for(int i=0; i<curve.scale.keys[d].Length;i++){
keys.Add(new Keyframe(curve.scale.keys[d][i].value,curve.scale.keys[d][i].time,curve.scale.keys[d][i].inTangent,curve.scale.keys[d][i].outTangent));
}
keyframes[d] = keys.ToArray();
}
scale.keys = keyframes;
#endregion Scale
}
public static TransformCurve operator +(TransformCurve curve1, TransformCurve curve2){
TransformCurve rtn = new TransformCurve();
//Debug.Log("Add Positions");
rtn.position = curve1.position+curve2.position;
//Debug.Log("Add Rotations");
rtn.rotation = curve1.rotation+curve2.rotation;
//Debug.Log("Add Scales");
rtn.scale = curve1.scale+curve2.scale;
//DBG.Break = "+";
//DBG.Log(curve1.position.z[0].value,curve2.position.z[0].value+"="+rtn.position.z[0].value);
return rtn;
}
public static TransformCurve operator -(TransformCurve curve1, TransformCurve curve2){
TransformCurve rtn = new TransformCurve();
rtn.position = curve1.position-curve2.position;
rtn.rotation = curve1.rotation-curve2.rotation;
rtn.scale = curve1.scale-curve2.scale;
return rtn;
}
public static TransformCurve operator *(TransformCurve curve1, TransformCurve curve2){
TransformCurve rtn = new TransformCurve();
rtn.position = curve1.position*curve2.position;
rtn.rotation = curve1.rotation*curve2.rotation;
rtn.scale = curve1.scale*curve2.scale;
return rtn;
}
public static TransformCurve operator /(TransformCurve curve1, TransformCurve curve2){
TransformCurve rtn = new TransformCurve();
rtn.position = curve1.position/curve2.position;
rtn.rotation = curve1.rotation/curve2.rotation;
rtn.scale = curve1.scale/curve2.scale;
return rtn;
}
public static TransformCurve operator +(TransformCurve curve1, float num){
TransformCurve rtn = new TransformCurve();
rtn.position = curve1.position+num;
rtn.rotation = curve1.rotation+num;
rtn.scale = curve1.scale+num;
return rtn;
}
public static TransformCurve operator -(TransformCurve curve1, float num){
TransformCurve rtn = new TransformCurve();
rtn.position = curve1.position-num;
rtn.rotation = curve1.rotation-num;
rtn.scale = curve1.scale-num;
return rtn;
}
public static TransformCurve operator *(TransformCurve curve1, float num){
TransformCurve rtn = new TransformCurve();
rtn.rotation = curve1.rotation*num;
rtn.position = curve1.position*num;
rtn.scale = curve1.scale*num;
return rtn;
}
public static TransformCurve operator /(TransformCurve curve1, float num){
TransformCurve rtn = new TransformCurve();
rtn.position = curve1.position/num;
rtn.rotation = curve1.rotation/num;
rtn.scale = curve1.scale/num;
return rtn;
}
#endregion Operators
}
[System.Serializable]public class Vector3Curve{
public AnimationCurve x;
public AnimationCurve y;
public AnimationCurve z;
public Vector3 Evaluate(float time){ 
return new Vector3(x.Evaluate(time),y.Evaluate(time),z.Evaluate(time));
}
public int length{get{
int rtn = 0;
if(x.length>rtn)rtn=x.length;
if(y.length>rtn)rtn=y.length;
if(z.length>rtn)rtn=z.length;
return rtn;
}}
public Keyframe[][] keys{
get{
return new Keyframe[][]{x.keys,y.keys,z.keys};
}
set{
x.keys = value[0];
y.keys = value[1];
z.keys = value[2];
}
}
public void AddKey(float time,Vector3 value){
x.AddKey(time,value.x);
y.AddKey(time,value.y);
z.AddKey(time,value.z);
}
public void MoveKey(int index,Keyframe[] key){
x.MoveKey(index,key[0]);
y.MoveKey(index,key[1]);
z.MoveKey(index,key[2]);
}
public void RemoveKey(int index){
x.RemoveKey(index);
y.RemoveKey(index);
z.RemoveKey(index);
}
public void SmoothTangents(int index,float weight){ 
x.SmoothTangents(index,weight);
y.SmoothTangents(index,weight);
z.SmoothTangents(index,weight);
}
public void Clear(){
x.keys = new Keyframe[0];
y.keys = new Keyframe[0];
z.keys = new Keyframe[0];
}
public new string ToString(){
string rtn = "";
rtn += "X "+x.keys.AsString()+"\n";
rtn += "Y "+y.keys.AsString()+"\n";
rtn += "Z "+z.keys.AsString()+"\n";
return rtn;
}
public float startTime{get{
float rtn = x.startTime();
if(y.startTime()>rtn)rtn = y.startTime();
if(z.startTime()>rtn)rtn = z.startTime();
return rtn;
}}
public float endTime{get{
float rtn = x.endTime();
if(y.endTime()<rtn)rtn = y.endTime();
if(z.endTime()<rtn)rtn = z.endTime();
return rtn;
}}
public float totalTime{get{return endTime-startTime;}}
public Vector3Curve(){ 
x = new AnimationCurve();
y = new AnimationCurve();
z = new AnimationCurve();
}
public Vector3Curve(AnimationCurve x, AnimationCurve y, AnimationCurve z){
this.x = x;
this.y = y;
this.z = z;
}
#region Operators
public static Vector3Curve operator +(Vector3Curve V3C1,Vector3Curve V3C2){
Vector3Curve rtn = new Vector3Curve();
for(int d=0; d<3;d++){
//Debug.Log(d==0?("x"):(d==1?("y"):("z")));
List<Keyframe> keyframes = new List<Keyframe>();
//if(d==0)Debug.Log("X");
//if(d==1)Debug.Log("Y");
//if(d==2)Debug.Log("Z");
bool oneHasMore = V3C1.keys[d].Length>V3C2.keys[d].Length;
//find which Curve has more KeyFrames
int j=0;
//iterate through every keyframe
for(int i=0; i<(oneHasMore?V3C1.keys[d].Length:V3C2.keys[d].Length);i++){
Keyframe moreNext = oneHasMore?V3C1.keys[d][i]:V3C2.keys[d][i];
Keyframe lessNext = oneHasMore?V3C2.keys[d][j]:V3C1.keys[d][j];
//Debug.Log("moreNext["+d+"]["+(oneHasMore?i:j)+"] " + moreNext.time+":"+moreNext.value);
//Debug.Log("lessNext["+d+"]["+(oneHasMore?j:i)+"] " + lessNext.time+":"+lessNext.value);

//if less.next keyframe comes before more.next keyframe
if(lessNext.time<moreNext.time){
//Debug.Log(lessNext.time+"<"+moreNext.time);
//synthesize keyframe from less.next and more.value at less.next.time
if(d==0){keyframes.Add(new Keyframe(lessNext.time,lessNext.value+(oneHasMore?V3C1.x.Evaluate(lessNext.time):V3C2.x.Evaluate(lessNext.time)) ));}//Debug.Log("Add Keyframe "+lessNext.value+(oneHasMore?V3C1.x.Evaluate(lessNext.time):V3C2.x.Evaluate(lessNext.time))+" at time "+lessNext.time+" value+Evaluate");}
if(d==1){keyframes.Add(new Keyframe(lessNext.time,lessNext.value+(oneHasMore?V3C1.y.Evaluate(lessNext.time):V3C2.y.Evaluate(lessNext.time)) ));}//Debug.Log("Add Keyframe "+lessNext.value+(oneHasMore?V3C1.y.Evaluate(lessNext.time):V3C2.y.Evaluate(lessNext.time))+" at time "+lessNext.time+" value+Evaluate");}
if(d==2){keyframes.Add(new Keyframe(lessNext.time,lessNext.value+(oneHasMore?V3C1.z.Evaluate(lessNext.time):V3C2.z.Evaluate(lessNext.time)) ));}//Debug.Log("Add Keyframe "+lessNext.value+(oneHasMore?V3C1.z.Evaluate(lessNext.time):V3C2.z.Evaluate(lessNext.time))+" at time "+lessNext.time+" value+Evaluate");}
j++;
}
//if less.next and more.next times line up
if(moreNext.time==lessNext.time){
//Combine Keyframes
keyframes.Add(new Keyframe(moreNext.time,moreNext.value+lessNext.value));
//Debug.Log("Add Keyframe "+moreNext.time+":"+(moreNext.value+lessNext.value)+" combine Keyframes");
j++;
}else{
//if more.next is not same time as less.next, add it
keyframes.Add(new Keyframe(moreNext.time,moreNext.value,moreNext.inTangent,moreNext.outTangent));
//Debug.Log("Add Keyframe "+moreNext.value+" at time "+moreNext.time+" add individual");

}

}

if(d==0)rtn.x.keys = keyframes.ToArray();
if(d==1)rtn.y.keys = keyframes.ToArray();
if(d==2)rtn.z.keys = keyframes.ToArray();
}
return rtn;
}
//UnTested
public static Vector3Curve operator -(Vector3Curve V3C1,Vector3Curve V3C2){
Vector3Curve rtn = new Vector3Curve();
List<Keyframe> xkeys = new List<Keyframe>();
List<Keyframe> ykeys = new List<Keyframe>();
List<Keyframe> zkeys = new List<Keyframe>();
for(int i=0; i<V3C1.keys[0].Length;i++){
xkeys.Add(new Keyframe(V3C1.keys[0][i].time,V3C1.keys[0][i].value-V3C2.keys[0][i].value,Mathf.Lerp(V3C1.keys[0][i].inTangent,V3C2.keys[0][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[0][i].outTangent,V3C2.keys[0][i].outTangent,.5f)));
ykeys.Add(new Keyframe(V3C1.keys[1][i].time,V3C1.keys[1][i].value-V3C2.keys[1][i].value,Mathf.Lerp(V3C1.keys[1][i].inTangent,V3C2.keys[1][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[1][i].outTangent,V3C2.keys[1][i].outTangent,.5f)));
zkeys.Add(new Keyframe(V3C1.keys[2][i].time,V3C1.keys[2][i].value-V3C2.keys[2][i].value,Mathf.Lerp(V3C1.keys[2][i].inTangent,V3C2.keys[2][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[2][i].outTangent,V3C2.keys[2][i].outTangent,.5f)));
}
rtn.x.keys = xkeys.ToArray();
rtn.y.keys = ykeys.ToArray();
rtn.z.keys = zkeys.ToArray();
return rtn;
}
//UnTested
public static Vector3Curve operator *(Vector3Curve V3C1,Vector3Curve V3C2){
Vector3Curve rtn = new Vector3Curve();
List<Keyframe> xkeys = new List<Keyframe>();
List<Keyframe> ykeys = new List<Keyframe>();
List<Keyframe> zkeys = new List<Keyframe>();
for(int i=0; i<V3C1.keys[0].Length;i++){
xkeys.Add(new Keyframe(V3C1.keys[0][i].time,V3C1.keys[0][i].value*V3C2.keys[0][i].value,Mathf.Lerp(V3C1.keys[0][i].inTangent,V3C2.keys[0][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[0][i].outTangent,V3C2.keys[0][i].outTangent,.5f)));
ykeys.Add(new Keyframe(V3C1.keys[1][i].time,V3C1.keys[1][i].value*V3C2.keys[1][i].value,Mathf.Lerp(V3C1.keys[1][i].inTangent,V3C2.keys[1][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[1][i].outTangent,V3C2.keys[1][i].outTangent,.5f)));
zkeys.Add(new Keyframe(V3C1.keys[2][i].time,V3C1.keys[2][i].value*V3C2.keys[2][i].value,Mathf.Lerp(V3C1.keys[2][i].inTangent,V3C2.keys[2][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[2][i].outTangent,V3C2.keys[2][i].outTangent,.5f)));
}
rtn.x.keys = xkeys.ToArray();
rtn.y.keys = ykeys.ToArray();
rtn.z.keys = zkeys.ToArray();
return rtn;
}
//UnTested
public static Vector3Curve operator /(Vector3Curve V3C1,Vector3Curve V3C2){
Vector3Curve rtn = new Vector3Curve();
List<Keyframe> xkeys = new List<Keyframe>();
List<Keyframe> ykeys = new List<Keyframe>();
List<Keyframe> zkeys = new List<Keyframe>();
for(int i=0; i<V3C1.keys[0].Length;i++){
xkeys.Add(new Keyframe(V3C1.keys[0][i].time,V3C1.keys[0][i].value/V3C2.keys[0][i].value,Mathf.Lerp(V3C1.keys[0][i].inTangent,V3C2.keys[0][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[0][i].outTangent,V3C2.keys[0][i].outTangent,.5f)));
ykeys.Add(new Keyframe(V3C1.keys[1][i].time,V3C1.keys[1][i].value/V3C2.keys[1][i].value,Mathf.Lerp(V3C1.keys[1][i].inTangent,V3C2.keys[1][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[1][i].outTangent,V3C2.keys[1][i].outTangent,.5f)));
zkeys.Add(new Keyframe(V3C1.keys[2][i].time,V3C1.keys[2][i].value/V3C2.keys[2][i].value,Mathf.Lerp(V3C1.keys[2][i].inTangent,V3C2.keys[2][i].inTangent,.5f),Mathf.Lerp(V3C1.keys[2][i].outTangent,V3C2.keys[2][i].outTangent,.5f)));
}
rtn.x.keys = xkeys.ToArray();
rtn.y.keys = ykeys.ToArray();
rtn.z.keys = zkeys.ToArray();
return rtn;
}
public static Vector3Curve operator +(Vector3Curve V3C1,float num){
Vector3Curve rtn = new Vector3Curve();
List<Keyframe> xkeys = new List<Keyframe>();
List<Keyframe> ykeys = new List<Keyframe>();
List<Keyframe> zkeys = new List<Keyframe>();
for(int i=0; i<V3C1.keys[0].Length;i++){
xkeys.Add(new Keyframe(V3C1.keys[0][i].time,V3C1.keys[0][i].value+num,V3C1.keys[0][i].inTangent,V3C1.keys[0][i].outTangent));
ykeys.Add(new Keyframe(V3C1.keys[1][i].time,V3C1.keys[1][i].value+num,V3C1.keys[1][i].inTangent,V3C1.keys[1][i].outTangent));
zkeys.Add(new Keyframe(V3C1.keys[2][i].time,V3C1.keys[2][i].value+num,V3C1.keys[2][i].inTangent,V3C1.keys[2][i].outTangent));
}
rtn.x.keys = xkeys.ToArray();
rtn.y.keys = ykeys.ToArray();
rtn.z.keys = zkeys.ToArray();
return rtn;
}
public static Vector3Curve operator -(Vector3Curve V3C1,float num){
Vector3Curve rtn = new Vector3Curve();
List<Keyframe> xkeys = new List<Keyframe>();
List<Keyframe> ykeys = new List<Keyframe>();
List<Keyframe> zkeys = new List<Keyframe>();
for(int i=0; i<V3C1.keys[0].Length;i++){
xkeys.Add(new Keyframe(V3C1.keys[0][i].time,V3C1.keys[0][i].value-num,V3C1.keys[0][i].inTangent,V3C1.keys[0][i].outTangent));
ykeys.Add(new Keyframe(V3C1.keys[1][i].time,V3C1.keys[1][i].value-num,V3C1.keys[1][i].inTangent,V3C1.keys[1][i].outTangent));
zkeys.Add(new Keyframe(V3C1.keys[2][i].time,V3C1.keys[2][i].value-num,V3C1.keys[2][i].inTangent,V3C1.keys[2][i].outTangent));
}
rtn.x.keys = xkeys.ToArray();
rtn.y.keys = ykeys.ToArray();
rtn.z.keys = zkeys.ToArray();
return rtn;
}
public static Vector3Curve operator *(Vector3Curve V3C1,float num){
Vector3Curve rtn = new Vector3Curve();
List<List<Keyframe>> keys = new List<List<Keyframe>>();
for(int d=0; d<3;d++){
keys.Add(new List<Keyframe>());
for(int i=0; i<V3C1.keys[d].Length;i++){
keys[d].Add(new Keyframe(V3C1.keys[d][i].time,V3C1.keys[d][i].value*num,V3C1.keys[d][i].inTangent,V3C1.keys[d][i].outTangent));
}
}
rtn.x.keys = keys[0].ToArray();
rtn.y.keys = keys[1].ToArray();
rtn.z.keys = keys[2].ToArray();
return rtn;
}
public static Vector3Curve operator /(Vector3Curve V3C1,float num){
Vector3Curve rtn = new Vector3Curve();
List<Keyframe> xkeys = new List<Keyframe>();
List<Keyframe> ykeys = new List<Keyframe>();
List<Keyframe> zkeys = new List<Keyframe>();
for(int i=0; i<V3C1.keys[0].Length;i++){
xkeys.Add(new Keyframe(V3C1.keys[0][i].time,V3C1.keys[0][i].value/num,V3C1.keys[0][i].inTangent,V3C1.keys[0][i].outTangent));
ykeys.Add(new Keyframe(V3C1.keys[1][i].time,V3C1.keys[1][i].value/num,V3C1.keys[1][i].inTangent,V3C1.keys[1][i].outTangent));
zkeys.Add(new Keyframe(V3C1.keys[2][i].time,V3C1.keys[2][i].value/num,V3C1.keys[2][i].inTangent,V3C1.keys[2][i].outTangent));
}
rtn.x.keys = xkeys.ToArray();
rtn.y.keys = ykeys.ToArray();
rtn.z.keys = zkeys.ToArray();
return rtn;
}
#endregion Operators
}
[System.Serializable]public class Vector4Int: System.IEquatable<Vector4Int>{
public int x;
public int y;
public int z;
public int w;
#region Constructors
public Vector4Int(int X, int Y, int Z, int W){
string dpName = "Vector4Int";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
x = X;
y = Y;
z = Z;
w = W;
}
public Vector4Int(int[] dim){
string dpName = "Vector4Int";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
x = dim[0];
y = dim[1];
z = dim[2];
w = dim[3];
}
public Vector4Int(Vector4Int V4I){
string dpName = "Vector4Int";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
this.x = V4I.x;
this.y = V4I.y;
this.z = V4I.z;
this.w = V4I.w;
}
#endregion Constructors
#region Arithmetic Operators
public static Vector4Int operator +(Vector4Int V1,Vector4Int V2){return new Vector4Int(V1.x+V2.x,V1.y+V2.y,V1.z+V2.z,V1.w+V2.w);}
public static Vector4Int operator +(Vector4Int V4I,Vector3Int V3I){return new Vector4Int(V4I.x+V3I.x,V4I.y+V3I.y,V4I.z+V3I.z,V4I.w);}
public static Vector4Int operator +(Vector4Int V4I,Vector2Int V2I){return new Vector4Int(V4I.x+V2I.x,V4I.y+V2I.y,V4I.z,V4I.w);}
public static Vector4Int operator -(Vector4Int V1,Vector4Int V2){return new Vector4Int(V1.x-V2.x,V1.y-V2.y,V1.z-V2.z,V1.w-V2.w);}
public static Vector4Int operator -(Vector4Int V4I,Vector3Int V3I){return new Vector4Int(V4I.x-V3I.x,V4I.y-V3I.y,V4I.z-V3I.z,V4I.w);}
public static Vector4Int operator -(Vector4Int V4I,Vector2Int V2I){return new Vector4Int(V4I.x-V2I.x,V4I.y-V2I.y,V4I.z,V4I.w);}
public static Vector4Int operator *(Vector4Int V1,Vector4Int V2){return new Vector4Int(V1.x*V2.x,V1.y*V2.y,V1.z*V2.z,V1.w*V2.w);}
public static Vector4Int operator *(Vector4Int V1,Vector3Int V2){return new Vector4Int(V1.x*V2.x,V1.y*V2.y,V1.z*V2.z,V1.w);}
public static Vector4Int operator *(Vector4Int V1,Vector2Int V2){return new Vector4Int(V1.x*V2.x,V1.y*V2.y,V1.z,V1.w);}
public static Vector4Int operator *(Vector4Int V1,int Int){return new Vector4Int(V1.x*Int,V1.y*Int,V1.z*Int,V1.w*Int);}
public static Vector4Int operator /(Vector4Int V1,Vector4Int V2){return new Vector4Int(V1.x/V2.x,V1.y/V2.y,V1.z/V2.z,V1.w/V2.w);}
public static Vector4Int operator /(Vector4Int V1,Vector3Int V2){return new Vector4Int(V1.x/V2.x,V1.y/V2.y,V1.z/V2.z,V1.w);}
public static Vector4Int operator /(Vector4Int V1,Vector2Int V2){return new Vector4Int(V1.x/V2.x,V1.y/V2.y,V1.z,V1.w);}
public static Vector4Int operator /(Vector4Int V1,int Int){return new Vector4Int(V1.x/Int,V1.y/Int,V1.z/Int,V1.w/Int);}
#endregion Arithmetic Operators
#region Comparison Operators
public static bool operator ==(Vector4Int V1,object obj){
if(obj == null)return false;
if(obj.GetType()==typeof(Vector4Int)){
Vector4Int V2 = obj as Vector4Int;
return V1.x==V2.x&&V1.y==V2.y&&V1.z==V2.z&&V1.w==V2.w;
}
return false;
}
public static bool operator !=(Vector4Int V1,object obj){
if(obj == null)return true;
if(obj.GetType()==typeof(Vector4Int)){
Vector4Int V2 = obj as Vector4Int;
return V1.x!=V2.x&&V1.y!=V2.y&&V1.z!=V2.z&&V1.w!=V2.w;
}
return true;
}
public bool Equals(Vector4Int V4){
return this.x!=V4.x&&this.y!=V4.y&&this.z!=V4.z&&this.w!=V4.w;
}
public override bool Equals(object obj){
if(obj==null)return false;
return Equals(obj as Vector4Int);
}
#endregion Comparison Operators
#region Conversion Operators
//Convert to other Vector
public static implicit operator Vector2Int(Vector4Int V4I){return new Vector2Int(V4I.x, V4I.y);}
public static implicit operator Vector2(Vector4Int V4I){return new Vector2(V4I.x, V4I.y);}
public static implicit operator Vector3Int(Vector4Int V4I){return new Vector3Int(V4I.x, V4I.y,V4I.z);}
public static implicit operator Vector3(Vector4Int V4I){return new Vector3(V4I.x, V4I.y,V4I.z);}
public static implicit operator Vector4(Vector4Int V4I){return new Vector4(V4I.x, V4I.y,V4I.z,V4I.w);}
//Convert from other Vector
public static implicit operator Vector4Int(Vector2Int V4I){return new Vector4Int(V4I.x, V4I.y, 0, 0);}
public static implicit operator Vector4Int(Vector3Int V4I){return new Vector4Int(V4I.x, V4I.y,V4I.z, 0);}
//Serialize and Deserialize
public static implicit operator int[](Vector4Int V4I){return new int[]{V4I.x,V4I.y,V4I.z,V4I.w};}
public static implicit operator Vector4Int(int[] array){return new Vector4Int(array[0],array[1],array[2],array[3]);}

public static implicit operator string(Vector4Int V4I){return "("+V4I.x+","+V4I.y+","+V4I.z+","+V4I.w+")";}
public override string ToString(){
return "("+x+","+y+","+z+","+w+")";
}
#endregion Conversion Operators
#region Functions
#endregion Functions
}
[System.Serializable]public class FontData{
public Color color;
public float size;
public TMPro.TMP_FontAsset font;
public TMPro.TextAlignmentOptions alignment;
public static TMPro.TMP_FontAsset defaultFont{get{
if(_defaultFont==null){
string[] fontPaths = Font.GetPathsToOSFonts();
Font osFont = new Font(fontPaths[0]);
_defaultFont = TMPro.TMP_FontAsset.CreateFontAsset(osFont);
}
return _defaultFont;
}}
static TMPro.TMP_FontAsset _defaultFont;

public FontData(){
string dpName = "FontData";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
size = 12;
font = defaultFont;
alignment = TMPro.TextAlignmentOptions.Center;
this.color = Color.black;
}
public FontData(float Size,TMPro.TMP_FontAsset font,TMPro.TextAlignmentOptions Alignment=TMPro.TextAlignmentOptions.Center){
string dpName = "FontData";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
size = Size;
this.font = font;
alignment = Alignment;
color = Color.black;
}
public FontData(float Size,Color color,TMPro.TextAlignmentOptions Alignment=TMPro.TextAlignmentOptions.Center){
string dpName = "FontData";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
size = Size;
font = defaultFont;
alignment = Alignment;
this.color = color;
}
public FontData(float Size,TMPro.TMP_FontAsset font,Color color,TMPro.TextAlignmentOptions Alignment=TMPro.TextAlignmentOptions.Center){
string dpName = "FontData";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
size = Size;
this.font = font;
alignment = Alignment;
this.color = color;
}
}
[System.Serializable]public class GizmoData{
public Color color = Color.black;
public Vector3 position;
public Vector3 size;
public float radius;
public float time;
public enum Type{
Sphere,
Ray
}
public Type type;
public GizmoData(Vector3 Position,float Radius){
this.position = Position;
this.radius = Radius;
string dpName = "GizmoData";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
}
public GizmoData(Vector3 Position,Vector3 Size){
this.position = Position;
this.size = Size;
string dpName = "GizmoData";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
}
public void Draw(){
Color gc = Gizmos.color;
Gizmos.color = this.color;
switch(type){
case Type.Ray:
Gizmos.DrawRay(position,size);
break;
case Type.Sphere:
Gizmos.DrawSphere(position,radius);
break;
}
Gizmos.color = gc;
}
public void Draw(Color color){
Color gc = Gizmos.color;
Gizmos.color = color;
switch(type){
case Type.Ray:
Gizmos.DrawRay(position,size);
break;
case Type.Sphere:
Gizmos.DrawSphere(position,radius);
break;
}
Gizmos.color = gc;
}

}
[System.Serializable]public class Quad<T>{
public Quad(){
string dpName = "Quad";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
}
public T Default;
#region Data
public List<ListWrapper<T>> NN = new List<ListWrapper<T>>();
public List<T> ZN = new List<T>();
public List<ListWrapper<T>> PN = new List<ListWrapper<T>>();
public List<T> NZ = new List<T>();
public T Origin;
public List<T> PZ = new List<T>();
public List<ListWrapper<T>> PP = new List<ListWrapper<T>>();
public List<T> ZP = new List<T>();
public List<ListWrapper<T>> NP = new List<ListWrapper<T>>();
#endregion Data
//Indexers
public T this[int x,int y]{
get{
int X = Mathf.Abs(x);
int Y = Mathf.Abs(y);
#region Negative X
//NY
if(x<0){
//NN
if(y<0){
if(X>NN.Count)return (T)Default;
if(Y>NN[X].Count)return (T)Default;
return NN[X][Y];
}
//NZ
if(y==0){
if(X>NZ.Count)return (T)Default;
return NZ[X];
}
//NP
if(y>0){
if(X>NP.Count)return (T)Default;
if(Y>NP[X].Count)return (T)Default;
return NP[X][Y];
}
}
#endregion Negative X
#region Zero X
//ZY
if(x==0){
//ZN
if(y<0){
if(Y>ZN.Count)return (T)Default;
return ZN[Y];
}
//ZZ
if(y==0){
return Origin;
}
//ZP
if(y>0){
if(Y>ZP.Count)return (T)Default;
return ZP[Y];
}
}
#endregion Zero X
#region Positive X
//PY
if(x>0){
//PN
if(y<0){
if(X>PN.Count)return (T)Default;
if(Y>PN[X].Count)return (T)Default;
return PN[X][Y];
}
//PZ
if(y==0){
if(X>PZ.Count)return (T)Default;
return PZ[X];
}
//PP
if(y>0){
if(X>PP.Count)return (T)Default;
if(Y>PP[X].Count)return (T)Default;
return PP[X][Y];
}
}
#endregion Positive X
Debug.Log("Unknown Error, returning Origin:"+Origin);
return Origin;
}
set{
bool set = false;
int X = Mathf.Abs(x);
int Y = Mathf.Abs(y);
#region Negative X
//NY
if(x<0){
//NN
if(y<0){
fillGaps(new Vector2Int(X,Y));
NN[X][Y] = value;
set = true;
}
//NZ
if(y==0){
fillGaps(new Vector2Int(X,Y));
NZ[X] = value;
set = true;
}
//NP
if(y>0){
fillGaps(new Vector2Int(X,Y));
NP[X][Y] = value;
set = true;
}
}
#endregion Negative X
#region Zero X
//ZY
if(x==0){
//ZN
if(y<0){
fillGaps(new Vector2Int(X,Y));
ZN[Y] = value;
set = true;
}
//ZZ
if(y==0){
Origin = value;
set = true;
}
//ZP
if(y>0){
fillGaps(new Vector2Int(X,Y));
ZP[Y] = value;
set = true;
}
}
#endregion Zero X
#region Positive X
//PY
if(x>0){
//PN
if(y<0){
fillGaps(new Vector2Int(X,Y));
PN[X][Y] = value;
set = true;
}
//PZ
if(y==0){
fillGaps(new Vector2Int(X,Y));
PZ[X] = value;
set = true;
}
//PP
if(y>0){
fillGaps(new Vector2Int(X,Y));
PP[X][Y] = value;
set = true;
}
}
#endregion Positive X
if(!set)Debug.Log("Unknown Error, no values set");
}
}
public T this[Vector2Int position]{
get{
Vector2Int Position = position.Abs();
#region Negative X
//NY
if(position.x<0){
//NN
if(position.y<0){
fillGaps(position);
return NN[Position.x][Position.y];
}
//NZ
if(position.y ==0){
fillGaps(position);
return NZ[Position.x];
}
//NP
if(position.y >0){
fillGaps(position);
return NP[Position.x][Position.y];
}
}
#endregion Negative X
#region Zero X
//ZY
if(position.x ==0){
//ZN
if(position.y <0){
fillGaps(position);
return ZN[Position.y];
}
//ZZ
if(position.y ==0){
return Origin;
}
//ZP
if(position.y >0){
fillGaps(position);
return ZP[Position.y];
}
}
#endregion Zero X
#region Positive X
//PY
if(position.x >0){
//PN
if(position.y <0){
fillGaps(position);
return PN[Position.x][Position.y];
}
//PZ
if(position.y ==0){
fillGaps(position);
return PZ[Position.x];
}
//PP
if(position.y >0){
fillGaps(position);
return PP[Position.x][Position.y];
}
}
#endregion Positive X
Debug.Log("Unknown Error, returning Origin:"+Origin);
return Origin;
}
set{ 
bool set = false;
Vector2Int Position = position.Abs();
#region Negative X
//NY
if(position.x<0){
//NN
if(position.y<0){
NN[Position.x][Position.y] = value;
set = true;
}
//NZ
if(position.y ==0){
NZ[Position.x] = value;
set = true;
}
//NP
if(position.y >0){
NP[Position.x][Position.y] = value;
set = true;
}
}
#endregion Negative X
#region Zero X
//ZY
if(position.x ==0){
//ZN
if(position.y <0){
ZN[Position.y] = value;
set = true;
}
//ZZ
if(position.y ==0){
Origin = value;
set = true;
}
//ZP
if(position.y >0){
ZP[Position.y] = value;
set = true;
}
}
#endregion Zero X
#region Positive X
//PY
if(position.x >0){
//PN
if(position.y <0){
PN[Position.x][Position.y] = value;
set = true;
}
//PZ
if(position.y ==0){
PZ[Position.x] = value;
set = true;
}
//PP
if(position.y >0){
PP[Position.x][Position.y] = value;
set = true;
}
}
#endregion Positive X
if(!set)Debug.Log("Unknown Error, no values set");
}
}
public void fillGaps(Vector2Int position){
Vector2Int Position = position.Abs();
#region Negative X
if(position.x<0){
#region NN
if(position.y<0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>NN.Count-1){
NN.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>NN[_x].Count-1){
NN[_x].Add((T)Default);
}
}
}
}
#endregion NN
#region NZ
if(position.y==0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>NZ.Count-1){
NZ.Add((T)Default);
}
}
}
#endregion NZ
#region NP
if(position.y >0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>NP.Count){
NP.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>NP[_x].Count){
NP[_x].Add((T)Default);
}
}
}
}
#endregion NP
}
#endregion Negative X
#region Zero X
if(position.x ==0){
#region ZN
if(position.y <0){
for(int _y=0; _y<Position.y+1;_y++){
if(_y>ZN.Count-1){
ZN.Add((T)Default);
}
}
}
#endregion ZN
#region ZZ
/*
if(position.y ==0){
Origin = (T)Default;
}
*/
#endregion ZZ
#region ZP
if(position.y >0){
for(int _y=0; _y<Position.y+1;_y++){
if(_y>ZP.Count-1){
ZP.Add((T)Default);
}
}
}
#endregion ZP
}
#endregion Zero X
#region Positive X
if(position.x >0){
#region PN
if(position.y <0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>PN.Count-1){
PN.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>PN[_x].Count-1){
PN[_x].Add((T)Default);
}
}
}
}
#endregion PN
#region PZ
if(position.y ==0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>PZ.Count-1){
PZ.Add((T)Default);
}
}
}
#endregion PZ
#region PP
if(position.y >0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>PP.Count-1){
PP.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>PP[_x].Count-1){
PP[_x].Add((T)Default);
}
}
}
}
#endregion PP
}
#endregion Positive X
}
public void fillGaps(int x,int y){
Vector2Int position = new Vector2Int(x,y);
Vector2Int Position = position.Abs();
#region Negative X
if(position.x<0){
#region NN
if(position.y<0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>NN.Count-1){
NN.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>NN[_x].Count-1){
NN[_x].Add((T)Default);
}
}
}
}
#endregion NN
#region NZ
if(position.y==0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>NZ.Count-1){
NZ.Add((T)Default);
}
}
}
#endregion NZ
#region NP
if(position.y >0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>NP.Count){
NP.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>NP[_x].Count){
NP[_x].Add((T)Default);
}
}
}
}
#endregion NP
}
#endregion Negative X
#region Zero X
if(position.x ==0){
#region ZN
if(position.y <0){
for(int _y=0; _y<Position.y+1;_y++){
if(_y>ZN.Count-1){
ZN.Add((T)Default);
}
}
}
#endregion ZN
#region ZZ
/*
if(position.y ==0){
Origin = (T)Default;
}
*/
#endregion ZZ
#region ZP
if(position.y >0){
for(int _y=0; _y<Position.y+1;_y++){
if(_y>ZP.Count-1){
ZP.Add((T)Default);
}
}
}
#endregion ZP
}
#endregion Zero X
#region Positive X
if(position.x >0){
#region PN
if(position.y <0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>PN.Count-1){
PN.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>PN[_x].Count-1){
PN[_x].Add((T)Default);
}
}
}
}
#endregion PN
#region PZ
if(position.y ==0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>PZ.Count-1){
PZ.Add((T)Default);
}
}
}
#endregion PZ
#region PP
if(position.y >0){
for(int _x=0; _x<Position.x+1;_x++){
if(_x>PP.Count-1){
PP.Add(new ListWrapper<T>());
}
for(int _y=0; _y<Position.y+1;_y++){
if(_y>PP[_x].Count-1){
PP[_x].Add((T)Default);
}
}
}
}
#endregion PP
}
#endregion Positive X
}
}
[System.Serializable]public class ProcessWatcher{
public MonoBehaviour parent;
public float lastCheckTime;
public float logSpeed = 1;
public float completion;
float lastCompletion;
public List<string> processes = new List<string>();
public List<float> processesCompletion = new List<float>();
public bool loggerRunning = false;
public void Report(string id,float value){
if(!loggerRunning){
lastCheckTime = Time.time;
parent.SendMessage("Run",Logger());
}
bool processExists = false;
float totalProgress = 0;
//search through every process
for(int i=0; i<processes.Count;i++){
//if found process
if(id==processes[i]){
if(value==100){
processes.RemoveAt(i);
processesCompletion.RemoveAt(i);
}else{
processesCompletion[i]=value;
totalProgress += value;
processExists = true;
}
}else{ 
totalProgress += processesCompletion[i];
}
}
//if process doesn't exist, add it
if(!processExists){
processes.Add(id);
processesCompletion.Add(value);
totalProgress += value;
}
completion = totalProgress/processes.Count;
}
public IEnumerator Logger(){
loggerRunning = true;
while(completion<100&&processes.Count>0){
if(lastCheckTime+logSpeed < Time.time&&lastCompletion!=completion){
Debug.Log(completion);
lastCheckTime = Time.time;
lastCompletion = completion;
}
yield return null;
}
loggerRunning = false;
}
public ProcessWatcher(MonoBehaviour Parent){
string dpName = "ProcessWatcher";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
parent = Parent;
}
}
[System.Serializable]public class AnimatedImage{
#region AnimatedImage Variables
public List<Sprite> images;
//Time
public float frameLength;
public Vector3 Size = Vector3.one;
public Vector3 Position;
public Sprite this[int index]{get{ 
return images[index];
}}
public GameObject gameObject;
public RectTransform tf{get{
return (gameObject!=null)?(gameObject.GetComponent<RectTransform>()!=null)?gameObject.GetComponent<RectTransform>():null:null;
}}
[HideInInspector]public bool setTF = false;
public string name;
public int loopCount = 1;
public string loopBehaviour = "Loop";
public AnimatedImage changeTo;
#endregion AnimatedImage Variables
public void Transform(Vector3 size,Vector3 position){
Size = size;
Position = position;
if(tf != null){
tf.sizeDelta = Size;
tf.localPosition = Position;
}else{ 
setTF = true;
}
}
public void Destroy(){
GameObject.Destroy(gameObject);
}
public void Create(GameObject go,bool isChildOfGameObject=true){
if(isChildOfGameObject){
gameObject = new GameObject(name);
gameObject.transform.parent = go.transform;
gameObject.AddComponent<ImageAnimator>().image = this;
}else{
gameObject = go;
go.AddComponent<ImageAnimator>().image = this;
}
}
public class ImageAnimator : MonoBehaviour {
#region ImageAnimator Variables
public AnimatedImage image;
public int frame = 0;
public float startTime;
public float totalTime{get{return image.frameLength*image.images.Count;}}

public int loop{get{return Mathf.FloorToInt(timeEllapsed/totalTime);}}
public float timeEllapsed{get{return Time.time-startTime;}}
public UnityEngine.UI.Image Image;
public RectTransform tf;
#endregion ImageAnimator Variables
public void Start(){
startTime = Time.time;
frame = 0;
Image = gameObject.AddComponent<UnityEngine.UI.Image>();
tf = gameObject.GetComponent<RectTransform>();
}
public void Update(){
if(image.setTF){
tf.sizeDelta = image.Size;
tf.localPosition = image.Position;
}
Image.sprite = image[Mathf.FloorToInt((timeEllapsed%totalTime)/image.frameLength)];
if(loop>=image.loopCount){
startTime = Time.time;
frame = 0;
switch(image.loopBehaviour.ToLower()){
case "destroy":
image.Destroy();
break;
case "change image":
if(image.changeTo==null){
Debug.Log("image.changeTo is null, looping image instead");
}else{
startTime = Time.time;
image.changeTo.gameObject = gameObject;
image = image.changeTo;
}
break;
}//switch loopBehaviour
}

}
}
public AnimatedImage(List<Sprite> Images,float FrameLength,string Name="AnimatedImage"){
string dpName = "AnimatedImage";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
images = Images;
frameLength = FrameLength;
name = Name;
}
}
[System.Serializable]public class LineSegment{
public Vector3 start;
public Vector3 end;
public float length{get{
return (end-start).magnitude;
}}
public LineSegment(){
string dpName = "Cube";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
}
}
[System.Serializable]public class Ranger<T>{
//Used for getting ranges of numeral values over time
//Use Ranger.Range(value) to set the min and max values of the ranger
[Header("Numbers")]
public int min_int;
public int max_int;
public double min_double;
public double max_double;
public float min_float;
public float max_float;
[Header("Vectors")]
public Vector2 min_vector2;
public Vector2 max_vector2;
public Vector2Int min_vector2int;
public Vector2Int max_vector2int;
public Vector3 min_vector3;
public Vector3 max_vector3;
public Vector3Int min_vector3int;
public Vector3Int max_vector3int;
public T min;
public T max;
public bool updated{get{
bool temp = _updated;
_updated = false;
return temp;
}}
bool _updated;
System.Type type;
public void Range(T value){
#region Number
if(type==typeof(int)){
if(min_int>(int)(object)value){min_int = (int)(object)value;_updated=true;}
if(max_int<(int)(object)value){max_int = (int)(object)value;_updated=true;}
}
if(type==typeof(double)){
if(min_double>(double)(object)value){min_double = (double)(object)value;_updated=true;}
if(max_double<(double)(object)value){max_double = (double)(object)value;_updated=true;}
}
if(type==typeof(float)){
if(min_float>(float)(object)value){min_float = (float)(object)value;_updated=true;}
if(max_float<(float)(object)value){max_float = (float)(object)value;_updated=true;}
}
#endregion Number
#region Vector
if(type==typeof(Vector2Int)){
Vector2Int Value = (Vector2Int)(object)value;
Vector2Int minLast = min_vector2int;
Vector2Int maxLast = max_vector2int;
min_vector2int = new Vector2Int(Value.x<min_vector2int.x?Value.x:min_vector2int.x,Value.y<min_vector2int.y?Value.y:min_vector2int.y);
max_vector2int = new Vector2Int(Value.x>max_vector2int.x?Value.x:max_vector2int.x,Value.y>max_vector2int.y?Value.y:max_vector2int.y);
if(minLast!=min_vector2int||maxLast!=max_vector2int){
_updated = true;
}
}
if(type==typeof(Vector2)){
Vector2 Value = (Vector2)(object)value;
Vector2 minLast = min_vector2;
Vector2 maxLast = max_vector2;
min_vector2 = new Vector2(Value.x<min_vector2.x?Value.x:min_vector2.x,Value.y<min_vector2.y?Value.y:min_vector2.y);
max_vector2 = new Vector2(Value.x>max_vector2.x?Value.x:max_vector2.x,Value.y>max_vector2.y?Value.y:max_vector2.y);
if(minLast!=min_vector2||maxLast!=max_vector2){
_updated = true;
}
}
if(type==typeof(Vector3Int)){
Vector3Int Value = (Vector3Int)(object)value;
Vector3Int minLast = min_vector3int;
Vector3Int maxLast = max_vector3int;
min_vector3int = new Vector3Int(Value.x<min_vector3int.x?Value.x:min_vector3int.x,Value.y<min_vector3int.y?Value.y:min_vector3int.y,Value.z<min_vector3int.z?Value.z:min_vector3int.z);
max_vector3int = new Vector3Int(Value.x>max_vector3int.x?Value.x:max_vector3int.x,Value.y>max_vector3int.y?Value.y:max_vector3int.y,Value.z>max_vector3int.z?Value.z:max_vector3int.z);
if(minLast!=min_vector3int||maxLast!=max_vector3int){
_updated = true;
}
}
if(type==typeof(Vector3)){
Vector3 Value = (Vector3)(object)value;
Vector3 minLast = min_vector3;
Vector3 maxLast = max_vector3;
min_vector3 = new Vector3(Value.x<min_vector3.x?Value.x:min_vector3.x,Value.y<min_vector3.y?Value.y:min_vector3.y,Value.z<min_vector3.z?Value.z:min_vector3.z);
max_vector3 = new Vector3(Value.x>max_vector3.x?Value.x:max_vector3.x,Value.y>max_vector3.y?Value.y:max_vector3.y,Value.z>max_vector3.z?Value.z:max_vector3.z);
if(minLast!=min_vector3||maxLast!=max_vector3){
_updated = true;
}
}
#endregion Vector
}
public void Log(){
#region Number
if(type==typeof(int))Debug.Log(min_int+","+max_int);
if(type==typeof(double))Debug.Log(min_double+","+max_double);
if(type==typeof(float))Debug.Log(min_float+","+max_float);
#endregion Number
#region Vector
if(type==typeof(Vector2))Debug.Log(min_vector2+","+max_vector2);
if(type==typeof(Vector2Int))Debug.Log(min_vector2int+","+max_vector2int);
if(type==typeof(Vector3))Debug.Log(min_vector3+","+max_vector3);
if(type==typeof(Vector3Int))Debug.Log(min_vector3int+","+max_vector3int);
#endregion Vector
}
public Ranger(){
type = typeof(T);
#region Number
if(type==typeof(int)){
min_int = FF.Int_Largest;
max_int = FF.Int_Largest*-1;
}
if(type==typeof(double)){
min_double = Mathf.Infinity;
max_double = Mathf.Infinity*-1;
}
if(type==typeof(float)){
min_float = Mathf.Infinity;
max_float = Mathf.Infinity*-1;
}
#endregion Number
#region Vector
if(type==typeof(Vector2)){
min_vector2 = Vector2.one*Mathf.Infinity;
max_vector2 = Vector2.one*Mathf.Infinity*-1;
}
if(type==typeof(Vector2Int)){
min_vector2int = Vector2Int.one*FF.Int_Largest;
max_vector2int = Vector2Int.one*FF.Int_Largest*-1;
}
if(type==typeof(Vector3)){
min_vector3 = Vector3.one*Mathf.Infinity;
max_vector3 = Vector3.one*Mathf.Infinity*-1;
}
if(type==typeof(Vector3Int)){
min_vector3int = Vector3Int.one*FF.Int_Largest;
max_vector3int = Vector3Int.one*FF.Int_Largest*-1;
}
#endregion Vector
}
}
#region Data Wrappers
[System.Serializable]public class WeightedList<T>: IEnumerable<T>{
public List<listItem<T>> items = new List<listItem<T>>();
public float Range{get{
float rtn = 0;
foreach(listItem<T> li in items){
rtn += li.weight;
}
return rtn;
}}
public int Count{get{return items.Count;}}
public listItem<T> this[int i]{
get{return items[i];}
set{items[i] = value;}
}
public T Get(){
float num = Random.Range(0,Range);
for(int i=0; i<items.Count;i++){
num -= items[i].weight;
if(num<0){
return items[i].value;
}
}
Debug.Log("Error! returning items[0]");
return items[0].value;
}
public T GetInRange(float value){
float num = value;
for(int i=0; i<items.Count;i++){
num -= items[i].weight;
if(num<0){
return items[i].value;
}
}
Debug.Log("Error! returning items[0]");
return items[0].value;
}
public void Add(T value,float weight=0){
items.Add(new listItem<T>(value,weight));
}
public bool Contains(T value){
for(int i=0; i<items.Count;i++){
if(EqualityComparer<T>.Default.Equals(items[i],value)){
return true;
}
}
return false;
}
public int IndexOf(T value){
for(int i=0; i<items.Count;i++){
if(EqualityComparer<T>.Default.Equals(items[i],value))return i;
}
return -1;
}
public void Remove(T value){
for(int i=0; i<items.Count;i++){
if(EqualityComparer<T>.Default.Equals(items[i],value))items.RemoveAt(i);
}
}
public void RemoveAt(int i){
items.RemoveAt(i);
}
[System.Serializable]public class listItem<T>{
string name;
public T value;
public float weight;
public listItem(T value,float weight){
this.value = value;
this.weight = weight;
this.name = value.ToString();
}
public listItem(){
}
public static implicit operator T(listItem<T> This){
return This.value;
}
}
#region Enumerable
public IEnumerator<T> GetEnumerator(){
List<T> list = new List<T>();
foreach(listItem<T> li in items){
list.Add(li.value);
}
return list.GetEnumerator();
}
IEnumerator IEnumerable.GetEnumerator(){
return GetEnumerator();
}
#endregion Enumerable
}
[System.Serializable]public class NIArray<T>{
T _default;
public T[] Positive;
public T Zero;
public T[] Negative;
public int Length{get{
return Positive.Length+Negative.Length+1;
}}
public int PosLength{get{return Positive.Length;}}
public int NegLength{get{return Negative.Length;}}
public T this[int i]{
get{
if(i>0)return Positive[i-1];
if(i<0)return Negative[i-1];
return Zero;
}
set{
if(i>0)Positive[i-1] = value;
if(i<0)Negative[i-1] = value;
Zero = value;
}
}
#region Constructors
public NIArray(int length,T Default){
_default = Default;
Positive = new T[length];
Zero = Default;
Negative = new T[length];
for(int i=0; i<Positive.Length;i++){
Positive[i] = Default;
}
for(int i=0; i<Negative.Length;i++){
Negative[i] = Default;
}
}
public NIArray(int length){
Positive = new T[length];
Negative = new T[length];
}
#endregion Constructors
}
[System.Serializable]public class NIArray2D<T>{
//X Contains Y
T _default;
public NIArray<NIArray<T>> value;
#region return dimensions
public Vector2Int Size{get{return new Vector2Int(value.Length,value[0].Length);}}
public int Width{get{return value.Length;}}
public int Height{get{return value[0].Length;}}
#endregion return dimensions
#region Indexer
public T this[int x,int y]{
get{
return value[x-1][y-1];
}
set{
this.value[x-1][y-1] = value;
}
}
#endregion Indexer
#region Constructors
public NIArray2D(int width,int height,T Default){
_default = Default;
NIArray<T> def = new NIArray<T>(height,_default);
value = new NIArray<NIArray<T>>(width,def);
for(int i=0; i<value.Length;i++){
value[i] = new NIArray<T>(height,_default);
}
}
public NIArray2D(int width,int height){
NIArray<T> def = new NIArray<T>(height,_default);
value = new NIArray<NIArray<T>>(width,def);
for(int i=0; i<value.Length;i++){
value[i] = new NIArray<T>(height,_default);
}
}
#endregion Constructors
}
[System.Serializable]public class NIArray3D<T>{
//Z Contains X Contains Y
T _default;
public NIArray<NIArray2D<T>> value;
#region return dimensions
public Vector3Int Size{get{return new Vector3Int(value[0].Width,value[0].Height,value.Length);}}
public int Width{get{return value[0].Width;}}
public int Height{get{return value[0].Height;}}
public int Depth{get{return value.Length;}}
#endregion return dimensions
#region Indexer
public T this[int x,int y,int z]{
get{
return value[z-1][x-1,y-1];
}
set{
this.value[z-1][x-1,y-1] = value;
}
}
#endregion Indexer
#region Constructors
public NIArray3D(int width,int height,int depth,T Default){
_default = Default;
NIArray2D<T> def = new NIArray2D<T>(width,height,_default);

value = new NIArray<NIArray2D<T>>(depth,def);
for(int i=0; i<value.Length;i++){
value[i] = new NIArray2D<T>(width,height,_default);
}
}
public NIArray3D(int width,int height,int depth){
NIArray2D<T> def = new NIArray2D<T>(width,height,_default);

value = new NIArray<NIArray2D<T>>(depth,def);
for(int i=0; i<value.Length;i++){
value[i] = new NIArray2D<T>(width,height,_default);
}
}
#endregion Constructors
}
[System.Serializable]public class NIList<T>: IEnumerable<T>{
[HideInInspector]public string name;
T _default;
public List<T> Positive;
public T Zero;
public List<T> Negative;
public bool showDebug = false;
public List<T> All{get{
List<T> temp = new List<T>();
foreach(T t in Negative)temp.Add(t);
temp.Add(Zero);
foreach(T t in Positive)temp.Add(t);
return temp;
}}
#region Functions
public T Add(T val,int dir){
///dir(-1,0,1), returns index of added value
if(dir>0){Positive.Add(val);return Positive[Positive.Count-1];}
if(dir<0){
Negative.Add(val);
return Negative[Negative.Count-1];
}
//dir==0
Zero = val;
return Zero;
}
public bool Contains(T val){
if((object)Zero==(object)val)return true;
return (Positive.Contains(val)||Negative.Contains(val));
}
public int IndexOf(T val,out bool found){
found = true;
if((object)Zero==(object)val)return 0;
if(Negative.IndexOf(val)!=-1){
return Negative.IndexOf(val)*-1;
}else{

if(Positive.IndexOf(val)!=-1){
return Positive.IndexOf(val);
}else{
found = false;
return 0;
}

}
}
public void Clear(){
Positive.Clear();
Negative.Clear();
Zero = _default;
}
//T[negative,0,positive][list of elements]
public T[][] Serialize(){
T[][] rtn = new T[3][];
rtn[0] = Negative.ToArray();
rtn[1] = new T[]{Zero};
rtn[2] = Positive.ToArray();
return rtn;
}
public NIList<T> Deserialize(T[][] input){
Negative = new List<T>(input[0]);
Zero = input[1][0];
Positive = new List<T>(input[2]);
return this;
}
#endregion Functions
#region Get Dimensions
public int Count{get{
return Positive.Count+Negative.Count+1;
}}
public int PosLength{get{return Positive.Count;}}
public int NegLength{get{return Negative.Count;}}
#endregion Get Dimensions
#region Enumerable
public IEnumerator<T> GetEnumerator(){
return All.GetEnumerator();
}
IEnumerator IEnumerable.GetEnumerator(){
return GetEnumerator();
}
#endregion Enumerable
public T this[int i]{
get{
if(i>0){
if(i-1>=Positive.Count){
//Debug.Log("Index "+i+" is too big Returning default.");
return _default;
}
return Positive[i-1];
}
if(i<0){
if(Mathf.Abs(i)-1>=Negative.Count){
//Debug.Log("Index "+i+" is too small Returning default.");
return _default;
}
return Negative[Mathf.Abs(i)-1];
}
return Zero;
}
set{
bool valSet = false;
#region Fill Gaps
while(i>Positive.Count){
Positive.Add(_default);
//Debug.Log("List fill positive gap");
}
while(i*-1>Negative.Count){
//Debug.Log("List fill negative gap");
Negative.Add(_default);
}
#endregion Fill Gaps
if(i>0){
Positive[i-1] = value;
//Debug.Log("set ["+i+"] to "+value);
valSet = true;
}
if(i<0){
Negative[(i*-1)-1] = value;
valSet = true;
//Debug.Log("set ["+i+"] to "+value);
}
//Debug.Log("set [0]");
if(!valSet)Zero = value;
}
}
#region Constructors
public NIList(T Default,bool showDebug=true){
_default = Default;
//Debug.Log("Construct NIList with \""+_default+"\" as default");
Positive = new List<T>();
Negative = new List<T>();
Zero = _default;
this.showDebug = showDebug;
}
public NIList(){
Positive = new List<T>();
Negative = new List<T>();
}
#endregion Constructors
}
[System.Serializable]public class NIList2D<T> : IEnumerable<T>{
//Y Contains X
public T _default;
public NIList<NIList<T>> value;
public T Zero{get{return value[0].Zero;}set{this.value[0].Zero = value;}}
public NIList<T> ZeroList{get{return value[0];}set{this.value[0] = value;}}
public List<NIList<T>> Negative{get{
List<NIList<T>> rtn = new List<NIList<T>>();
foreach(NIList<T> list in value.Negative)rtn.Add(list);
return rtn;
}set{
this.value.Negative = value;
}}
public List<NIList<T>> Positive{get{
List<NIList<T>> rtn = new List<NIList<T>>();
foreach(NIList<T> list in value.Positive)rtn.Add(list);
return rtn;
}set{
this.value.Positive = value;
}}
public List<NIList<T>> AllLists{get{
List<NIList<T>> rtn = new List<NIList<T>>();
foreach(NIList<T> list in value.Negative)rtn.Add(list);
rtn.Add(value[0]);
foreach(NIList<T> list in value.Positive)rtn.Add(list);
return rtn;
}}
public List<T> AllElements{get{
List<T> rtn = new List<T>();
foreach(NIList<T> list in value.Negative)foreach(T elem in list)rtn.Add(elem);
foreach(T elem in ZeroList)rtn.Add(elem);
foreach(NIList<T> list in value.Positive)foreach(T elem in list)rtn.Add(elem);
return rtn;
}}
public bool showDebug = false;
#region return dimensions
public Vector2Int Size{get{return new Vector2Int(value.Count,Height);}}
public int Height{get{return value.Count;}}
public int Width{get{
if(value.Count>0){
int longest = 0;
foreach(NIList<T> list in value.Negative){
if(list.Count>longest)longest = list.Count;
}
if(value.Zero.Count>longest)longest = value.Zero.Count;
foreach(NIList<T> list in value.Positive){
if(list.Count>longest)longest = list.Count;
}
return longest;
}else{
return 0;
}
}}
#endregion return dimensions
#region Functions
//Add T Value
public NIList<T> Add(T val,Vector2Int dir){
///dir(-1,0,1), returns index of added value
//returning value.y
int y;
//x+
if(dir.y>0){
value.Positive.Add(new NIList<T>());
y = value.Positive.Count-1;
value.Positive[y].Add(val,dir.x);
return value.Positive[y];
//return new Vector2Int(y,value.Positive[value.Positive.Count-1].Count-1);
}
//x-
if(dir.y<0){
value.Negative.Add(new NIList<T>());
y = value.Negative.Count-1;
value.Negative[y].Add(val,dir.x);
return value.Negative[y];
//return new Vector2Int(y,value.Negative[value.Negative.Count-1].Count-1);
}
//x0
value.Zero.Add(val,dir.x);
return value.Zero;
//return new Vector2Int(0,value.Negative[value.Negative.Count-1].Count-1);
}
public NIList<T> Add(T val,int y,Vector2Int dir){
///dir(-1,0,1), returns index of added value
//x-
if(dir.y<0){
value.Negative[y].Add(val,dir.x);
return value.Negative[y];
}
//x+
if(dir.y>0){
value.Positive[y].Add(val,dir.x);
return value.Positive[y];
}
//x0
value.Zero.Add(val,dir.x);
return value.Zero;
}
//Add List
public int Add(NIList<T> val,int dir){
///dir(-1,0,1), returns index of added value
//x+
if(dir>0){
value.Positive.Add(val);
return value.Positive.Count - 1;
}
//x-
if(dir<0){
value.Negative.Add(val);
return value.Negative.Count - 1;
}
//x0
value.Zero = val;
return 0;
}
public bool Contains(T val){
if((object)value.Zero==(object)val)return true;
foreach(NIList<T> list in value){
if(list.Positive.Contains(val)||list.Negative.Contains(val))return true;
}
return false;
}
public Vector2Int IndexOf(T val,out bool found){
found = true;
if((object)Zero==(object)val)return new Vector2Int(0,0);

bool found2 = false;
//foreach list on y axis
for(int y=0; y<value.Count;y++){
//if in list, get y
int x = value[y].IndexOf(val,out found2);
//if is in list, return
if(found2)return new Vector2Int(x,y);
}
found = false;
return new Vector2Int(0,0);
}
public void Clear(){
foreach(NIList<T> list in value){
list.Positive.Clear();
list.Negative.Clear();
list.Zero = _default;
}
}
//T[2dlist negative,2dlist zero,2dlist positive][2dlist elements][list negative,list zero,list positive][list elements]
/*
T[a][x][b][y]
EX:
T[0][3][2][6] = [-3,6]
T[1][0][0][7] = [0,-7]
if a is 1, x MUST BE 0
if b is 1, y MUST BE 0

T[0] is a List<NIList<T>> of -x elements
T[1][0] is a NIList<T>
T[1][1+] DOES NOT EXIST
T[2] is a List<NIList<T>> of +x elements
*/
public T[][][][] Serialize(){
T[][][][] rtn = new T[3][][][];
//Negative
List<NIList<T>> neg = Negative;
rtn[0] = new T[neg.Count][][];
for(int i=0; i<neg.Count;i++){
rtn[0][i] = neg[i].Serialize();
}
//Zero
rtn[1] = new T[][][]{ZeroList.Serialize()};
//Positive
List<NIList<T>> pos = Positive;
rtn[2] = new T[pos.Count][][];
for(int i=0; i<pos.Count;i++){
rtn[2][i] = pos[i].Serialize();
}
return rtn;
}
public void Deserialize(T[][][][] input){
//Negative
List<NIList<T>> neg = new List<NIList<T>>();
for(int i=0; i<input[0].Length;i++){
neg.Add(new NIList<T>().Deserialize(input[0][i]));
}
Negative = neg;
//Zero
ZeroList = new NIList<T>().Deserialize(input[1][0]);
//Positive
List<NIList<T>> pos = new List<NIList<T>>();
for(int i=0; i<input[2].Length;i++){
pos.Add(new NIList<T>().Deserialize(input[2][i]));
}
Positive = pos;

}
#endregion Functions
#region Enumerable
public IEnumerator<T> GetEnumerator(){
return ((NIList<T>)this).GetEnumerator();
}
IEnumerator IEnumerable.GetEnumerator(){
return GetEnumerator();
}
#endregion Enumerable
#region Indexer
public T this[int x,int y]{
get{
if(y==0){
if(x==0){
//(0,0)
return value[0][0];
}else{
//(x,0)
return value[0][x];
}
}else{
if(y>0){
if(y-1>=value.Positive.Count){
//if(showDebug)Debug.Log("Index y:"+y+" is too big Returning default.");
return _default;
}
}
if(y<0){
if(Mathf.Abs(y)-1>=value.Negative.Count){
//if(showDebug)Debug.Log("Index y:"+y+" is too small Returning default.");
return _default;
}
}
return value[y][x];
}
}
set{
if(this.value[y]==null)this.value[y] = new NIList<T>(_default,showDebug);
#region Fill Y Gaps
//y is too big
while(y>this.value.Positive.Count){
NIList<T> list = this.value.Add(new NIList<T>(_default),1);
list.showDebug = showDebug;
}
//y is too small
while((y*-1)>this.value.Negative.Count){
NIList<T> list = this.value.Add(new NIList<T>(_default),-1);
list.showDebug = showDebug;
}
#endregion Fill Y Gaps
#region Fill X Gaps
void fillXGap(ref List<NIList<T>> list,int dir){
void subfillXGap(ref List<T> list,int dir){
while(x*dir>list.Count){
list.Add(_default);
}
}
if(x<0)subfillXGap(ref list[Mathf.Abs(y)-1].Negative,-1);
if(x>0)subfillXGap(ref list[Mathf.Abs(y)-1].Positive,1);
}
if(y>0){
fillXGap(ref this.value.Positive,1);
}
if(y<0){
fillXGap(ref this.value.Negative,-1);
}
#endregion Fill X Gaps
//Debug.Log("set ["+x+","+y+"] to "+value);
this.value[y][x] = value;
}//set
}//indexer
#endregion Indexer
#region Constructors
public NIList2D(T Default,bool shDB=true){
_default = Default;
//Debug.Log("Construct NIList2D with \""+_default+"\" as default");
NIList<T> def = new NIList<T>(_default);
value = new NIList<NIList<T>>(def);
showDebug = shDB;
}
public NIList2D(){
NIList<T> def = new NIList<T>(_default);
value = new NIList<NIList<T>>(def);
}
#endregion Constructors
#region conversion operator
public static explicit operator NIList<T>(NIList2D<T> This){
NIList<T> rtn = new NIList<T>();
foreach(NIList<T> list in This.value.Negative){
foreach(T val in list){
rtn.Add(val,-1);
}
}
foreach(T val in This.value.Zero){rtn.Add(val,-1);}
foreach(NIList<T> list in This.value.Positive){
foreach(T val in list){
rtn.Add(val,-1);
}
}
return rtn;
}
#endregion conversion operator
}
[System.Serializable]public class NIList3D<T> : IEnumerable<T>{
//Z Contains Y Contains X
T _default;
public NIList<NIList2D<T>> value;
public List<NIList2D<T>> All{get{
List<NIList2D<T>> rtn = new List<NIList2D<T>>();
foreach(NIList2D<T> list in value.Negative)rtn.Add(list);
rtn.Add(value.Zero);
foreach(NIList2D<T> list in value.Positive)rtn.Add(list);
return rtn;
}}
public bool showDebug = true;
public T Zero{get{return value[0].Zero;}set{this.value[0].Zero = value;}}
#region Functions
//Add T Value
public NIList2D<T> Add(T val,Vector3Int dir){
///dir(-1,0,1), returns index of added value
//Create a new plane and populate it
NIList2D<T> newPlane = new NIList2D<T>(_default);
NIList<T> newList = newPlane.Add(_default,new Vector2Int(dir.x,dir.y));
newList.Add(val,dir.x);

//z+
if(dir.z>0){
value.Positive.Add(newPlane);
return value.Positive[value.Positive.Count-1];
}
if(dir.z<0){
value.Negative.Add(newPlane);
return value.Negative[value.Negative.Count-1];
}

//x0
value.Zero.Add(val,new Vector2Int(dir.x,dir.y));
return value.Zero;

}
public int Add(NIList2D<T> val,int z){
///dir(-1,0,1), returns index of added value
//z+
if(z>0){
value.Positive.Add(val);
return value.Positive.Count - 1;
}
//z-
if(z<0){
value.Negative.Add(val);
return value.Negative.Count - 1;
}
//z0
value.Zero = val;
return 0;
}
public Vector3Int Add(NIList<T> val,Vector2Int zy){
///dir(-1,0,1), returns index of added value
int z = zy.y;
int y = zy.x;
NIList2D<T> newPlane = new NIList2D<T>(_default);
newPlane.Add(val,y);
//z+
if(z>0){
value.Positive.Add(newPlane);
return new Vector3Int(newPlane.Width-1,newPlane.Height-1,value.Positive.Count-1);
}
//z-
if(z>0){
value.Negative.Add(newPlane);
return new Vector3Int(newPlane.Width-1,newPlane.Height-1,(value.Negative.Count-1)*-1);
}
//z0
value[0] = newPlane;
return new Vector3Int(0,0,0);
}
public bool Contains(T val){
if((object)value.Zero==(object)val)return true;
foreach(NIList2D<T> list in value){
if(list.Contains(val))return true;
}
return false;
}
public Vector3Int IndexOf(T val,out bool found){
found = true;
if((object)Zero==(object)val)return new Vector3Int(0,0);

bool found2 = false;
for(int z=0; z<value.Count;z++){
Vector2Int xy = value[z].IndexOf(val,out found2);
int x = xy.x;
int y = xy.y;
//if is in list, return
if(found2)return new Vector3Int(x,y,z);
}
found = false;
return new Vector3Int(0,0,0);
}
public void Clear(){
foreach(NIList2D<T> list in value){
list.Clear();
}
}
#endregion Functions
#region return dimensions
public Vector3Int Size{get{return new Vector3Int(Width,Height,value.Count);}}
public int Width{get{
if(value.Count>0){
int longest = 0;
foreach(NIList2D<T> list in value.Negative){
if(list.Width>longest)longest = list.Width;
}
if(value.Zero.Width>longest)longest = value.Zero.Width;
foreach(NIList2D<T> list in value.Positive){
if(list.Width>longest)longest = list.Width;
}
return longest;
}else{
return 0;
}
}}
public int Height{get{
if(value.Count>0){
int longest = 0;
foreach(NIList2D<T> list in value.Negative){
if(list.Height>longest)longest = list.Height;
}
if(value.Zero.Height>longest)longest = value.Zero.Height;
foreach(NIList2D<T> list in value.Positive){
if(list.Height>longest)longest = list.Height;
}
return longest;
}else{
return 0;
}
}}
public int Depth{get{return value.Count;}}
#endregion return dimensions
#region Enumerable
public IEnumerator<T> GetEnumerator(){
return ((NIList2D<T>)this).GetEnumerator();
}
IEnumerator IEnumerable.GetEnumerator(){
return GetEnumerator();
}
#endregion Enumerable
#region Indexer
public T this[int x,int y,int z]{
get{

if(z>0){
if(z>value.Count){
if(showDebug)Debug.Log("Index z:"+z+" is too big Returning default.");
return _default;
}
}
if(z<0){
if(Mathf.Abs(z)>value.Negative.Count){
if(showDebug)Debug.Log("Index z:"+z+" is too small Returning default.");
return _default;
}
}
return value[z][x,y];

}
set{
#region Fill Z Gaps
//z is too big
if(z>0){
while(z>this.value.Positive.Count){
NIList2D<T> list = this.value.Add(new NIList2D<T>(_default),1);
list.showDebug = showDebug;
}
}
if(z<0){
//z is too small
while((z*-1)>this.value.Negative.Count){
NIList2D<T> list = this.value.Add(new NIList2D<T>(_default),-1);
list.showDebug = showDebug;
}
}
#endregion Fill Z Gaps
//Debug.Log("set ["+x+","+y+","+z+"] to "+value);
this.value[z][x,y] = value;
}
}
#endregion Indexer
#region Constructors
public NIList3D(T Default){
_default = Default;
//Debug.Log("Construct NIList3D with \""+_default+"\" as default");
NIList2D<T> def = new NIList2D<T>(_default);
value = new NIList<NIList2D<T>>(def);
}
public NIList3D(){
NIList2D<T> def = new NIList2D<T>(_default);
value = new NIList<NIList2D<T>>(def);
}
#endregion Constructors
#region conversion operator
public static explicit operator NIList2D<T>(NIList3D<T> This){
NIList2D<T> rtn = new NIList2D<T>();
foreach(NIList2D<T> list in This.value.Negative){
rtn.Add((NIList<T>)list,-1);
}
rtn.Add((NIList<T>)This.value.Zero,0);
foreach(NIList2D<T> list in This.value.Positive){
rtn.Add((NIList<T>)list,1);
}
return rtn;
}
#endregion conversion operator
}
[System.Serializable]public class ListWrapper<T> : IEnumerable<T>{
public List<T> value;
public int Count{get{return value.Count;}}
#region Casting&Constructors
//From List to ListWrapper
public static implicit operator ListWrapper<T>(List<T> value){return new ListWrapper<T>(value);}
//From ListWrapper to List
public static implicit operator List<T>(ListWrapper<T> value){return new List<T>(value.ToArray());}
public ListWrapper(List<T> list){
string dpName = "ListWrapper";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
this.value = new List<T>();
foreach(T val in list){
value.Add(val);
}
}
public ListWrapper(){
string dpName = "ListWrapper";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
this.value = new List<T>();
}
public ListWrapper(T[] list){
string dpName = "ListWrapper";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
Debug.Log("Array to ListWrapper");
this.value = new List<T>(list);
}
#endregion Casting&Constructors
public IEnumerator<T> GetEnumerator(){
return value.GetEnumerator();
}
IEnumerator IEnumerable.GetEnumerator(){
return GetEnumerator();
}
//Indexer
public T this[int i]{
get{return this.value[i];}
set{this.value[i] = value;}
}
#region Functions
public void Add(T Int){value.Add(Int);}
public void RemoveAt(int i){value.RemoveAt(i);}
public T[] ToArray(){
return value.ToArray();
}
#endregion Functions
}
[System.Serializable]public class Named<T>{
public string name;
public T value;
public Named(string name,T Value){
string dpName = "Named";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
value = Value;
this.name = name;
}
public static implicit operator T(Named<T> This){return This.value;}
public static implicit operator Named<T>(T This){return new Named<T>("",This);}
public override string ToString(){
return name+":"+value.ToString();
}
}
[System.Serializable]public class Linked<T,U>{
[HideInInspector]public string name;
public U value;
public T linkedValue;
public Linked(T val,U Value){
string dpName = "Linked";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);

//switch(Value.GetType()){
#region case Transform:
if(Value.GetType()==typeof(Transform)){
#endregion case Transform:
name = ((Transform)(object)Value).gameObject.name;
#region case default:
}else{
#endregion case default:
name = Value.ToString();
}

value = Value;
this.linkedValue = val;
}
public static implicit operator T(Linked<T,U> This){return This.linkedValue;}
public static implicit operator U(Linked<T,U> This){return This.value;}
public static implicit operator Linked<T,U>(U This){return new Linked<T,U>(default(T),This);}
public static implicit operator Linked<T,U>(T This){return new Linked<T,U>(This,default(U));}
public override string ToString(){
return value.ToString()+":"+linkedValue.ToString();
}
}
[System.Serializable]public class Wrapper<T>{
#region Variables
public string name;
public T value;
public T[] arrayValue;
public System.Type type;
#endregion Variables
#region Properties
public bool isArray{get{return (arrayValue!=null);}}
public int Length{get{
if(arrayValue!=null){return arrayValue.Length;}else{return -1;}
}}
#endregion Properties
#region Methods
public Y As<Y>(){
return (Y)((object)value);
}
public Y[] AsArray<Y>(){
List<Y> temp = new List<Y>();
foreach(T val in arrayValue){
temp.Add((Y)((object)val));
}
return temp.ToArray();
}
public Y AsArray<Y>(int index){
return (Y)(object)arrayValue[index];
}
#endregion Methods
#region Constructors
public Wrapper(string Name,T value){
string dpName = "Wrapper";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
this.value = value;
this.type = typeof(T);
this.name = Name;
}
public Wrapper(string Name,T[] value){
string dpName = "Wrapper";if(!FF.ClassDependancies.Contains(dpName))FF.ClassDependancies.Add(dpName);
this.arrayValue = value;
this.type = typeof(T);
this.name = Name;
}
#endregion Constructors
#region Type Casting
//Get Type from Wrapper<Type>
public static implicit operator T(Wrapper<T> wr){ 
Debug.Log("Casting "+wr.name+" to "+typeof(T));
if(wr.isArray){
Debug.Log(wr.name+".isArray");
if(typeof(T)==typeof(Vector3))Debug.Log("Casting to Vector3");
if(wr.arrayValue.GetType()==typeof(float[]))Debug.Log(wr.name+".type = float[]");
//if casting to Vector3 and type is float[]
if(typeof(T)==typeof(Vector3)&&wr.arrayValue.GetType()==typeof(float[])){
return (T)(object)new Vector3((float)(object)wr.arrayValue[0],(float)(object)wr.arrayValue[1],(float)(object)wr.arrayValue[2]);
}
}
return wr.value;
}
//Get Type[] from Wrapper<Type>
public static implicit operator T[](Wrapper<T> wr){ 
return wr.arrayValue;
}
//Get Wrapper<Type> from Type
public static implicit operator Wrapper<T>(T val){ 
return new Wrapper<T>("",val);
}
//Get Wrapper<Type> from Type[]
public static implicit operator Wrapper<T>(T[] val){ 
return new Wrapper<T>("",val);
}
#endregion Type Casting
}
#endregion Data Wrappers
#endregion Classes

#region Property Drawers

/*
[CustomPropertyDrawer(typeof(Linked<,>))]public class LinkedDrawer : PropertyDrawer{

private const float FOLDOUT_HEIGHT = 16f;
private SerializedProperty T;
private SerializedProperty U;

public override float GetPropertyHeight(SerializedProperty property,GUIContent label){
if(T==null)T = property.FindPropertyRelative("linkedValue");
if(U==null)U = property.FindPropertyRelative("value");

return FOLDOUT_HEIGHT*(property.isExpanded?2:1);
}

public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
EditorGUI.BeginProperty(position,label,property);
Rect foldoutRect = new Rect(position.x,position.y,position.width,FOLDOUT_HEIGHT);
property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded,label);

if(property.isExpanded){
EditorGUI.indentLevel++;
Rect rect = new Rect(position.x,position.y+FOLDOUT_HEIGHT,position.width/2,FOLDOUT_HEIGHT);
EditorGUI.PropertyField(rect,T,GUIContent.none,true);
rect.x = position.x+EditorGUIUtility.fieldWidth-((EditorGUI.indentLevel-1)*22);
EditorGUI.PropertyField(rect,U,GUIContent.none,true);
EditorGUI.indentLevel--;
}

EditorGUI.EndProperty();
}

}
*/
#endregion Property Drawers
}
