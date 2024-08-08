using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManageBondingJumpMove : MonoBehaviour
{
    private float timeToMove = 0.2f;
    public static float distanceToAnimal = 1.5f;


    public void moveCharToCollidedWithAnimal()
    {
        StartCoroutine(SmoothJumpMove(timeToMove));
    }


    private IEnumerator SmoothJumpMove(float time)
    {
        //1,7 x distance => 0.2 f is good time => 0.2/1.7 = 0,117647 sec / 1 distance from animal transform
        Vector3 startingPos = PlayerRelated.Instance.playerMovingTransform.position;//playerTransform.position;
        //Vector3 finalPos = transform.position + new Vector3(5, 5, 0);
        //Vector3 finalPos = ShowAffectionBonding.animalCollWith.transform.position;// + new Vector3(-1, 0, 0);//PlayerStats.collidedWith.transform.position + new Vector3(-1, 0, 0);//transform.position + new Vector3(5, 5, 0);
        Vector3 finalPos = BondingHandler.animalToInteractWith.transform.position;//PlayerStats.collidedWith.transform.position;// + new Vector3(-1, 0, 0);//PlayerStats.collidedWith.transform.position + new Vector3(-1, 0, 0);//transform.position + new Vector3(5, 5, 0);
        //if (startingPos.x < finalPos.x)
        if (PlayerRelated.Instance.playerAnim.GetBool("JumpLeft"))
        {
            finalPos += new Vector3(distanceToAnimal, 0, 0);
        }else
        {
            finalPos -= new Vector3(distanceToAnimal, 0, 0);
        }
        float distance = Vector3.Distance(startingPos, finalPos);
        float timeToExecute = Mathf.Clamp(Mathf.Abs(distance * 0.075f),0.2f,2f);

        if (BondingHandler.Instance.GetCrntBondingNumber() == 4)
        {
            timeToExecute = 0.16f;
        }

        float elapsedTime = 0;

        //while (elapsedTime < time)
        while (elapsedTime < timeToExecute)
        {
            PlayerRelated.Instance.playerMovingTransform.position = Vector3.Slerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        PlayerRelated.Instance.playerAnim.SetTrigger("FinishedJump");
    }
}
