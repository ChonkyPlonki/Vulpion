using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallColliderChecker : MonoBehaviour
{
    public static bool wallIsColliding;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("trigger entering!");
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("trigger stopped entering!!");
    //}
  
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("wall continoulsy colliding with: " + collision.gameObject.name);
            wallIsColliding = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("wall colliding with: " + collision.gameObject.name);
            wallIsColliding = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("wall stopped colliding with: " + collision.gameObject.name);
            wallIsColliding = false;
        }

    }
    /*
   private static int collisionCount = 0;

   public static bool wallIsColliding
   {
       get { return collisionCount == 0; }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       collisionCount++;
   }

   private void OnCollisionExit2D(Collision2D collision)
   {
       collisionCount--;
   }
    */
}
