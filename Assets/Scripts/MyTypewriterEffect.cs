using UnityEngine;
//using PixelCrushers.DialogueSystem;

public class MyTypewriterEffect : MonoBehaviour//: TextMeshProTypewriterEffect
{
    public ConversationAudio convoAudio;
    private TMPro.TextMeshProUGUI tmp;

    /*public override void Awake()
    {
        base.Awake();
        tmp = GetComponent<TMPro.TextMeshProUGUI>();
        //ConversationAudio.currentDialogText = tmp.GetParsedText();
        //print("doing awake in textmeshpro!");
        //print(ConversationAudio.currentDialogText);
    }

    protected override void PlayCharacterAudio()
    {
        //ConversationAudio.currentDialogText = tmp.GetParsedText();
        var c = tmp.GetParsedText()[tmp.maxVisibleCharacters];
        convoAudio.PlayLetterspecificAudio(c);
    }*/
}