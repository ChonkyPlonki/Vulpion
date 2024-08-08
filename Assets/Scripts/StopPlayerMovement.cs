using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb2;
    public Animator playerAnim;

    public void stopPlayerMovement()
    {
        //print("doiiing!");
        playerRb2.velocity = new Vector3(0, 0, 0);
        PlayerController.playerMoving = false;
        playerAnim.SetBool("PlayerMoving", false);
    }
}
