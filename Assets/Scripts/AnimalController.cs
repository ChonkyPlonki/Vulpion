using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeToChangeDirection;
    private float desiredLength = 1;
    private Vector3 direction;
    private bool shouldMove = true;
    private bool isMoving;

    public void StopMovement()
    {
        shouldMove = false;
    }

    public void StartMovement()
    {
        shouldMove = true;
    }

    // Use this for initialization
    public void Start()
    {
        //ChangeDirection();
    }

    // Update is called once per frame
    public void Update()
    {
        if(shouldMove)
        {
            isMoving = true;
            timeToChangeDirection -= Time.deltaTime;

            if (timeToChangeDirection <= 0)
            {
                //ChangeDirection();
                test();
            }

            //rigidbody2D.velocity = transform.up * 2;
            //GetComponent<Rigidbody2D>().velocity = transform.up * 2;
            //GetComponent<Rigidbody2D>().velocity = new Vector3(1, 2, 0);//direction * 2;
            GetComponent<Rigidbody2D>().velocity = direction;
        } else
        {
            if(isMoving)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                isMoving = false;
            }
        }

    }

    private void test()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);
        direction = new Vector3(x, y, 0f);
        //if you need the vector to have a specific length:
        direction = direction.normalized * desiredLength;
        timeToChangeDirection = 1.5f;
    }

    private void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 newUp = quat * Vector3.up;
        newUp.z = 0;
        newUp.Normalize();
        newUp.z = 0;
        transform.up = newUp;
        timeToChangeDirection = 1.5f;
    }
}




/*
// Start is called before the first frame update
private float timeToChangeDirection;

// Use this for initialization
public void Start()
{
    ChangeDirection();
}

// Update is called once per frame
public void Update()
{
    timeToChangeDirection -= Time.deltaTime;

    if (timeToChangeDirection <= 0)
    {
        ChangeDirection();
    }

    //rigidbody2D.velocity = transform.up * 2;
    GetComponent<Rigidbody2D>().velocity = transform.up * 2;
}



private void ChangeDirection()
{
    float angle = Random.Range(0f, 360f);
    Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
    Vector3 newUp = quat * Vector3.up;
    newUp.z = 0;
    newUp.Normalize();
    transform.up = newUp;
    timeToChangeDirection = 1.5f;
}
*/

/*
     public float accelerationTime = 2f;
public float maxSpeed = 5f;
private Vector2 movement;
private float timeLeft;

void Update()
{
    timeLeft -= Time.deltaTime;
    if (timeLeft <= 0)
    {
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        timeLeft += accelerationTime;
    }
}

void FixedUpdate()
{
    GetComponent<Rigidbody2D>().AddForce(movement * maxSpeed);
}
 */
