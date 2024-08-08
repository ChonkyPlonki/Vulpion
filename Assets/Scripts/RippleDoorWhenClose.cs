using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleDoorWhenClose : MonoBehaviour
{
    public GameObject doorRipplePS;
    private GameObject instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entering!: " + instance + collision);
        if (instance == null)
        {
            instance = Instantiate(doorRipplePS, transform.position, doorRipplePS.transform.rotation);
            instance.transform.parent = this.transform;
        }
        else
        {
            instance.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (instance != null)
        {
            instance.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
