using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerEmoter : MonoBehaviour
{
    public Animator VillagerAnim;
    public Transform VillagerEmotePos;
    public AudioSource VillagerAs;
    public AudioClip happygasp;

    public void PlayHappyEmote()
    {
        VillagerAnim.SetTrigger("isHappy");
        VillagerAs.PlayOneShot(happygasp);
        GameObject fxInstance = Instantiate(EmoteParticleVars.Instance.happyParticles, VillagerEmotePos.position, Quaternion.identity);
        //var main = fxInstance.transform.GetComponent<ParticleSystem>().main;
        //main.startColor = Inventory.Instance.itemPrefab[ItemHolder.InvSlotNbrHeld].GetComponent<Item>().colorOfUseEffect;//.main.startColor = new Color(0, 0, 0, 0);//Inventory.Instance.itemPrefab[invPlace].GetComponent<Item>().soundWhenUsed
        ParticleSystem parts = fxInstance.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(fxInstance, totalDuration);
    }
}
