using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondingHandler : MonoBehaviour
{

    private static BondingHandler _instance;

    public static BondingHandler Instance { get { return _instance; } }


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

    public static string currentAction;
    public static AudioSource currentActionSound;
    public static float playerAnimalAngle;
    public GameObject[] allBondingActions;
    private Dictionary<string, BondingAction> bondingActions;
    private Dictionary<int, string> bondingAnimNbrs;
    //public Animator playerAnimator;
    public Transform playerTr;
    public GameObject bondingTextFXGameObj;
    public Animator bondingTextAnim;
    public GameObject bondingMask;
    public GameObject mainCam;
    public static GameObject animalToInteractWith;

    private void Start()
    {
        bondingActions = new Dictionary<string, BondingAction>();
        bondingAnimNbrs = new Dictionary<int, string>();

        foreach( GameObject action in allBondingActions)
        {
            BondingAction bA = action.GetComponent<BondingAction>();
            bondingActions.Add(bA.GetBondingActionName(), bA);
            //bondingAnimNbrs.Add(bA.GetBondingActionName(), bA.GetBondingAnimatorNumber());
        }

        
    }

    public void PlayBondingAnimation()
    {
        Animator playerAnimator = PlayerRelated.Instance.playerAnim;
        animalToInteractWith = PlayerStats.collidedWith;
        Vector3 animalPos = PlayerStats.collidedWith.transform.position;
        Vector3 diffVectorAnimalPlayer = playerTr.position - animalPos;
        playerAnimalAngle = GetAnimationAngle(diffVectorAnimalPlayer.x, diffVectorAnimalPlayer.y);
        playerAnimator.SetFloat("BondingAngle", playerAnimalAngle);
        if (playerAnimalAngle >= 270 || playerAnimalAngle < 90)
        {
            playerAnimator.SetBool("JumpLeft", true);
        } else
        {
            playerAnimator.SetBool("JumpLeft", false);
        }

        bondingMask.SetActive(true);

        playerAnimator.SetFloat("BondingRitual", bondingActions[currentAction].GetBondingAnimatorNumber());
        PlayerRelated.Instance.playerAnim.SetBool("Bonding", true);

        //TurnOnBondingText();
        bondingActions[currentAction].PerformAction();
    }

    public void TurnOnBondingText()
    {        
        bondingTextFXGameObj.SetActive(true);
        bondingTextAnim.SetInteger("BondingSkill", bondingActions[currentAction].GetBondingAnimatorNumber());

    }

    public int GetCrntBondingNumber()
    {
        return bondingActions[currentAction].GetBondingAnimatorNumber();
    }

    public void DoBeforeAnimation()
    {
        Animator playerAnimator = PlayerRelated.Instance.playerAnim;
        playerAnimator.SetInteger("Emotion", bondingActions[currentAction].GetBondingAnimatorNumber());
        playerAnimator.SetLayerWeight(3, 0); //turns off regular emotions
        playerAnimator.SetLayerWeight(4, 1); //turns on bonding-regulated emotions

        PlayerController.canMove = false;
        bondingActions[currentAction].DoAtStartAnimation();
    }

    public void DoAfterAnimation()
    {
        PlayerRelated.Instance.playerAnim.SetBool("Bonding", false);
        Animator playerAnimator = PlayerRelated.Instance.playerAnim;
        playerAnimator.SetInteger("Emotion", 0);
        playerAnimator.SetLayerWeight(3, 1);
        playerAnimator.SetLayerWeight(4, 0);

        PlayerController.canMove = true;
        bondingActions[currentAction].DoAfterAnimation();
        ActionHandler.doingBondingRitual = false;
        bondingMask.SetActive(false);
    }


    private float GetAnimationAngle(float x, float y) //from -180 to 180
    {
        List<int> animAngles = new List<int>{0,45,90,135,180,225,270,315,345};

        float angle = (Mathf.Atan2(y, x) / Mathf.PI) * 180;
        if (angle < 0)
            angle = 360+angle;

        int closest = animAngles.Aggregate((i, j) => System.Math.Abs(i - angle) < System.Math.Abs(j - angle) ? i : j);

        return closest;
    }

    public void ShakeCam(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
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

    public void InstAnimalReaction(GameObject partSys, Transform transform, Quaternion quat, float animalFXOffset, GameObject animalCollWith)
    {
        GameObject inst = Instantiate(partSys, transform.position + new Vector3(0, animalFXOffset, 0), quat) as GameObject;

        CorrectSortOrderOfFX(inst, animalCollWith);

        ParticleSystem parts = inst.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(inst, totalDuration);
    }

    public void InstAnimalReaction(GameObject partSys, Quaternion quat, float animalFXOffset)
    {
        GameObject inst = Instantiate(partSys, animalToInteractWith.transform.position + new Vector3(0, animalFXOffset, 0), quat) as GameObject;

        CorrectSortOrderOfFX(inst, animalToInteractWith);

        ParticleSystem parts = inst.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(inst, totalDuration);
    }

    private void CorrectSortOrderOfFX(GameObject inst, GameObject animalCollWith)
    {
        //Make animal the parent of FX
        inst.gameObject.transform.parent = animalCollWith.transform;
        SpriteRenderer animalSR = animalCollWith.transform.GetComponent<Animal>().animalSR;
        inst.GetComponent<ParticleSystemRenderer>().sortingLayerName = animalSR.sortingLayerName;
        inst.GetComponent<ParticleSystemRenderer>().sortingOrder = animalSR.sortingOrder - 10;
    }

    public void InstPlayerParticleFX(GameObject partSys, Vector3 position, Quaternion quat, int sortingOrd)
    {
        GameObject inst = Instantiate(partSys, position, quat) as GameObject;
        inst.GetComponent<ParticleSystemRenderer>().sortingOrder = sortingOrd;
        // inst.GetComponent<ParticleSystemRenderer>().sortingLayerName = animalSR.sortingLayerName;
        // inst.GetComponent<ParticleSystemRenderer>().sortingOrder = animalSR.sortingOrder - 15;

        ParticleSystem parts = inst.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
        Destroy(inst, totalDuration);
    }

    public void TurnOnPlayerBondingExpression()
    {
        Animator playerAnimator = PlayerRelated.Instance.playerAnim;
        playerAnimator.SetInteger("Emotion", BondingHandler.Instance.GetCrntBondingNumber());
        playerAnimator.SetLayerWeight(3, 0); //turns off regular emotions
        playerAnimator.SetLayerWeight(4, 1); //turns on bonding-regulated emotions
    }

    public void StopAnimalCollMovement()
    {
        animalToInteractWith.transform.parent.gameObject.GetComponent<AnimalController>().StopMovement();
    }

    public void StartAnimalCollMovement()
    {
        animalToInteractWith.transform.parent.gameObject.GetComponent<AnimalController>().StartMovement();
    }
}
