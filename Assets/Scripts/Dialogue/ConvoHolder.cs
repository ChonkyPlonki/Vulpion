using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoHolder : MonoBehaviour {

    public Conversation conversationToRun;
    public ConversationController conversationControl;

    private bool isInsideTrigger = false;
    private bool isThisNPCConvoStarted = false;
    public ConvoEnder convoEnder;
    public ConvoContainer convoContainer;
    
    public void Speak()
    {
        if (isInsideTrigger && !conversationControl.checkHasConvoStarted())
        {
            conversationControl.SetConversation(convoContainer.conversationToStart);
            conversationControl.ProceedConversation();
            isThisNPCConvoStarted = true;
        }
        else if (conversationControl.checkHasConvoStarted() && /*convoEnder.canKeepTalking() &&*/ isThisNPCConvoStarted)
        {
            conversationControl.ProceedConversation();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    isInsideTrigger = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        isInsideTrigger = false;
    }


}
