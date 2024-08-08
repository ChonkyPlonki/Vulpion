using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepsAudioConnector : MonoBehaviour
{
    void playFootstepsSound()
    {
        //The main function for footsteps is in Footsteps-script. This is simply a connector for good organization-purposes.
        Footsteps.Instance.stepSound();
    }
}
