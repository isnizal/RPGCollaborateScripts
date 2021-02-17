using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionControll : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraManager cam;

    //Transform PlayerPos;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraManager>();
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
        }
       
    }
}
