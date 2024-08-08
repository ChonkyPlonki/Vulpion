using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnParticles : MonoBehaviour {

    public Animator animator;
    private GameObject inst;
    public Transform parentToParticles;

    [SerializeField]
    GameObject grassCloud = null;

    [SerializeField]
    GameObject waterBubbles = null;

    [SerializeField]
    GameObject underWaterBubbles = null;

    public float aboveWaterYOffset = 0;
    public float underWaterXOffset = 0;
    public float underWaterYOffset = 0;
    public SortingGroup playerSortingGroup;

    //private GameObject particleToSpawn;

    // Update is called once per frame
    void Update () {
		if (animator.GetBool("PlayerMoving") == true)
        {
            if (Random.Range(0,100) > 85)
            {
                if(WaterHandler.currentDepth <= 0.05)
                {
                    spawnParticle(grassCloud, 0, 0);
                } else
                {
                    if (!WaterHandler.isInWater)
                    {
                        spawnParticle(waterBubbles, 0, aboveWaterYOffset);
                    }  else
                    {
                        spawnParticle(underWaterBubbles, underWaterXOffset, underWaterYOffset);
                        inst.GetComponent<Renderer>().sortingLayerName = "WaterStuff";
                        inst.GetComponent<Renderer>().sortingOrder = 1;
                    }                  
                }             
            }
        }
	}

    private void spawnParticle(GameObject particleToSpawn, float offsetX, float offsetY)
    {
        Vector3 position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
        inst = Instantiate(particleToSpawn, position, particleToSpawn.transform.rotation);
        inst.transform.parent = parentToParticles;
        inst.transform.parent = parentToParticles;

        int temp = playerSortingGroup.sortingOrder;

        inst.GetComponent<Renderer>().sortingLayerID = 0;
        inst.GetComponent<Renderer>().sortingOrder = temp + 1;

        if (animator.GetFloat("LastMoveY") < 0)
        {
            
            inst.GetComponent<Renderer>().sortingLayerID = 0;
            inst.GetComponent<Renderer>().sortingOrder = temp-5;
        }
    }

    private void spawnUnderWaterParticles()
    {

    }
}
