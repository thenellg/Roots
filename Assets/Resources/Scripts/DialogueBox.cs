using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour{
#region Reference
Image bg;
Image portrait;
TMPro.TextMeshProUGUI text;
Transform optionContainer;
RectTransform optionSelect;
List<RectTransform> options;
#endregion Reference
public Conversation conversation;
public int position;
public int selection;

string playerPrevState;

void Start(){
playerPrevState = Player.instance.state;
if(conversation.freezePlayer)Player.instance.state = "In Dialogue";
//Get References
bg = transform.Find("Canvas").transform.Find("Background").GetComponent<Image>();
portrait = transform.Find("Canvas").transform.Find("Portrait").GetComponent<Image>();
text = transform.Find("Canvas").transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
optionContainer = transform.Find("Canvas").transform.Find("Options");
//Setup first Dialogue
CreateOptions();
conversation.dialogue[0].Apply(text);
Resize();
DrawPortrait();
}

void Update(){
//Selection
selection += (int)Player.instance.controls.Get<ControlFloat>("Menu_Navigation").up;
if(selection>conversation.dialogue[position].options.Count-1)selection = 0;
if(selection<0)selection = conversation.dialogue[position].options.Count-1;
if(optionSelect!=null){
optionSelect.localPosition = options[selection].localPosition;
optionSelect.sizeDelta = options[selection].sizeDelta;
}
//Advance
if(Player.instance.controls.Get<Control>("Menu_Select").up){
if(optionSelect!=null){
position = conversation.dialogue[position].options[selection].linkedValue;
}else{
if(conversation.dialogue[position].jumpTo!=0){
position = conversation.dialogue[position].jumpTo;
}else{
position++;
}
selection = 0;
}
//Destroy previous options
foreach(Transform tf in optionContainer){
Destroy(tf.gameObject);
}
if(position<conversation.dialogue.Count)CreateOptions();

//if reached end of dialogue, destroy this
if(position>conversation.dialogue.Count-1){
Destroy(gameObject);
Player.instance.state = playerPrevState;
}
}

//Apply new Dialogue
if(position<conversation.dialogue.Count){
conversation.dialogue[position].Apply(text);
Resize();
DrawPortrait();
}

}

void Advance(){
//Advance dialogue and destory object if at end
}
void DrawPortrait(){
if(conversation.dialogue[position].portrait!=null){
portrait.color = new Color(1,1,1,1);
float scale = conversation.height+(conversation.TextPadding.y*2);
portrait.sprite = conversation.dialogue[position].portrait;
portrait.rectTransform.sizeDelta = Vector2.one*(scale-(conversation.TextPadding.y*2));
#region Position Portrait
portrait.rectTransform.localPosition = new Vector2(
conversation.dialogue[position].portraitOnRight?
//portrait on right
(Screen.width/2)-(portrait.rectTransform.sizeDelta.x/2)-conversation.TextPadding.x-conversation.BGPadding.x:
//portrait on left
0-(Screen.width/2)+(portrait.rectTransform.sizeDelta.x/2)+conversation.TextPadding.x+conversation.BGPadding.x,
//portrait y
0-(Screen.height/2)+(portrait.rectTransform.sizeDelta.y/2)+conversation.TextPadding.y+conversation.BGPadding.y
);
#endregion Position Portrait
//Mirror Portrait
portrait.rectTransform.localScale = new Vector2(conversation.dialogue[position].portraitMirrored?-1:1, 1);
}else{ 
portrait.color = new Color(1,1,1,0);
}
}
void Resize(){
float scale = conversation.height+(conversation.TextPadding.y*2);
//Scale Background
bg.rectTransform.sizeDelta = new Vector2(
FF.ScreenSize.x-(conversation.BGPadding.x*2),
conversation.height+(conversation.TextPadding.y*2)
);
//Scale Text Box
text.rectTransform.sizeDelta = new Vector2(
FF.ScreenSize.x-(conversation.BGPadding.x*2)-(conversation.TextPadding.x*2)-(conversation.dialogue[position].portrait!=null?scale+conversation.TextPadding.x:0),
conversation.height
);
//Reposition
bg.rectTransform.localPosition = new Vector2(0,0-(Screen.height/2)+(bg.rectTransform.sizeDelta.y/2)+conversation.TextPadding.y);
text.rectTransform.localPosition = new Vector2(
conversation.dialogue[position].portrait!=null?conversation.dialogue[position].portraitOnRight?0-scale/2:scale/2:0,
0-(Screen.height/2)+(bg.rectTransform.sizeDelta.y/2)+conversation.TextPadding.y
);
}
void CreateOptions(){
options = new List<RectTransform>();
if(conversation.dialogue[position].options.Count>0){
//Create Option Selector
optionSelect = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Image")).GetComponent<RectTransform>();
optionSelect.SetParent(optionContainer);
optionSelect.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Frame");
}
float curY = (conversation.TextPadding.y+conversation.BGPadding.y)*2;
//Create Options
for(int i=conversation.dialogue[position].options.Count-1;i>=0;i--){
Linked<string,int> option = conversation.dialogue[position].options[i];
//Create and Parent Option
GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/TMPro Text"));
go.transform.SetParent(optionContainer);
options.Add(go.GetComponent<RectTransform>());

//Create Text Component
TMPro.TextMeshProUGUI Text = go.GetComponent<TMPro.TextMeshProUGUI>();
Text.text = option.value;
conversation.dialogue[position].profile.Apply(Text);

//Scale Option
Text.rectTransform.sizeDelta = new Vector2(
FF.ScreenSize.x-(conversation.BGPadding.x*2)-(conversation.TextPadding.x*2)-(conversation.dialogue[position].portrait!=null? conversation.height +(conversation.TextPadding.y*2)+conversation.TextPadding.x:0),
option.value.height()*(conversation.dialogue[position].profile.fontSize*1.1f)
);
//Position Option
go.transform.localPosition = new Vector2(//TAG
((conversation.height+(conversation.TextPadding.y*2))-(conversation.TextPadding.y*2))/2,
(conversation.TextPadding.y+curY)-(Screen.height/2)
);
curY += Text.rectTransform.sizeDelta.y;

}

options.Reverse();
}
}
