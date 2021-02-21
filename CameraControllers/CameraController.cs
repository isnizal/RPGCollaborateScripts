using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject thePlayer;
    public Transform lookAtTarget;
    private CinemachineVirtualCamera vcam;
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        AttachCam();
    }

    void AttachCam()
	{
        if (thePlayer == null)
		{
            thePlayer = GameObject.FindWithTag("Player");
            if(thePlayer != null)
			{
                lookAtTarget = thePlayer.transform;
                vcam.Follow = lookAtTarget;
			}
		}
	}
}
