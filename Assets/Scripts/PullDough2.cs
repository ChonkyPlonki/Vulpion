using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PullDough2 : MonoBehaviour
{
    public GameObject[] doughParts;
    public Camera camToCheckMousePosOn;
    public float speed = 1;

    private Dictionary<string, DoughPoint> doughPoints;
    // private bool isPullingDough = false;
    private SortedDictionary<float, DoughPoint> doughAngles;
    private SortedList<float, DoughPoint> listDoughAngles;
    private SortedList<float, DoughPoint> angleDivisions;
    private SortedList<float, DoughPoint> angleDivisions2;

    private KeyValuePair<float, DoughPoint>[] chosenDough;
    private KeyValuePair<float, DoughPoint>[] previousChosenDough;

    //private SortedDictionary<string, KeyValuePair<GameObject, Vector3>> doughOriginalPos;
    private SortedDictionary<string, DoughPoint> allDoughPoints;
    private Vector3 tempMousePos;

    public Transform DoughAllPos;
    public float xRange = 5;
    public float yRange = 3;
    //public float YMax = 73f;
    //public float YMin = 69f;
    //public float XMax = -36f;
    //public float XMin = -28f;


    //public float YMax = 20f;
    //public float YMin = 0f;
    //public float XMax = 14f;
    //public float XMin = -14f;

    //LBottom_Corner.position).y, transform.TransformPoint(LTop_Corner.position).y);
    //float clampedX = Mathf.Clamp(toMousePosition.y, transform.TransformPoint(LBottom_Corner.position).x, transform.TransformPoint(RTop_Corner

    //public Transform LBottom_Corner;
    //public Transform LTop_Corner;
    //public Transform RTop_Corner;


    private class DoughPoint {
        public string doughName;
        public GameObject doughObject;
        public Vector3 originalPosition;
        public Vector3 tempPosition;
        public float associatedAngle;
    }


private void Start()
    {
        CreateAllDoughtPoints();
        doughPoints = new Dictionary<string, DoughPoint>();
        doughAngles = new SortedDictionary<float, DoughPoint>();
        angleDivisions = new SortedList<float, DoughPoint>();
        listDoughAngles = new SortedList<float, DoughPoint>();
        CalculateDoughAngles();
    }

    private void CreateAllDoughtPoints()
    {
        allDoughPoints = new SortedDictionary<string, DoughPoint>();
        for (int i = 0; i < doughParts.Length; i++)
        {
            DoughPoint dough = new DoughPoint();
            dough.doughObject = doughParts[i];
            dough.doughName = doughParts[i].name;
            dough.originalPosition = doughParts[i].transform.position;

            allDoughPoints.Add(doughParts[i].name, dough);
        }
    }

    private void Update()
    {
        tempMousePos = camToCheckMousePosOn.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = new Vector3(tempMousePos.x, tempMousePos.y, 0);

        
        if (Input.GetMouseButton(0)) //If left mouse button held
        {
            SlowlyMorphBread();
        } 
        else if (Input.GetMouseButtonDown(1)) //If right mouse button clicked
        {
            NoteDoughsTempPositions();        
        } else if (Input.GetMouseButtonUp(1)) // If Let go of right mouse button up
        {
            //If we let go, no dough is chosen to be moved. It's reset.
            chosenDough = null;        
        } else if (Input.GetMouseButton(1)) // if Right mouse button helt
        {
            DragDoughWithMouse();         
        } else if (Input.GetMouseButtonDown(2)) //If middle mouse button clicked
        {
            ResetDoughPos();

        }
    }

    private void ResetDoughPos()
    {
        foreach (KeyValuePair<string, DoughPoint> dough in allDoughPoints)
        {
            dough.Value.doughObject.transform.position = dough.Value.originalPosition;
        }
    }

    private void DragDoughWithMouse()
    {
        Vector3 midPoint = CalculateCentroid(doughParts);
        Vector3 mouseDir = midPoint - tempMousePos;
        float mouseAng = GetAngle(mouseDir.x, mouseDir.y);
        mouseAng = findDoubleMod(mouseAng, 360);

        KeyValuePair<float, DoughPoint>[] doughToMove = DoughsCorrToAngle(mouseAng);
        previousChosenDough = chosenDough;
        chosenDough = doughToMove;


        if (previousChosenDough != null)
        {
            foreach (KeyValuePair<float, DoughPoint> dough in previousChosenDough)
            {
                DoughPoint tempD = dough.Value;
                tempD.doughObject.transform.position = tempD.tempPosition;
            }
        }


        foreach (KeyValuePair<float, DoughPoint> dough in chosenDough)
        {            
            Vector3 doughMidPoint = CalculateCentroid(doughParts);
            Vector3 toMousePosition = this.gameObject.transform.position;

            /*
            if (dough.Value.doughName == chosenDough[0].Value.doughName || dough.Value.doughName == chosenDough[2].Value.doughName)
            {
                Vector3 doughPosTemp = dough.Value.doughObject.transform.position;
                //float xDiff = (doughPosTemp.x - toMousePosition.x);
                //float yDiff = (doughPosTemp.y - toMousePosition.y);
                Vector3 midwayPos = new Vector3((doughPosTemp.x + toMousePosition.x) / 2, (doughPosTemp.y + toMousePosition.y) / 2);
                dough.Value.doughObject.transform.position = midwayPos;
                //Then dont take them the whole way to mouse, halfway there


            } else
            {
                dough.Value.doughObject.transform.position = toMousePosition;
            }*/

            //Limit position x and y
            //LTopCopy = Transform.TransformPoint();
            //float clampedY = Mathf.Clamp(toMousePosition.y, transform.TransformPoint(LBottom_Corner.position).y, transform.TransformPoint(LTop_Corner.position).y);
            //float clampedX = Mathf.Clamp(toMousePosition.y, transform.TransformPoint(LBottom_Corner.position).x, transform.TransformPoint(RTop_Corner.position).x);

            float clampedY = Mathf.Clamp(toMousePosition.y, DoughAllPos.position.y-yRange, DoughAllPos.position.y+yRange);
            float clampedX = Mathf.Clamp(toMousePosition.x, DoughAllPos.position.x-xRange, DoughAllPos.position.x+xRange);


            //Debug.Log(toMousePosition);
            //dough.Value.doughObject.transform.position = toMousePosition;
            dough.Value.doughObject.transform.position = new Vector3(clampedX,clampedY,toMousePosition.z);
        }
    }

    private void NoteDoughsTempPositions()
    {
        foreach (KeyValuePair<string, DoughPoint> dough in allDoughPoints)
        {
            dough.Value.tempPosition = dough.Value.doughObject.transform.position;
        }
    }

    private void SlowlyMorphBread()
    {
        Vector3 midPoint = CalculateCentroid(doughParts);
        Vector3 mouseDir = midPoint - tempMousePos;
        float mouseAng = GetAngle(mouseDir.x, mouseDir.y);
        mouseAng = findDoubleMod(mouseAng, 360);

        KeyValuePair<float, DoughPoint>[] doughToMove = DoughsCorrToAngle(mouseAng);
        //GameObject doughToPull = DoughCorrToAngle(mouseAng);
        float step = speed * Time.deltaTime;

        //New temp pos that is clamped
        float clampedY = Mathf.Clamp(this.gameObject.transform.position.y, DoughAllPos.position.y - yRange, DoughAllPos.position.y + yRange);
        float clampedX = Mathf.Clamp(this.gameObject.transform.position.x, DoughAllPos.position.x - xRange, DoughAllPos.position.x + xRange);
        Vector3 newTempPos = new Vector3(clampedX, clampedY, this.gameObject.transform.position.z);
        //dough.Value.doughObject.transform.position = new Vector3(clampedX, clampedY, toMousePosition.z);


        foreach (KeyValuePair<float, DoughPoint> dough in doughToMove)
        {
            //dough.Value.doughObject.transform.position = Vector3.MoveTowards(dough.Value.doughObject.transform.position, this.gameObject.transform.position, step);
            dough.Value.doughObject.transform.position = Vector3.MoveTowards(dough.Value.doughObject.transform.position, newTempPos, step);
        }
        //doughToPull.transform.position = Vector3.MoveTowards(doughToPull.transform.position, this.gameObject.transform.position, step);
    }

    private Vector3 CalcIntersection(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {

            // Line AB represented as a1x + b1y = c1 
            float a1 = B.y - A.y;
            float b1 = A.x - B.x;
            float c1 = a1 * (A.x) + b1 * (A.y);

            // Line CD represented as a2x + b2y = c2 
            float a2 = D.y - C.y;
            float b2 = C.x - D.x;
            float c2 = a2 * (C.x) + b2 * (C.y);

            float determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                // The lines are parallel. This is simplified 
                // by returning a pair of FLT_MAX 
                return new Vector3(0,0,0);
            }
            else
            {
                float x = (b2 * c1 - b1 * c2) / determinant;
                float y = (a1 * c2 - a2 * c1) / determinant;
                return new Vector3(x, y,0);
            }
    }

    private void CalculateDoughAngles()
    {
        AddDoughAnglesToList();
        MakeAngleDivisions();
    }

    private void AddDoughAnglesToList()
    {
        Vector3 midPoint = CalculateCentroid(doughParts);
        int nrDivides = doughParts.Length;
        foreach (GameObject doughPiece in doughParts)
        {
            Vector3 placement = doughPiece.transform.position;
            Vector3 dir = midPoint - placement;
            Vector2 temp = midPoint - placement;
            float theAngle = GetAngle(temp.x, temp.y);
            doughAngles.Add(theAngle, allDoughPoints[doughPiece.name]);
            listDoughAngles.Add(theAngle, allDoughPoints[doughPiece.name]);

            allDoughPoints[doughPiece.name].associatedAngle = theAngle;
        }
    }

    private void MakeAngleDivisions()
    {
        int counting = 0;
        foreach (KeyValuePair<float, DoughPoint> dAngle in doughAngles)
        {
            float prevAng = doughAngles.ElementAt(Mod(counting - 1, doughAngles.Count)).Key;
            float thisAng = dAngle.Key;

            float decidingAng = ((prevAng + thisAng) / 2);
           
            if (counting == 0)
            {
                decidingAng = (((360 + thisAng)+prevAng)/2);            
            }

            decidingAng = findDoubleMod(decidingAng, 360);

            DoughPoint assDough = dAngle.Value;
            angleDivisions.Add(decidingAng, assDough);

            counting++;
        }
    }


    private KeyValuePair<float, DoughPoint>[] DoughsCorrToAngle(float angle)
    {
        KeyValuePair<float, DoughPoint>[] corrDoughs = new KeyValuePair<float, DoughPoint>[3];
        KeyValuePair<float, DoughPoint>[] corrDoughPoints = new KeyValuePair<float, DoughPoint>[3];
        float greaterThanAng;

        if (angle > angleDivisions.Last().Key)
        {
            greaterThanAng = angleDivisions.First().Key;
        }
        else
        {
            greaterThanAng = angleDivisions.Keys.SkipWhile(print => print <= angle).First();
        }

        int index = angleDivisions.IndexOfKey(greaterThanAng);
        int prevIndex = Mod(index - 1, angleDivisions.Count);
        int nextIndex = Mod(index + 1, angleDivisions.Count);

        corrDoughPoints[0] = new KeyValuePair<float, DoughPoint>(angleDivisions.Keys[prevIndex], angleDivisions.Values[prevIndex]);
        corrDoughPoints[1] = new KeyValuePair<float, DoughPoint>(angleDivisions.Keys[index], angleDivisions.Values[index]);
        corrDoughPoints[2] = new KeyValuePair<float, DoughPoint>(angleDivisions.Keys[nextIndex], angleDivisions.Values[nextIndex]);

        return corrDoughPoints;
    }

    private float GetAngle(float x, float y)
    {
        float angle = (Mathf.Atan2(y,x) / Mathf.PI) * 180;        
        return angle + 180;
    }



    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(CalculateCentroid(doughParts), 1);
    }

    private Vector3 CalculateCentroid(GameObject[] centerPoints)
    {
        Vector3 centroid = new Vector3(0, 0, 0);
        var numPoints = centerPoints.Length;
        foreach (var point in centerPoints)
        {
            centroid += point.transform.position;
        }

        centroid /= numPoints;
        return centroid;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dough")
        {
            doughPoints.Add(collision.name, allDoughPoints[collision.name]);//collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Dough")
        {
            if (doughPoints.ContainsKey(collision.name))
            {
                doughPoints.Remove(collision.name);
            }            
        }
    }

    private int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    float findDoubleMod(float a, float b)
    {
        float mod;
        // Handling negative values 
        if (a < 0)
            mod = -a;
        else
            mod = a;
        if (b < 0)
            b = -b;

        // Finding mod by repeated subtraction 

        while (mod >= b)
            mod = mod - b;

        // Sign of result typically depends 
        // on sign of a. 
        if (a < 0)
            return -mod;

        return mod;
    }

}



























