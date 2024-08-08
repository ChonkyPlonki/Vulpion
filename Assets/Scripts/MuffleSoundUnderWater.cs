using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffleSoundUnderWater : MonoBehaviour
{
    [SerializeField]
    public AudioLowPassFilter[] allAudioFilters;

    private static MuffleSoundUnderWater _instance;

    public static MuffleSoundUnderWater Instance { get { return _instance; } }


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

    public void turnOnUnderWaterAudio()
    {
        foreach (var item in allAudioFilters)
        {
            item.enabled = true;
        }
    }

    public void turnOffUnderWaterAudio()
    {
        foreach (var item in allAudioFilters)
        {
            item.enabled = false;
        }
    }
}
