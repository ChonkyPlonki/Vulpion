using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PixelCrushers.DialogueSystem;

public class SetVoiceAppearance : MonoBehaviour
{
    [SerializeField]
    public CharacterInfo[] characterPrefabs;

    public TMPro.TextMeshProUGUI tmp1;
    public TMPro.TextMeshProUGUI tmp2;

    private Dictionary<string, CharacterInfo> allCharacters;

    private string currentDialogText;

    private void Start()
    {
        allCharacters = new Dictionary<string, CharacterInfo>();
        foreach (var character in characterPrefabs)
        {
            allCharacters.Add(character.charName, character);
            //print(allCharacters[character.charName]);
        }
        
    }

    void OnConversationLine()//Subtitle subtitle)
    {
        /*
        //Setting pitch based on character

       ConversationAudio.currentPitchOfVoice = allCharacters[DialogueManager.currentConversationState.subtitle.speakerInfo.nameInDatabase].pitch;
        currentDialogText = DialogueManager.currentConversationState.subtitle.dialogueEntry.currentDialogueText;

        //
        ConversationAudio.nbrOfCharsInDialogText = currentDialogText.Length;

        ConversationAudio.currentIndexOfCharInDialog = 0;
        ConversationAudio.currentDialogText = currentDialogText;
        ConversationAudio.placeLastWordStartsInString = currentDialogText.LastIndexOf(' '); 


        


        if (currentDialogText.EndsWith("?"))
        {            
            ConversationAudio.isDialogTextAQuestion = true;
        } else {
            ConversationAudio.isDialogTextAQuestion = false;
        }
        */
    }
}
