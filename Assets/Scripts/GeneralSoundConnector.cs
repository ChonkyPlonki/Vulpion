using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSoundConnector : MonoBehaviour
{
    //public PlaySingleSound playSingleSpecifiedSound;

    void playSpecifiedSound()
    {
        AudioPlayAcrossScenes.Instance.PlayPapiBreatheAudio();
        //playSingleSpecifiedSound.PlaySoundOnce();
    }
}
