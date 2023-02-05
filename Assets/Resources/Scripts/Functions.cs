using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraFunctions{
public static class FF{
public static Vector2 ScreenSize{get{return new Vector2(Screen.width,Screen.height);}}
public static float ScreenConstant{get{return 38.5f;}}
public static int relativeHemisphere(float angle,float relativeAngle){
if(angle>180){
if(relativeAngle>angle){
return 1;
}else{
return (relativeAngle>angle-180)?-1:1;
}
}else{ 
if(relativeAngle>angle){
return (relativeAngle<angle+180)?1:-1;
}else{
return -1;
}
}
}
}
public static class Extensions{
public static int height(this string This){return This.Split('\n').Length;}
public static Vector2 Rotated(this Vector2 This,float angle){
float Angle = Mathf.Atan2(This.y,This.x)+(angle*Mathf.Deg2Rad);
return new Vector2(Mathf.Cos(Angle),Mathf.Sin(Angle)*This.magnitude);
}
public static Vector2 ToVector2(this float This){
return new Vector2(Mathf.Cos(This*Mathf.Deg2Rad),Mathf.Sin(This*Mathf.Deg2Rad));
}
public static Vector3 To3(this Vector2 This,string blankDimension="z",float fillDimension=0){
switch(blankDimension.ToLower()){
case "x":return new Vector3(fillDimension,This.y,This.y);
case "y":return new Vector3(This.x,fillDimension,This.y);
case "z":return new Vector3(This.x,This.y,fillDimension);
}
return new Vector3(This.x,This.y,fillDimension);
}
public static float angle(this Vector2 This,bool inDegrees=false){
return Mathf.Atan2(This.y,This.x)*(inDegrees?Mathf.Rad2Deg:1);
}
public static PolarVector2 ToPolar(this Vector2 This){return new PolarVector2(Mathf.Atan2(This.y,This.x)*Mathf.Rad2Deg,This.magnitude);}
public static float time(this AnimationCurve This){
return This[This.length-1].time;
}
public static Vector2 Multiply(this Vector2 This,Vector2 v2){
return new Vector2(This.x*v2.x,This.y*v2.y);
}
public static Transform[] GetChildren(this Transform This){
Transform[] rtn = new Transform[This.childCount];
for(int i=0; i<This.childCount;i++){
rtn[i] = This.GetChild(i);
}
return rtn;
}
public static GameObject[] GameObjects(this Transform[] This){
GameObject[] rtn = new GameObject[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = This[i].gameObject;
}
return rtn;
}
public static int Cycle(this int This,int min,int max,bool overflow=false){
//Works like Mathf.Clamp but pacmans the result instead
if(This<min)return overflow?max+(This%max):max;
if(This>max)return overflow?min+(This%max):min;
return This;
}
public static T[] GetComponent<T>(this GameObject[] This){
//Get component[] from GameObject[]
T[] rtn = new T[This.Length];
for(int i=0; i<This.Length;i++){
rtn[i] = This[i].GetComponent<T>();
}
return rtn;
}
}
[System.Serializable]public class Vector2Curve{
public AnimationCurve x;
public AnimationCurve y;
public float time{get{return x.time()>y.time()?x.time():y.time();}}
public Vector2 Evaluate(float time){
return new Vector2(x.Evaluate(time),y.Evaluate(time));
}
}
[System.Serializable]public class PolarVector2{
public float radians{
get{return degrees*Mathf.Deg2Rad;}
set{degrees=value*Mathf.Rad2Deg;}
}
public float degrees;
public float magnitude;
public PolarVector2(float degrees,float magnitude){
this.degrees = degrees;
this.magnitude = magnitude;
}
public Vector2 rectangular{get{return new Vector2(Mathf.Cos(radians),Mathf.Sin(radians))*magnitude;}}
public override string ToString(){return "("+degrees+","+magnitude+")";}
}
[System.Serializable]public class Linked<T,U>{
public T value;
public U linkedValue;
public Linked(T value,U linkedValue){
this.value = value;
this.linkedValue = linkedValue;
}

}
[System.Serializable]public class Named<T>{
public string name;
public T value;
public Named(string name,T Value){
value = Value;
this.name = name;
}
public static implicit operator T(Named<T> This){return This.value;}
public static implicit operator Named<T>(T This){return new Named<T>("",This);}
public override string ToString(){
return name+":"+value.ToString();
}
}
[System.Serializable]public class Dialogue{
[Header("Text")]
[TextArea]public string text;
public TMProTextProfile profile;
public List<Linked<string,int>> options;
[Header("Portrait")]
public Sprite portrait;
public bool portraitOnRight;
public bool portraitMirrored;

[Header("Misc")]
[Tooltip("The index of the dialogue to jump to when the player advances the dialogue.\nMoves to next dialogue if set to 0")]
public int jumpTo;
public void Apply(TMPro.TextMeshProUGUI Text){
Text.text = text;
profile.Apply(Text);
}
}
[System.Serializable]public class TMProTextProfile{
public TMPro.TMP_FontAsset font;
public float fontSize;
public TMPro.FontStyles fontStyle;
public TMPro.TextAlignmentOptions textAlignment;
public Color color;

public void Apply(TMPro.TextMeshProUGUI text){
text.font = font;
text.fontSize = fontSize;
text.fontStyle = fontStyle;
text.alignment = textAlignment;
text.color = color;
}
}
[System.Serializable]public class TransformData{
public Vector3 position;
public Vector3 rotation;
public Vector3 scale;

public TransformData(Transform tf,bool local=false){
if(local){
position = tf.localPosition;
rotation = tf.localEulerAngles;
scale = tf.localScale;
}else{
position = tf.position;
rotation = tf.eulerAngles;
scale = tf.localScale;
}
}
public void Apply(Transform tf){
tf.position = position;
tf.localEulerAngles = rotation;
tf.localScale = scale;
}
public void ApplyLocal(Transform tf){
tf.localPosition = position;
tf.localEulerAngles = rotation;
tf.localScale = scale;
}
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
this.value = new List<T>();
foreach(T val in list){
value.Add(val);
}
}
public ListWrapper(){
this.value = new List<T>();
}
public ListWrapper(T[] list){
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


}
