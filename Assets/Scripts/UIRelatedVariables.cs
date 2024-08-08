using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRelatedVariables : MonoBehaviour
{
    private static UIRelatedVariables _instance;

    public static UIRelatedVariables Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Sprite filledInvIcon;
    public Sprite emptyInvIcon;
    public Sprite highlightInvIcon;
}
