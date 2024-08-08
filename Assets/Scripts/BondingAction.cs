using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BondingAction
{
    string GetBondingActionName();
    int GetBondingAnimatorNumber();
    void PerformAction();

    void DoAtStartAnimation();
    void DoAfterAnimation();
}
