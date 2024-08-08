using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFollowPlayer : MonoBehaviour

    
{
    public Transform pointToFollow;
    public GameObject maskToDisplace;
    public float maskDisplaceY;
    // Start is called before the first frame update
    void Start()
    {
        maskToDisplace.transform.position = new Vector3(maskToDisplace.transform.position.x, maskDisplaceY, maskToDisplace.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        maskToDisplace.transform.localPosition = new Vector3(maskToDisplace.transform.localPosition.x, maskDisplaceY, maskToDisplace.transform.localPosition.z);
        gameObject.transform.position = pointToFollow.position;//new Vector3(pointToFollow.position.x, pointToFollow.position.y  - offsetY, pointToFollow.position.z); 
    }
}
