using UnityEngine;
using System.Collections;

public class StolenMoveCamera : MonoBehaviour
{

    //public float dampTime = 0.15f;
    //private Vector3 velocity = Vector3.zero;
    //public Transform target;
    //public Camera cameraFollowing;

    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;
    public GameObject player;
    private Rigidbody2D rb2d;

    public float cameraDistanceMax = 15f;
    public float cameraDistanceMin = 8f;
    public float scrollSpeed = 3f;
    private float cameraZoom = 10f;

    private Vector3 newPosition;
    private Camera cam;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        rb2d = player.GetComponent<Rigidbody2D>();
        cam = this.GetComponent<Camera>();
    }

    public float FollowSpeed = 2f;
    public Transform Target;

    private void LateUpdate()
    {
        newPosition = rb2d.transform.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
        HandleScroll();
    }

    private void HandleScroll()
    {
        cameraZoom -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        cameraZoom = Mathf.Clamp(cameraZoom, cameraDistanceMin, cameraDistanceMax);
        cam.orthographicSize = cameraZoom;
        //newPosition.z = cameraDistance;
    }
}