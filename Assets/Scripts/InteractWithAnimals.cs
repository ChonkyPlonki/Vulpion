using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithAnimals : MonoBehaviour
{
    //public GameObject heartParticleSys;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerStats.collidedWith = this.gameObject;
        }
           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerStats.collidedWith = null;
        }
            
    }


}
