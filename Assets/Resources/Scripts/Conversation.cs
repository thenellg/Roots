
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraFunctions;

[CreateAssetMenu(fileName = "Cnv_", menuName = "Misc/Conversation")]
public class Conversation : ScriptableObject{
public List<Dialogue> dialogue;
public Vector2 TextPadding;
public Vector2 BGPadding;
public float height;
public bool freezePlayer;
}
