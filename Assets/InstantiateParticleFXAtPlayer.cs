using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateParticleFXAtPlayer : MonoBehaviour
{
    public int instSortLayerOffset = -50;
    public GameObject estbBoundariesPlayerFX;
    public GameObject showOffPlayerFX;
    public GameObject smallsparklesPlayerFX;

    public void EstbBoundariesFXAtPlayer()
    {
        InstEffect(estbBoundariesPlayerFX, new Vector3(0, 0.6f, 0));
        InstEffect(smallsparklesPlayerFX, new Vector3(0, 0.6f, 0));
    }

    public void ShowOffFXAtPlayer()
    {
        InstEffect(showOffPlayerFX, new Vector3(0.3f, 0.6f, 0));
        InstEffect(smallsparklesPlayerFX, new Vector3(0, 0.6f, 0));
    }

    private void InstEffect(GameObject bondingFX, Vector3 offset)
    {
        Vector3 instPos = PlayerRelated.Instance.playerMovingTransform.position;
        int playerSort = PlayerRelated.Instance.playerSortingGr.sortingOrder;

        GameObject inst = Instantiate(bondingFX, instPos + offset, bondingFX.transform.rotation) as GameObject;
        inst.transform.parent = PlayerRelated.Instance.playerMovingTransform;
        inst.GetComponent<ParticleSystemRenderer>().sortingOrder = playerSort + instSortLayerOffset;

        ParticleSystem parts = inst.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(inst, totalDuration);
    }


}
