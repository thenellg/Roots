using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ForestsFunctions;
using ExtraFunctions;

public class Data : MonoBehaviour{
public static List<Data> all = new List<Data>();
public static Data Get(string name){
foreach(Data dat in all){
if(dat.name==name)return dat;
}
Debug.Log(name+" does not contain Data Component! Returning null");
return null;
}

public string name;
public List<Named<int>> ints;
public List<Named<float>> floats;
public List<Named<bool>> bools;
public List<Named<string>> strings;
public List<Named<Color>> colors;
public List<Named<GameObject>> gameObjects;
public List<Named<TMPro.TextMeshProUGUI>> TMP_texts;
public List<Named<TMPro.TMP_InputField>> TMP_inputs;

void Awake(){
if(!all.Contains(this))all.Add(this);
}

public Named<int> Int(string name){
foreach(Named<int> Value in ints){
if(name == Value.name)return Value;
}
Debug.Log("int "+name+" not found! Returning 0.");
return 0;
}
public Named<float> Float(string name){
foreach(Named<float> Value in floats){
if(name == Value.name)return Value;
}
Debug.Log(name+" not found! Returning 0.");
return 0;
}

public Named<bool> Bool(string name){
foreach(Named<bool> Value in bools){
if(name == Value.name)return Value;
}
Debug.Log(name+" not found! Returning false.");
return false;
}
public void Bool(string name,bool Value){
bool foundVar = false;
for(int i=0; i<bools.Count;i++){
if(name == bools[i].name)bools[i].value = Value;
}
if(!foundVar)bools.Add(new Named<bool>(name,Value));
}

public Named<string> String(string name){
foreach(Named<string> Value in strings){
if(name == Value.name)return Value;
}
Debug.Log(name+" not found! Returning \"\".");
return "";
}
public void String(string name,string value){
if(strings==null)strings = new List<Named<string>>();
bool foundVar = false;
for(int i=0; i<strings.Count;i++){
if(strings[i].name==name){
strings[i].value = value;
foundVar = true;
}
}
if(!foundVar)strings.Add(new Named<string>(name,value));
}

public Named<Color> Color(string name){
foreach(Named<Color> Value in colors){
if(name == Value.name)return Value;
}
Debug.Log(name+" not found! Returning black.");
return new Named<Color>("",UnityEngine.Color.black);
}
public void Color(string name,Color Value){
bool foundVar = false;
for(int i=0; i<colors.Count;i++){
if(name == colors[i].name)colors[i].value = Value;
}
if(!foundVar)colors.Add(new Named<Color>(name,Value));
}

public Named<GameObject> GameObject(string name){
foreach(Named<GameObject> Value in gameObjects){
if(name == Value.name)return Value;
}
Debug.Log(name+" not found! Returning null.");
return null;
}
public void GameObject(string name,GameObject gameObject){
bool foundVar = false;
for(int i=0; i<gameObjects.Count;i++){
if(name == gameObjects[i].name){
gameObjects[i].value = gameObject;
foundVar = true;
}
}
if(!foundVar)gameObjects.Add(new Named<GameObject>(name,gameObject));
}

public Named<TMPro.TextMeshProUGUI> TMP_Text(string name){
foreach(Named<TMPro.TextMeshProUGUI> Value in TMP_texts){
if(name == Value.name)return Value;
}
Debug.Log(name+" not found! Returning null.");
return null;
}
public void TMP_Text(string name,TMPro.TextMeshProUGUI Value){
bool foundVar = false;
for(int i=0; i<TMP_texts.Count;i++){
if(name == TMP_texts[i].name){
TMP_texts[i].value = Value;
foundVar=true;
}
}
if(!foundVar)TMP_texts.Add(new Named<TMPro.TextMeshProUGUI>(name,Value));
}

public TMPro.TMP_InputField TMP_InputField(string name){
foreach(Named<TMPro.TMP_InputField> Value in TMP_inputs){
if(name == Value.name)return Value;
}
Debug.Log(name+" not found! Returning null.");
return null;
}
public void TMP_InputField(string name,TMPro.TMP_InputField Value){
//foreach(TMPro.TMP_InputField Value in TMP_inputs){
bool foundVar = false;
for(int i=0; i<TMP_inputs.Count;i++){
if(name == TMP_inputs[i].name){
TMP_inputs[i].value = Value;
foundVar = true;
}
}
if(!foundVar)TMP_inputs.Add(new Named<TMPro.TMP_InputField>(name,Value));
}

}