using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> {}

public class ConversationController : MonoBehaviour {

    //EGEN TEMPORÄR
    public bool textIsWriting = false;
    public AudioClip talksound;
    public AudioSource sourceForTalk;
    public ConversationAudio voiceAudioHolder;
    //OVAN EGEN TEMPORÄR

    public Conversation conversation;
    public QuestionEvent questionEvent;

    public GameObject firstSpeaker;
    public GameObject secondSpeaker;

    private SpeakerUIController firstSpeakerUI;
    private SpeakerUIController secondSpeakerUI;

    private int activeLineIndex;
    private bool conversationStarted = false;

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }

    void Start() {
        firstSpeakerUI = firstSpeaker.GetComponent<SpeakerUIController>();
        secondSpeakerUI = secondSpeaker.GetComponent<SpeakerUIController>();

        //firstSpeakerUI.Speaker = conversation.firstSpeaker;
        //secondSpeakerUI.Speaker = conversation.secondSpeaker;
    }

    //NEDAN ÄR MiTT EGNA
    public void SetConversation(Conversation newConversation)
    {
        conversation = newConversation;
    }

    //Lgger till eget nedanför här
    public void ProceedConversation()
    {
        AdvanceLine();
    }
    //void Update() {
    //    if (Input.GetKeyDown("space"))
    //    {
    //        AdvanceLine();
    //    }
    //    else if (Input.GetKeyDown("x"))
    //        EndConversation();
    //}
    //OVAN ÄR TYP MITT EGNA

        public bool checkHasConvoStarted()
    {
        return conversationStarted;
    }


    public void EndConversation()
    {
        conversation = null;
        conversationStarted = false;
        firstSpeakerUI.Hide();
        secondSpeakerUI.Hide();
        //EGNA CALL UNDER HÄR
        StopAllCoroutines();
        textIsWriting = false;
    }

    private void Initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        firstSpeakerUI.Speaker = conversation.firstSpeaker;
        secondSpeakerUI.Speaker = conversation.secondSpeaker;
    }

    private void AdvanceLine()
    {
        if (conversation == null) return;
        if (!conversationStarted) Initialize();

        if (activeLineIndex < conversation.lines.Length || textIsWriting)
        {
            DisplayLine();
        }            
        else
            AdvanceConversation();
    }

    void DisplayLine()
    {
        //Debug.Log("activa line index is: " + activeLineIndex);
        //Debug.Log("the full convo lines are: " + conversation.lines);

        //Debug.Log("is text writing: " + textIsWriting);

        if (!textIsWriting)
        {
            Line line = conversation.lines[activeLineIndex];
            Character character = line.character;
            if (firstSpeakerUI.SpeakerIs(character))
            {
                SetDialog(firstSpeakerUI, secondSpeakerUI, line.text);
            }
            else
            {
                SetDialog(secondSpeakerUI, firstSpeakerUI, line.text);
            }

            activeLineIndex += 1;
        } else if (textIsWriting)
        {
            Line line = conversation.lines[activeLineIndex-1];
            Character character = line.character;
            if (firstSpeakerUI.SpeakerIs(character))
            {
                SetDialogImmediately(firstSpeakerUI, secondSpeakerUI, line.text);
            }
            else
            {
                SetDialogImmediately(secondSpeakerUI, firstSpeakerUI, line.text);
            }
            //activeLineIndex += 1;
        }

    }

    private void AdvanceConversation()
    {
        if (conversation.question != null)
        {
            StopAllCoroutines();
            questionEvent.Invoke(conversation.question);
        }
        else if (conversation.nextConversation != null)
            ChangeConversation(conversation.nextConversation);
        else
            EndConversation();
    }

    //void AdvanceConversation()
    //{
    //    if (activeLineIndex < conversation.lines.Length)
    //    {
    //        DisplayLine();
    //        activeLineIndex += 1;
    //    }
    //    else
    //    {
    //        firstSpeakerUI.Hide();
    //        secondSpeakerUI.Hide();
    //        activeLineIndex = 0;
    //    }
    //}

    void SetDialog(
        SpeakerUIController activeSpeakerUI,
        SpeakerUIController inactiveSpeakerUI,
        string text)
    {

        {
            StopAllCoroutines();
            StartCoroutine(TypeSentance(text, activeSpeakerUI));
            //Ovan är mitt egna
            activeSpeakerUI.Show();
            inactiveSpeakerUI.Hide();
        }
        //TESTAR SJÄLV UNDER
        //activeSpeakerUI.Dialog = text;
        //Debug.Log(textIsWriting);
        //if (textIsWriting)
        //{
        //    Debug.Log("Nu kom vi in!");
        //    StopAllCoroutines();
        //    activeSpeakerUI.dialog.text = text;
        //} else

    }

    void SetDialogImmediately(
        SpeakerUIController activeSpeakerUI,
        SpeakerUIController inactiveSpeakerUI,
        string text)
    {
        StopAllCoroutines();
        textIsWriting = false;
        activeSpeakerUI.dialog.text = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();
    }

    //MITT EGNA UNDER hÄR
    IEnumerator TypeSentance (string sentance, SpeakerUIController activeSpeakerUI)
    {
        activeSpeakerUI.Dialog = "";
        textIsWriting = true;
        //Debug.Log(textIsWriting);
        foreach (char letter in sentance.ToCharArray())
        {
            
            // ena kodraden nedan är min egna kodrad
            voiceAudioHolder.PlayLetterspecificAudio(letter/*activeSpeakerUI.Speaker.voicePitch*/);
            activeSpeakerUI.Dialog = activeSpeakerUI.dialog.text + letter;
            yield return new WaitForSeconds(0.04f); ; 
        }
        textIsWriting = false;

    }
}


