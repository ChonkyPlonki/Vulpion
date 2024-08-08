using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffChosenGameObject : MonoBehaviour
{
    public GameObject gameObjToTurnOff;

    public void TurnOffGameObj()
    {
        gameObjToTurnOff.SetActive(false);
    }
}
