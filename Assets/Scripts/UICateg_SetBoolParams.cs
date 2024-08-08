using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICateg_SetBoolParams : MonoBehaviour
{
    // Start is called before the first frame update

    public void setPlayInvFalse()
    {
        GetComponent<Animator>().SetBool("PlayInv", false);
    }

    public void setPlayMapFalse()
    {
        GetComponent<Animator>().SetBool("PlayMap", false);
    }

    public void setPlaySkillFalse()
    {
        GetComponent<Animator>().SetBool("PlaySkill", false);
    }

    public void setPlayDiscFalse()
    {
        GetComponent<Animator>().SetBool("PlayDisc", false);
    }

    public void setPlayAchiFalse()
    {
        GetComponent<Animator>().SetBool("PlayAchi", false);
    }

    public void setPlaySettFalse()
    {
        GetComponent<Animator>().SetBool("PlaySett", false);
    }
}
