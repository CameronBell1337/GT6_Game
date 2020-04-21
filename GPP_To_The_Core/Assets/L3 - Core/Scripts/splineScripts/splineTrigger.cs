using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineTrigger : MonoBehaviour
{
    public GameObject player;
    public Camera mainCam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //mainCam.GetComponent<cameraManager>().isSplineActive = true;
            player.GetComponent<splineFollowPath>().enabled = true;
            player.GetComponent<splineMovementController>().enabled = true;
            player.GetComponent<ThirdPersonCharacterController>().enabled = false;
        }
    }

    
}
