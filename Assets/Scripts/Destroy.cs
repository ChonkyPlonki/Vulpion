using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public float timeToDestroy = 1f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDestroy);	
	}
}
