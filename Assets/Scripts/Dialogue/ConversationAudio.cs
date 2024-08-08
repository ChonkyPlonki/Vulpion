using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationAudio : MonoBehaviour {
    public GameObject playerWithPosition;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public bool mirosVoice;
    public AudioClip a;
    public AudioClip b;
    public AudioClip c;
    public AudioClip d;
    public AudioClip e;
    public AudioClip f;
    public AudioClip g;
    public AudioClip h;
    public AudioClip i;
    public AudioClip j;
    public AudioClip k;
    public AudioClip l;
    public AudioClip m;
    public AudioClip n;
    public AudioClip o;
    public AudioClip p;
    public AudioClip q;
    public AudioClip r;
    public AudioClip s;
    public AudioClip t;
    public AudioClip u;
    public AudioClip v;
    public AudioClip w;
    public AudioClip x;
    public AudioClip y;
    public AudioClip z;
    public bool othersVoice;

    private int numberOfLetterSounds = 0;
    public static float currentPitchOfVoice = 1.3f;
    public static bool isDialogTextAQuestion = false;

    public static int nbrOfCharsInDialogText = 0;
    public static int placeLastWordStartsInString = 0;
    public static  int currentIndexOfCharInDialog = 0;
    public static string currentDialogText = " ";

    private AudioSource[] allAudioSources;

    private void Start()
    {
        allAudioSources =  new AudioSource[4]{ audioSource1, audioSource2, audioSource3, audioSource4 };
    }
    public AudioClip ReturnRightAudio(char letter)
    {
        switch (letter)
        {
            case 'a':
                return a;
            case 'b':
                return v;
            case 'c':
                return c;
            case 'd':
                return w;
            case 'e':
                return e;
            case 'f':
                return f;
            case 'g':
                return g;
            case 'h':
                return h;
            case 'i':
                return i;
            case 'j':
                return j;
            case 'k':
                return m;
            case 'l':
                return l;
            case 'm':
                return m;
            case 'n':
                return n;
            case 'o':
                return o;
            case 'p':
                return y;
            case 'q':
                return u;
            case 'r':
                return r;
            case 's':
                return s;
            case 't':
                return m;
            case 'u':
                return u;
            case 'v':
                return v;
            case 'w':
                return w;
            case 'x':
                return x;
            case 'y':
                return u;
            case 'z':
                return z;
            default:
                return null;
                           
        }
    } 

    public void PlayLetterspecificAudio(char letter/*, float pitch*/)
    {
        /*currentIndexOfCharInDialog++;
        print(currentIndexOfCharInDialog);
        print(placeLastWordStartsInString);
        print(nbrOfCharsInDialogText);
        print(currentDialogText);*/

        numberOfLetterSounds++;
        int asToPick = numberOfLetterSounds % 4;

        //placeLastWordStartsInString = currentDialogText.LastIndexOf(' ');

        AudioClip clip = ReturnRightAudio(char.ToLower(letter));
        if(asToPick == 0 || asToPick == 2)
        {
            audioSource1.pitch = currentPitchOfVoice;
            audioSource1.clip = clip;
            audioSource1.Play();
            if (clip != null)
            {
                audioSource1.volume = 0.4f;

                audioSource1.pitch = currentPitchOfVoice;

 /*               if (isDialogTextAQuestion)
                { 
                    if(currentIndexOfCharInDialog >= placeLastWordStartsInString-30)
                    {
                        audioSource1.volume += 0.005f;
                        currentPitchOfVoice += 0.05f;
                    }
                    
                } */
                audioSource1.clip = clip;
                audioSource1.Play();
                //allAudioSources[asToPick].pitch = currentPitchOfVoice;
                //allAudioSources[asToPick].clip = clip;
                //allAudioSources[asToPick].Play();


            }
        }

    }
}


//AudioSource audioSource = gameObject.AddComponent<AudioSource>();
//audioSource.clip = clip;
//audioSource.pitch = 1.3f;
//audioSource.Play();

//AudioSource.PlayClipAtPoint(clip, playerWithPosition.transform.position);
//AudioSource audioSource = gameObject.AddComponent<AudioSource>();
//audioSource.clip = Resources.Load(name) as AudioClip;
//audioSource.Play();

//audioSource.clip = ReturnRightAudio(char.ToLower(letter));
//audioSource.pitch = pitch;
//audioSource.Play();