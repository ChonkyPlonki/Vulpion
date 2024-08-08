using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMasterHandler : MonoBehaviour
{

    
    private static AudioMasterHandler _instance;

    public static AudioMasterHandler Instance { get { return _instance; } }


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

    [Range(0.0f, 1.0f)]
    public float VolumeBackgroundMusic = 1;
    [Range(0.0f, 1.0f)]
    public float VolumeSoundEffects = 1;
    [Range(0.0f, 1.0f)]
    public float VolumeAmbientSounds = 1;
    [Range(0.0f, 1.0f)]
    public float VolumeDialogue = 1;
    [Range(0.0f, 1.0f)]
    public float VolumeUI = 1;
    [Range(0.0f, 1.0f)]
    public float VolumeItems = 1;
    [Range(0.0f, 1.0f)]
    public float VolumePlayerSoundEffects = 1;

    public void playPlayerSound(AudioSource audioSource)
    {
        audioSource.volume *= VolumePlayerSoundEffects;
        audioSource.Play();
    }
    public void playUISound(AudioSource audioSource)
    {
        audioSource.volume *= VolumeUI;
        audioSource.Play();
    }

    public void playItemSound(AudioSource audioSource)
    {
        audioSource.volume *= VolumeItems;
        audioSource.Play();
    }

    public void playSoundEffect(AudioSource audioSource)
    {
        audioSource.volume *= VolumeSoundEffects;
        audioSource.Play();
    }


    public void playAmbientSound(AudioSource audioSource)
    {
        audioSource.volume *= VolumeAmbientSounds;
        audioSource.Play();
    }
}
