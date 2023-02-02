using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraFunctions{
public static class FF{
public static Vector2 ScreenSize{get{return new Vector2(Screen.width,Screen.height);}}
public static float ScreenConstant{get{return 38.5f;}}
}
public static class Extensions{
public static int height(this string This){return This.Split('\n').Length;}
}
[System.Serializable]public class Linked<T,U>{
public T value;
public U linkedValue;
}
[System.Serializable]public class Named<T>{
public string name;
public T value;
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
}
