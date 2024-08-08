using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoEnder : MonoBehaviour {

    public ConversationController conversatonControl;
    private bool isInsideTalkDistance = false;

    public bool canKeepTalking()
    {
        return isInsideTalkDistance;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInsideTalkDistance = false;
        conversatonControl.EndConversation();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInsideTalkDistance = true;
    }
}
