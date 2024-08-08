using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMainUIButtons : MonoBehaviour
{
    private GameObject currentUIChoice;
    private GameObject currentUIChoiceInterface;

    public Animator anim;
    public AudioSource audioS;

    public GameObject invToToggl;
    public GameObject mapToToggl;
    public GameObject skillToToggl;
    public GameObject discToToggl;
    public GameObject achiToToggl;
    public GameObject settToToggl;


    public GameObject invButtonImage;
    public GameObject mapButtonImage;
    public GameObject skillButtonImage;
    public GameObject discButtonImage;
    public GameObject achiButtonImage;
    public GameObject settButtonImage;

    //public GameObject parentWhenBelow;
    //public GameObject parentWhenOnTop;

    private float moveAmountWhenClicked = 10;
    private List<GameObject> allbuttonImages;
    private List<GameObject> allUIInterfaces;

    void Start()
    {        
        allbuttonImages = new List<GameObject> { invButtonImage, mapButtonImage, discButtonImage, skillButtonImage, achiButtonImage, settButtonImage };
        allUIInterfaces = new List<GameObject> { invToToggl, mapToToggl, discToToggl, skillToToggl, achiToToggl, settToToggl };
        currentUIChoice = invButtonImage;
        updateChoiceOnTop(invButtonImage);

    }

    public void turnOnInv()
    {
        setAllBoolsToFalse();
        anim.SetBool("PlayInv", true);
        turnOnRightCategory(invButtonImage, invToToggl);
    }

    public void turnOnMap()
    {
        setAllBoolsToFalse();
        anim.SetBool("PlayMap", true);
        turnOnRightCategory(mapButtonImage, mapToToggl);
    }

    public void turnOnSkill()
    {
        setAllBoolsToFalse();
        anim.SetBool("PlaySkill", true);
        turnOnRightCategory(skillButtonImage, skillToToggl);
    }    
    
    public void turnOnDisc()
    {
        setAllBoolsToFalse();
        anim.SetBool("PlayDisc", true);
        turnOnRightCategory(discButtonImage, discToToggl);
    }

    public void turnOnAchi()
    {
        setAllBoolsToFalse();
        anim.SetBool("PlayAchi", true);
        turnOnRightCategory(achiButtonImage, achiToToggl);
    }
    public void turnOnSett()
    {
        setAllBoolsToFalse();
        anim.SetBool("PlaySett", true);
        turnOnRightCategory(settButtonImage, settToToggl);
    }

    private void setAllBoolsToFalse()
    {
        anim.SetBool("PlayInv", false);
        anim.SetBool("PlayMap", false);
        anim.SetBool("PlaySkill", false);
        anim.SetBool("PlayDisc", false);
        anim.SetBool("PlayAchi", false);
        anim.SetBool("PlaySett", false);

    }

    public void turnOnRightCategory(GameObject imageButt, GameObject gameObToToggl)
    {
        PlayToggleSound();
        turnOffAllUIInterfaces();
        currentUIChoiceInterface = gameObToToggl;
        TurnOnUIPart();
        moveButtonImagesToCorrectPlace();        
        currentUIChoice = imageButt;
        resetButtonImagesParent();
        updateChoiceOnTop(imageButt);

    }

    private void PlayToggleSound()
    {
        audioS.volume = UnityEngine.Random.Range(0.05f, 0.08f);
        audioS.pitch = UnityEngine.Random.Range(0.8f, 1f);
        AudioMasterHandler.Instance.playUISound(audioS);
        //audioS.Play();
    }

    private void turnOffAllUIInterfaces()
    {
        foreach (GameObject interf in allUIInterfaces)
        {
            interf.SetActive(false);
        }
    }

    private void TurnOnUIPart()
    {
        if (currentUIChoiceInterface != null)
            currentUIChoiceInterface.SetActive(true);
    }

    private void updateChoiceOnTop(GameObject buttonImage)
    {
        buttonImage.GetComponent<Canvas>().overrideSorting = true;
        buttonImage.GetComponent<Canvas>().sortingOrder = 5;

        buttonImage.transform.position = buttonImage.transform.position + new Vector3(0, moveAmountWhenClicked, 0);
        //buttonImage.transform.SetParent(parentWhenOnTop.transform);
    }

    private void resetButtonImagesParent()
    {
        foreach (GameObject imageButt in allbuttonImages)
        {
            
            //print(imageButt);
            imageButt.GetComponent<Canvas>().overrideSorting = false;
            //imageButt.transform.SetParent(parentWhenBelow.transform);

        }
    }    
    
    private void moveButtonImagesToCorrectPlace()
    {
        if (currentUIChoice != null)
        {
            currentUIChoice.transform.position = currentUIChoice.transform.position - new Vector3(0, moveAmountWhenClicked, 0);
        }
    }


}
