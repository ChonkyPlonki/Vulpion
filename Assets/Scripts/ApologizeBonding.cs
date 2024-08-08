using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApologizeBonding : MonoBehaviour, BondingAction
{
    public int bondingNumber;
    public string bondingName;
    public GameObject apologizeFX;
    public GameObject animalReactFX;
    public Transform playerTr;
    public float duration;
    public float magnitude;
    //public GameObject mainCam;
    public AudioSource currentActionSound;
    private float animalFXOffset = 1;
    public AudioSource cryActionSound;

    public void PerformAction()
    {
        BondingHandler.Instance.TurnOnBondingText();
        BondWithAnimal();
    }

    private void BondWithAnimal()
    {        
        Quaternion q = new Quaternion();

        Vector3 animalPos = PlayerStats.collidedWith.transform.position;
        Vector3 diffVectorAnimalPlayer = playerTr.position - animalPos;
        float angleAnimalPlayer = GetAngle(diffVectorAnimalPlayer.x, diffVectorAnimalPlayer.y);
        q = Quaternion.Euler(-angleAnimalPlayer, 90, 90);
        //InstParticleFX(apologizeFX, playerTr, q);
        BondingHandler.Instance.InstPlayerParticleFX(apologizeFX, playerTr.position, q, apologizeFX.GetComponent<ParticleSystemRenderer>().sortingOrder);

        //GameObject partSys = PlayerStats.collidedWith.GetComponent<Animal>().heartPartSys;
        //InstParticleFX(partSys, PlayerStats.collidedWith.transform, partSys.transform.rotation);
        //InstParticleFX(animalReactFX, PlayerStats.collidedWith.transform, animalReactFX.transform.rotation);
        BondingHandler.Instance.InstAnimalReaction(animalReactFX, PlayerStats.collidedWith.transform, animalReactFX.transform.rotation, animalFXOffset, PlayerStats.collidedWith.gameObject);



    }

    public string GetBondingActionName()
    {
        return bondingName;
    }

    public int GetBondingAnimatorNumber()
    {
        return bondingNumber;
    }

    private float GetAngle(float x, float y)
    {
        float angle = (Mathf.Atan2(y, x) / Mathf.PI) * 180;
        return angle + 180;
    }

    /*
    private void InstParticleFX(GameObject partSys, Transform transform, Quaternion quat)
    {
        GameObject inst = Instantiate(partSys, transform.position + new Vector3(0,animalFXOffset, 0), quat) as GameObject;
        ParticleSystem parts = inst.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(inst, totalDuration);
    }*/

    public void DoAtStartAnimation()
    {
        cryActionSound.Play();
        currentActionSound.Play();
        PlayerRelated.Instance.playerEmoteHandlr.SetEmotiong("Sad");
        //StartCoroutine(Shake());
        BondingHandler.Instance.ShakeCam(duration, magnitude);
    }

    public void DoAfterAnimation()
    {
        PlayerRelated.Instance.playerEmoteHandlr.SetEmotiong("Neutral");
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
}
