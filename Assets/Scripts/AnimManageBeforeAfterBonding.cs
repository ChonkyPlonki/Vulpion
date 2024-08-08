using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManageBeforeAfterBonding : MonoBehaviour
{

    public void BeforeBonding()
    {
        BondingHandler.Instance.DoBeforeAnimation();
    }

    public void AfterBonding()
    {
        BondingHandler.Instance.DoAfterAnimation();
    }

}
