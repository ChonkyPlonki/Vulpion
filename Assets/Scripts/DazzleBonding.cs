using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazzleBonding : MonoBehaviour, BondingAction
{
    public int bondingNumber;
    public string bondingName;
    public AudioSource dazzleSound;
    //public GameObject mainCam;
    public float duration;
    public float magnitude;
    private float animalFXOffset = 1;
    public GameObject animalDazzledFX;

    //private float animalFXOffset = 1;
    public void DoAfterAnimation()
    {
        //PlayerRelated.Instance.playerEmoteHandlr.SetEmotiong("Neutral");
    }

    public void DoAtStartAnimation()
    {
        dazzleSound.Play();
        //PlayerRelated.Instance.playerEmoteHandlr.SetEmotiong("Sad");
        //StartCoroutine(Shake());
        BondingHandler.Instance.ShakeCam(duration, magnitude);
    }

    public string GetBondingActionName()
    {
        return bondingName;
    }

    public int GetBondingAnimatorNumber()
    {
        return bondingNumber;
    }

    public void PerformAction()
    {
        BondingHandler.Instance.TurnOnBondingText();
        BondingHandler.Instance.InstAnimalReaction(animalDazzledFX, PlayerStats.collidedWith.transform, animalDazzledFX.transform.rotation, animalFXOffset, PlayerStats.collidedWith.gameObject);
        //InstAnimalReaction(animalDazzledFX, PlayerStats.collidedWith.transform, animalDazzledFX.transform.rotation);
    }

    /*
    IEnumerator Shake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = mainCam.transform.localPosition;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            mainCam.transform.localPosition = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        mainCam.transform.localPosition = originalCamPos;
    }
    */
    /*
    private void InstAnimalReaction(GameObject partSys, Transform transform, Quaternion quat)
    {
        GameObject inst = Instantiate(partSys, transform.position + new Vector3(0, animalFXOffset, 0), quat) as GameObject;

        CorrectSortOrderOfFX(inst);

        ParticleSystem parts = inst.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(inst, totalDuration);
    }

    private void CorrectSortOrderOfFX(GameObject inst)
    {
        //Make animal the parent of FX
        inst.gameObject.transform.parent = PlayerStats.collidedWith.transform;
        SpriteRenderer animalSR = PlayerStats.collidedWith.transform.GetComponent<Animal>().animalSR;
        inst.GetComponent<ParticleSystemRenderer>().sortingLayerName = animalSR.sortingLayerName;
        inst.GetComponent<ParticleSystemRenderer>().sortingOrder = animalSR.sortingOrder;
    }
    */


}
