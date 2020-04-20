using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineTriggerEnd : MonoBehaviour
{
    public GameObject player;
    public Camera mainCam;
    public GameObject triggerStartObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(triggerStartObj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCam.GetComponent<cameraManager>().isSplineActive = false;
            player.GetComponent<ThirdPersonCharacterController>().enabled = true;
            player.GetComponent<splineFollowPath>().enabled = false;
            player.GetComponent<splineMovementController>().enabled = false;
            
            Destroy(gameObject);
        }
    }
}
