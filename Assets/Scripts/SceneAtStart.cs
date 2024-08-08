using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAtStart : MonoBehaviour
{
    public GameObject bigUI;
    public GameObject itemInfoHolder;
    // Start is called before the first frame update
    void Start()
    {
        TogglOnOffUI();
    }

    // Update is called once per frame

    private void TogglOnOffUI()
    {
        bigUI.SetActive(true);
        bigUI.SetActive(false);
        itemInfoHolder.SetActive(true);
        itemInfoHolder.SetActive(false);
    }
}
