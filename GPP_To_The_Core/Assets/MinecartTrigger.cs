using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MinecartTrigger : MonoBehaviour
{
    TrackController trackController;

    private void Awake()
    {
        trackController = GetComponentInParent<TrackController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            trackController.playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            trackController.playerInRange = false;
        }
    }
}
