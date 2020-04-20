using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformTrigger : MonoBehaviour
{
    public movingPlatforms triggerPlatform;
  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //triggerPlatform.autoStart = true;
            triggerPlatform.NextPlatform();
            gameObject.SetActive(false);
        }
    }

    /*void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggerPlatform.autoStart = true;
            Destroy(gameObject);
        }
    }*/
}