/*
 * 
 
        private DoughPoint DoughCorrToAngle(float angle)
    {
        float greaterThanAng;
        if (angle > angleDivisions.Last().Key)
        {
            greaterThanAng = angleDivisions.First().Key;
        } else
        {
            greaterThanAng = angleDivisions.Keys.SkipWhile(print => print <= angle).First();            
        }
        //Debug.Log("The first angle greatar than: " + greaterThanAng);
        return (angleDivisions[greaterThanAng]);
    }

     */




/*else
{

    foreach (KeyValuePair<float, GameObject> dough in chosenDough)
    {
        DoughPoint tempD = allDoughPoints[dough.Value.name];
        tempD.doughObject.transform.position = tempD.originalPosition;
    }

 } */


/*
 * 
 *         float firstDAng = chosenDough[0].Key;
        float lastDAng = chosenDough[2].Key;
        float step = (speed + 4) * Time.deltaTime;

Vector3 temp = doughMidPoint - toMousePosition;
float goalAngle = GetAngle(temp.x, temp.y);
Debug.Log(goalAngle);


if (goalAngle < firstDAng)
{
    Vector3 extendedDir = (allDoughPoints[chosenDough[2].Value.name].originalPosition) * 50;
    Vector3 newGoal = CalcIntersection(toMousePosition, dough.Value.transform.position, doughMidPoint, extendedDir);
    dough.Value.transform.position = newGoal;
}
else if (goalAngle > lastDAng)
{
    Vector3 extendedDir = (allDoughPoints[chosenDough[0].Value.name].originalPosition) * 50;
    Vector3 newGoal = CalcIntersection(toMousePosition, dough.Value.transform.position, doughMidPoint, extendedDir);
    dough.Value.transform.position = newGoal;
}
else
{
    dough.Value.transform.position = toMousePosition;
}
*/

