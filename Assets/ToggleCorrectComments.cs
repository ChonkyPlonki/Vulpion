using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is enabled via the when some bonding-actions are done.
public class ToggleCorrectComments : MonoBehaviour
{
    public GameObject affectionCmntHolder;
    public GameObject notOKCmntHolder;

    public GameObject[] affectionComments;
    private void OnEnable()
    {
        ResetHolders();
        if (BondingHandler.Instance.GetCrntBondingNumber() == 3)
        {
            //affectionCmntHolder.SetActive(true);
            //TogglRightAffectionComments();
        } else if (BondingHandler.Instance.GetCrntBondingNumber() == 4)
        {
            notOKCmntHolder.SetActive(true);
            TogglNotOkCmnt();
        } else if (BondingHandler.Instance.GetCrntBondingNumber() == 7)
        {
            // Fix so that listening-comments appear and dissappear as well on their own after time.
        }
    }

    private void ResetHolders()
    {
        affectionCmntHolder.SetActive(false);
        notOKCmntHolder.SetActive(false);
}

    private void TogglNotOkCmnt()
    {
        
    }

    private void TogglRightAffectionComments()
    {
                int randomIndex = UnityEngine.Random.Range(0, affectionComments.Length);
                for (int i = 0; i < affectionComments.Length; i++)
                {
                    if (i == randomIndex)
                        affectionComments[i].SetActive(true);
                    else
                        affectionComments[i].SetActive(false);
                }
    }
}
