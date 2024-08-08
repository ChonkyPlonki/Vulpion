using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public struct Line
{
    public Character character;

    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject {

    public Character firstSpeaker;
    public Character secondSpeaker;
    public Line[] lines;
    public Question question;
    public Conversation nextConversation;


}
