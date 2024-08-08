using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.Linq;

public class VillagerProximityHandler : MonoBehaviour
{
    public static SortedDictionary<int, GameObject> closeVillagers = new SortedDictionary<int, GameObject>();
    public static int villagerCount;
    private int thisVillagerID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        villagerCount++;
        thisVillagerID = villagerCount;
        closeVillagers.Add(thisVillagerID, this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        closeVillagers.Remove(thisVillagerID);
    }

    public static bool IsCloseToNPC()
    {
        return closeVillagers.Count != 0;
    }

    public static GameObject ClosestVillager()
    {
        return closeVillagers.Values.Last();
    }
}
