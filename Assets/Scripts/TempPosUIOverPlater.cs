using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPosUIOverPlater : MonoBehaviour
{
    public Camera uiCam;
    public GameObject uiImage;
    public GameObject playerBondingNamePos;
    void Update()
    {
        Vector3 newPos = uiCam.WorldToScreenPoint(playerBondingNamePos.transform.position);
        uiImage.transform.position = newPos; //+ new Vector3(100,100,0);
    }
}
