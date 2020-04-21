using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCamTrigger3 : MonoBehaviour
{
    public cameraManager triggerBool;

    private void Start()
    {
        triggerBool = FindObjectOfType<cameraManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //triggerBool.platform3 = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //triggerBool.platform3 = false;
        }
    }
}
