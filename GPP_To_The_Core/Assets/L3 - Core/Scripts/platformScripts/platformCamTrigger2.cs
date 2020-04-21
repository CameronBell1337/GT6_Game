using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCamTrigger2 : MonoBehaviour
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
            //triggerBool.platform2 = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //triggerBool.platform2 = false;
        }
    }
}
