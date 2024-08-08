using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDoughVersion2 : MonoBehaviour
{
    public GameObject[] doughPoints;
    //private Dictionary<string,GameObject> doughPoints;
    public Camera camToCheckMousePosOn;
    public float speed = 1;
    //private bool isPullingDough = false;

    private void Start()
    {
        //doughPoints = new Dictionary<string, GameObject>();
    }
    private void Update()
    {
        //Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = camToCheckMousePosOn.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        if (Input.GetMouseButton(0)) 
        {
            //isPullingDough = true;
            //Debug.Log("getting in here! found: ");
            foreach (GameObject dough in GetClosestDough(doughPoints))
            {
                //Debug.Log("found: " + entry.Key);
                //entry.Value.transform.position = this.gameObject.transform.position;

                float step = speed * Time.deltaTime;
                dough.transform.position = Vector3.MoveTowards(dough.transform.position, this.gameObject.transform.position, step);
                // do something with entry.Value or entry.Key
            }
        }    else
        {
            //isPullingDough = false;
        }
    }

    private GameObject[] GetClosestDough(GameObject[] doughPoints)
    {
        SortedDictionary<double, LinkedList<GameObject>> sortedDistances = new SortedDictionary<double, LinkedList<GameObject>>();
        Vector3 currentPos = transform.position;
        foreach(GameObject dough in doughPoints)
        {
            Vector3 directionToTarget = dough.transform.position - currentPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (sortedDistances.ContainsKey(dSqrToTarget)){
                sortedDistances[dSqrToTarget].AddLast(dough);
            } else
            {
                LinkedList<GameObject> tempList = new LinkedList<GameObject>();
                tempList.AddLast(dough);
                sortedDistances.Add(dSqrToTarget, tempList);
            } 
        }

        GameObject[] closestObjects = new GameObject[3];

        int count = 0;
        while (count < 2)
        {
            foreach (var pair in sortedDistances)
            {
                foreach (var dough in pair.Value)
                {
                    closestObjects[count] = dough;
                    count++;
                    if (count == 2)
                        break;
                }
                if (count == 2)
                    break;
            }
        }

        return closestObjects;

        /*
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;*/
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Found something!: " + collision.name);
        if (collision.tag == "Dough" && isPullingDough == false)
        {
            //Debug.Log("Found Dough piece!: " + collision.name);
            doughPoints.Add(collision.name, collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Dough" && isPullingDough == false)
        {
            if (doughPoints.ContainsKey(collision.name))
            {
                doughPoints.Remove(collision.name);
            }            
        }
    }
    */
}
