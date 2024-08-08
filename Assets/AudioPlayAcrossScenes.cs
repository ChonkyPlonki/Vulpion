using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayAcrossScenes : MonoBehaviour
{
    private static AudioPlayAcrossScenes _instance;

    public static AudioPlayAcrossScenes Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public AudioSource papiBreathAS;
    public AudioSource enterHouseAS;
    public AudioSource exitHouseAS;

    public void PlayPapiBreatheAudio()
    {
        papiBreathAS.Play();
    }

    public void PlayEnterHouseAudio()
    {
        enterHouseAS.Play();
    }

    public void PlayExitHouseAudio()
    {
        exitHouseAS.Play();
    }
}
