using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemHandler : MonoBehaviour
{

    private static UseItemHandler _instance;

    public static UseItemHandler Instance { get { return _instance; } }


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

    public Transform itemPosition;
    public GameObject useEffect;
    public AudioSource asEffectSound;


    public void PlayUseEffectOfHeldItem()
    {
        int invPlace = ItemHolder.InvSlotNbrHeld;
        

        if(PlayerRelated.Instance.playerAnim.GetBool("ConsumingItem"))
        {
            PlayerRelated.Instance.playerAnim.SetTrigger("ResetUseItemAnim");
            PlayerRelated.Instance.playerAnim.SetBool("ConsumingItem", false);
        }
        if (Inventory.Instance.amount[invPlace] == 1)
        {
            PlayerRelated.Instance.playerAnim.SetBool("ConsumingLastItem", true);
        }
        else
        {
            PlayerRelated.Instance.playerAnim.SetBool("ConsumingItem", true);
        }

        asEffectSound.clip = Inventory.Instance.itemPrefab[invPlace].GetComponent<Item>().soundWhenUsed;
        asEffectSound.Play();
        if (!ActionHandler.isGivingGift)
            PlayFX();

    }

    private void PlayFX()
    {
        GameObject fxInstance = Instantiate(useEffect, itemPosition.position, Quaternion.identity);
        var main = fxInstance.transform.GetComponent<ParticleSystem>().main;
        main.startColor = Inventory.Instance.itemPrefab[ItemHolder.InvSlotNbrHeld].GetComponent<Item>().colorOfUseEffect;//.main.startColor = new Color(0, 0, 0, 0);//Inventory.Instance.itemPrefab[invPlace].GetComponent<Item>().soundWhenUsed
        ParticleSystem parts = fxInstance.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(fxInstance, totalDuration);
    }
}
