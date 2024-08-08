using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderOffWhenInWater : MonoBehaviour
{
    public Collider2D[] collToTurnOff;

    public void turnOffColl()
    {
        Debug.Log("COLLIDERS THAT ARENT IN WATER SHOULD BE TURNED OFF NOW, but I've yet to make a across-scenes solution that can handlemultiple scene references");
        foreach (var coll in collToTurnOff)
        {
            coll.enabled = false;
        }
        
    }

    public void turnOnColl()
    {
        Debug.Log("COLLIDERS THAT ARENT IN WATER SHOULD BE TURNED ON NOW, but I've yet to make a across-scenes solution that can handlemultiple scene references");
        foreach (var coll in collToTurnOff)
        {
            coll.enabled = true;
        }
    }
}
