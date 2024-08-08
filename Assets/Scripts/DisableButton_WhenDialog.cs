using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton_WhenDialog : MonoBehaviour
{

    public void disableButton()
    {
        GetComponent<Button>().enabled = false;
    }

    public void enableButton()
    {
        GetComponent<Button>().enabled = true;
    }

}
