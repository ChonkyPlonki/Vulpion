using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteParticleVars : MonoBehaviour
{


    private static EmoteParticleVars _instance;

    public static EmoteParticleVars Instance { get { return _instance; } }


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

    public GameObject happyParticles;
}