/*
        foreach (KeyValuePair<float, GameObject> angle in doughAngles)
{
    Debug.Log(angle.Key + ", " + angle.Value);

}
*/



/*
//isPullingDough = true;
//Debug.Log("getting in here! found: ");
foreach (KeyValuePair<string,GameObject> entry in doughPoints)
{
    //Debug.Log("found: " + entry.Key);
    //entry.Value.transform.position = this.gameObject.transform.position;

    float step = speed * Time.deltaTime;
    entry.Value.transform.position = Vector3.MoveTowards(entry.Value.transform.position, this.gameObject.transform.position, step);
    // do something with entry.Value or entry.Key
}
*/

/*
 *     private void Start()
    {
        doughPoints = new Dictionary<string, GameObject>();
    }
    private void Update()
    {
        //Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = camToCheckMousePosOn.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        if (Input.GetMouseButton(0)) 
        {
            isPullingDough = true;
            //Debug.Log("getting in here! found: ");
            foreach (KeyValuePair<string,GameObject> entry in doughPoints)
            {
                //Debug.Log("found: " + entry.Key);
                //entry.Value.transform.position = this.gameObject.transform.position;

                float step = speed * Time.deltaTime;
                entry.Value.transform.position = Vector3.MoveTowards(entry.Value.transform.position, this.gameObject.transform.position, step);
                // do something with entry.Value or entry.Key
            }
        }    else
        {
            isPullingDough = false;
        }
    }

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
