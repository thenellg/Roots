using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using ForestsFunctions;
using ExtraFunctions;
using TMPro;

/*
All Selectable Menu Items Require a Data MonoBehaviour containing{

bool Selectable
Which dictates if the menu item is selectable

string text
Which Dictates what text will be displayed on it

string type
Values can be (Text:InputField)
Which dictates what type of menu item it is

Color Default
Color Selected
Color UnSelectable
Which Dictates what color they will be in these conditions

string selectBehaviour
Dictates what happens when enter is pressed while this option is selected
Menu:myMenuPrefab
Destroys this menu and replaces it with the menu named myMenuPrefab from the Menu:MonoBehaviour Menus[]

Out:menuOptionSelectBehaviour
Sets Out Data selectionBehaviour to "menuOptionSelectBehaviour"
}

Menu[0] should be set to the prefab of this menu

Out should be set to a Data MonoBehaviour that takes the menu data
to take action based on this game
Out should not be set inside the prefab, drag the menu to your Canvas, then set Out to the Out Data of your Menu Manager Script
Out Passes from Menu to Menu
Out Contains{
string Menu
Which tells what menu the player is on
string selectionBehaviour

}

Menu Requires a Control Scheme with:
Axis Navigation
Control Select
Control Back

Menus must be stored in Resources/Menus
*/

public class Menu : MonoBehaviour{
public List<Data> options;
public int OptionSelection;
public ControlScheme controls;
public List<string> Menus;
public int selectionDirection = 1;
public Data Out;
public List<string> History;

void Start(){
Out.String("Menu",Menus[0]);
Out.GameObject("Menu",gameObject);
//if options list is blank, autofill
if(options.Count==0){
//Get Data[] from menu children
options = new List<Data>(transform.GetChildren().GameObjects().GetComponent<Data>());
foreach(Data option in options){
option.name = gameObject.name;
//Add TMPText
if(option.gameObject.GetComponent<TextMeshProUGUI>()!=null){
option.TMP_Text(option.gameObject.name,option.gameObject.GetComponent<TextMeshProUGUI>());
option.TMP_Text(option.gameObject.name).value.text = option.String("text");
option.String("type","Text");
}
//Add TMPInputField
if(option.gameObject.GetComponent<TMP_InputField>()!=null){
option.TMP_InputField(option.gameObject.name,option.gameObject.GetComponent<TMP_InputField>());
option.TMP_InputField(option.gameObject.name).text = option.String("text");
option.String("type","InputField");
}
}
}
//options[OptionSelection].GetTMP_Text(options[OptionSelection].gameObject.name).color = options[OptionSelection].GetColor("Selected");
if(options.Count>0)Hilight(OptionSelection);
}
void Update(){
//Navigate Options
if(controls.Get<ControlFloat>("Menu_Navigation").down!=0){
UnHilight(OptionSelection);
OptionSelection = (OptionSelection+ (int)controls.Get<ControlFloat>("Menu_Navigation").down).Cycle(0,options.Count-1);
selectionDirection = (int)controls.Get<ControlFloat>("Menu_Navigation").down;
Hilight(OptionSelection);
}

//If this option is not selectable, move to the next one
if(!options[OptionSelection].Bool("Selectable")){
UnHilight(OptionSelection);
OptionSelection = (OptionSelection+selectionDirection).Cycle(0,options.Count-1);
Hilight(OptionSelection);
}

//If user selects this option
if(controls.Get<Control>("Menu_Select").down){
Select(OptionSelection);
}

//Go back one menu
if(controls.Get<Control>("Menu_Back").down){
GoUp();
}

}
public void Hilight(int index){
switch(options[index].String("type")){
case "Text":
if(options[index].Bool("Selectable")){
GetTMP(index).color = options[index].Color("Selected");
}else{
GetTMP(index).color = options[index].Color("UnSelectable");
}
break;
case "InputField":
UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(options[index].gameObject);
break;
}
}
public void UnHilight(int index){
switch(options[index].String("type")){
case "Text":
if(options[index].Bool("Selectable")){
GetTMP(index).color = options[index].Color("Default");
}else{
GetTMP(index).color = options[index].Color("UnSelectable");
}
break;
case "InputField":
UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
break;
}
}
public void Select(int index){
string behaviour = options[index].String("selectBehaviour");

switch(behaviour.Split(":")[0]){
case "Menu":
foreach(string menu in Menus){
GameObject GO = ((GameObject)Resources.Load("Menus/"+menu));

if(GO.name==behaviour.Split(":")[1]){
GameObject go = Instantiate(GO);
go.transform.parent = transform.parent;
go.transform.localPosition = Vector3.zero;
go.GetComponent<Menu>().Out = Out;
History.Add(Menus[0]);
go.GetComponent<Menu>().History = History;

Destroy(gameObject);
}
}
break;
case "Next":
UnHilight(OptionSelection);
OptionSelection = (OptionSelection+1).Cycle(0,options.Count-1);
Hilight(OptionSelection);
break;
case "Out":
Out.String("selectBehaviour",behaviour.Split(":")[1]);
break;
}
}
public void SetSelectable(int index,bool value){
options[index].Bool("Selectable",value);
options[index].TMP_Text("Load Game").value.color = options[index].Bool("Selectable")?options[index].Color("Default"):options[index].Color("UnSelectable");
}
TextMeshProUGUI GetTMP(int index){
return options[index].TMP_Text(options[index].gameObject.name);
}
TMP_InputField GetInputField(int index){
return options[index].TMP_InputField(options[index].gameObject.name);
}
public Data Option(string name){
foreach(Data option in options){
if(option.name == name)return option;
}
return null;
}
public void GoUp(){
if(History.Count>0){
GameObject go = Instantiate(((GameObject)Resources.Load("Menus/"+History[History.Count-1])));
go.transform.parent = transform.parent;
go.transform.localPosition = Vector3.zero;
go.GetComponent<Menu>().Out = Out;
History.RemoveAt(History.Count-1);
go.GetComponent<Menu>().History = History;
Destroy(gameObject);
}
}

}