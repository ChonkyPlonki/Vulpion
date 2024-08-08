using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    private static Footsteps _instance;

    public static Footsteps Instance { get { return _instance; } }


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

    public AudioClip footstep1;
    public AudioClip footstep2;
    public AudioClip footstep3;
    public AudioClip footstep4;

    public AudioClip waterStep1;
    public AudioClip waterStep2;
    public AudioClip waterStep3;
    public AudioClip waterStep4;
    public AudioClip waterStep5;

    private float lowPitchRange = .90F;
    private float highPitchRange = 1.1F;
    private float velToVol = .2F;
    

    public AudioSource source;
    public AudioSource source1;
    public AudioSource source2;
    private AudioClip[] asList;
    private AudioClip[] waterAsList;
    private AudioSource[] sourcesAsList;

    private int countStep = 0;

    public bool isRaining;
    public AudioClip rainStep1;
    public AudioClip rainStep2;
    public AudioClip rainStep3;
    private AudioClip[] rainAsList;

   // footsteps.Add(footstep1);
    


	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        //   private List<AudioClip> footsteps = new List<AudioClip>();
        // footsteps.Add(footsteps1);
        asList = new AudioClip[5];
        waterAsList = new AudioClip[5];
        rainAsList = new AudioClip[3];
        sourcesAsList = new AudioSource[3];

        asList[0] = footstep1;
        asList[1] = footstep2;
        asList[2] = footstep3;
        asList[3] = footstep4;

        waterAsList[0] = waterStep1;
        waterAsList[1] = waterStep2;
        waterAsList[2] = waterStep3;
        waterAsList[3] = waterStep4;
        waterAsList[4] = waterStep5;

        sourcesAsList[0] = source;
        sourcesAsList[1] = source1;
        sourcesAsList[2] = source2;

        rainAsList[0] = rainStep1;
        rainAsList[1] = rainStep2;
        rainAsList[2] = rainStep3;
    }

public void stepSound()
    {
        countStep++;
        AudioSource currentAS = sourcesAsList[countStep % sourcesAsList.Length];
    source.pitch = Random.Range(lowPitchRange, highPitchRange);
        float hitVol = Random.Range(2,6) * velToVol;
        if(Random.Range(0,100) > 0)
        {
            //print(ShallowWaterHandler.currentDepth);
            if (isRaining)
            {
                currentAS.pitch = Random.Range(7, 11) * 0.1f;
                currentAS.volume = hitVol;
                currentAS.clip = rainAsList[Random.Range(0, 3)];
                AudioMasterHandler.Instance.playSoundEffect(currentAS);
            }
            else if (WaterHandler.Instance.IsStandingOnDryLand())
            {
                currentAS.pitch = Random.Range(7, 11) * 0.1f;
                currentAS.volume = hitVol;
                currentAS.clip = asList[Random.Range(0, 3)];
                //currentAS.Play();
                AudioMasterHandler.Instance.playSoundEffect(currentAS);
                //source.PlayOneShot(, hitVol);
            } else
            {
                currentAS.pitch = Random.Range(6, 13) * 0.1f;
                currentAS.volume = Random.Range(10,18) * 0.01f;
                currentAS.clip = waterAsList[Random.Range(0, 4)];
                AudioMasterHandler.Instance.playPlayerSound(currentAS);
                //currentAS.Play();
            }
            
        }

    }
}
	
