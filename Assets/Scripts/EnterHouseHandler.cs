using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouseHandler : MonoBehaviour
{
    public GameObject insideHouse;
    public Collider2D collToTurnOff;
    public GameObject fader;
    //public AudioSource enterHouseAS;
    //public AudioSource exitHouseAS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            insideHouse.SetActive(true);
            collToTurnOff.enabled = false;
            if (fader != null)
            {
                fader.SetActive(false);
            }
            PlayerRelated.Instance.insideHouseDarkener.SetActive(true);
            AudioPlayAcrossScenes.Instance.PlayEnterHouseAudio();
            //enterHouseAS.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            insideHouse.SetActive(false);
            collToTurnOff.enabled = true;
            if (fader != null)
            {
                fader.SetActive(true);
            }
            PlayerRelated.Instance.insideHouseDarkener.SetActive(false);
            AudioPlayAcrossScenes.Instance.PlayExitHouseAudio();
            //exitHouseAS.Play();
        }
    }
}
