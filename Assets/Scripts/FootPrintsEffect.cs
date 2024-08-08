using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintsEffect : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns = 0.05f;
    public Transform parentToFootsteps;

    public GameObject[] footprintsLeft;
    public GameObject[] footprintsRight;
    private int nbrOfLeftSpawnTypes;
    private int nbrOfRightSpawnTypes;
    private int footprintObject = 0;

    private GameObject[] allInstanciatedObjects;
    private int amountOfStepsAtSameTime = 80;
    private int stepOnNow = 0;

    void Start()
    {
        nbrOfLeftSpawnTypes = footprintsLeft.Length;
        nbrOfRightSpawnTypes = footprintsRight.Length;
        allInstanciatedObjects = new GameObject[amountOfStepsAtSameTime];
    }

    // Update is called once per frame
    void Update()
    {
        if (WaterHandler.currentDepth <= 0.05)
        {
            if (PlayerController.playerMoving && !ActionHandler.doingBondingRitual)
            {


                if (timeBtwSpawns < 0)
                {

                    stepOnNow %= (amountOfStepsAtSameTime - 1);
                    //print(stepOnNow);
                    if (allInstanciatedObjects[stepOnNow] != null)
                    {
                        allInstanciatedObjects[stepOnNow].GetComponent<Animator>().SetBool("Fade", true);
                        allInstanciatedObjects[stepOnNow].GetComponent<Animator>().SetBool("Destroy", true);
                        //if(allInstanciatedObjects[(stepOnNow+1)%(allInstanciatedObjects.Length)] != null)
                        //{
                        //    allInstanciatedObjects[stepOnNow].GetComponent<Animator>().SetBool("Fade", true);
                        //}
                    }



                    Quaternion degrees = Quaternion.Euler(0, 0, getRotation());
                    footprintObject++;
                    int footSide = (footprintObject % 2);
                    if (footSide == 0)
                    {
                        GameObject footprint = footprintsLeft[Random.Range(0, footprintsLeft.Length)];
                        GameObject instance = (GameObject)Instantiate(footprint, transform.position, degrees);
                        instance.transform.parent = parentToFootsteps;
                        UpdateInstanceList(instance);
                    }
                    else
                    {
                        GameObject footprint = footprintsRight[Random.Range(0, footprintsRight.Length)];
                        GameObject instance = (GameObject)Instantiate(footprint, transform.position, degrees);
                        UpdateInstanceList(instance);
                        instance.transform.parent = parentToFootsteps;
                    }

                    timeBtwSpawns = startTimeBtwSpawns;
                }
                else
                {
                    timeBtwSpawns -= Time.deltaTime;
                }
            }

        }

    }

    private void UpdateInstanceList(GameObject instance)
    {
        allInstanciatedObjects[stepOnNow] = instance;
        stepOnNow++;
    }
    
    private float getRotation()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        float rotation = 0;

        if (xInput == 1 && yInput == 1)
        {
            rotation = -45;
        }
        else if (xInput == 1 && yInput == 0)
        {
            rotation = -90;
        }
        else if (xInput == 0 && yInput == 1)
        {
            rotation = 0;
        }
        else if (xInput == 0 && yInput == -1)
        {
            rotation = -180;
        }
        else if (xInput == -1 && yInput == 0)
        {
            rotation = 90;
        }
        else if (xInput == -1 && yInput == 1)
        {
            rotation = 45;
        }
        else if (xInput == 1 && yInput == -1)
        {
            rotation = -135;
        }
        else if (xInput == -1 && yInput == -1)
        {
            rotation = 135;
        }
        return rotation;
    }
}
