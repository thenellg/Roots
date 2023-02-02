using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

[CreateAssetMenu(fileName = "ControlScheme", menuName = "Misc/ControlScheme")]
public class ControlScheme : ScriptableObject{
public List<Named<Control>> controls;
public List<Named<ControlFloat>> floats;
public List<Named<ControlVector2>> vector2s;
public List<Named<ControlVector3>> vector3s;
public T Get<T>(string name){
#region switch(typeof(T))
//case Control:
if(typeof(T)==typeof(Control)){
foreach(Named<Control> namedControl in controls){
if(namedControl.name==name)return (T)(object)namedControl.value;
}
}
//break;
//case ControlFloat:
if(typeof(T)==typeof(ControlFloat)){
foreach(Named<ControlFloat> namedControl in floats){
if(namedControl.name==name)return (T)(object)namedControl.value;
}
}
//break;
//case ControlVector2:
if(typeof(T)==typeof(ControlVector2)){
foreach(Named<ControlVector2> namedControl in vector2s){
if(namedControl.name==name)return (T)(object)namedControl.value;
}
}
//break;
//case ControlVector3:
if(typeof(T)==typeof(ControlVector3)){
foreach(Named<ControlVector3> namedControl in vector3s){
if(namedControl.name==name)return (T)(object)namedControl.value;
}
}
#endregion
Debug.Log("Couldn't Find "+name);
return (T)(object)null;
}
}

[System.Serializable]public class Control{
public KeyCode key;
public int mouse;
public bool down{get{
return (mouse==0)?Input.GetKeyDown(key):Input.GetMouseButtonDown(mouse-1);
}}
public bool up{get{
return (mouse==0)?Input.GetKeyUp(key):Input.GetMouseButtonUp(mouse-1);
}}
public static implicit operator bool(Control This){
return (This.mouse==0)?Input.GetKey(This.key):Input.GetMouseButton(This.mouse-1);
}
}
[System.Serializable]public class ControlFloat{
public Control negative;
public Control positive;
public float down{get{
return (positive.down?1:0)-(negative.down?1:0);
}}
public float up{get{
return (positive.up?1:0)-(negative.up?1:0);
}}
public static implicit operator float(ControlFloat This){
return (This.positive?1:0)-(This.negative?1:0);
}
}
[System.Serializable]public class ControlVector2{
public ControlFloat x;
public ControlFloat y;
public Vector2 down{get{
return new Vector2(x.down,y.down);
}}
public Vector2 up{get{
return new Vector2(x.up,y.up);
}}
public static implicit operator Vector2(ControlVector2 This){
return new Vector2(This.x,This.y);
}

}
[System.Serializable]public class ControlVector3{
public ControlFloat x;
public ControlFloat y;
public ControlFloat z;
public Vector3 down{get{
return new Vector3(x.down,y.down,z.down);
}}
public Vector3 up{get{
return new Vector3(x.up,y.up,z.up);
}}
public static implicit operator Vector3(ControlVector3 This){
return new Vector3(This.x,This.y,This.z);
}

}
