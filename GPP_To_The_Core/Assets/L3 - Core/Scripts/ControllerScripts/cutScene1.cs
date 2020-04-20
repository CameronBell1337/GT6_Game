using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutScene1 : MonoBehaviour
{
    //public cameraManager cutsceneBool;
    public Camera mainCam;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCam.GetComponent<cameraManager>().isCutSceneActive = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCam.GetComponent<cameraManager>().isCutSceneActive = false;
        }
    }
}
