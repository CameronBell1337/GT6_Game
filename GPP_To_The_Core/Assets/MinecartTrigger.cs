using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MinecartTrigger : MonoBehaviour
{
    public Transform playerPositionTarget;
    public Transform playerExitPosition;
    
    bool playerInRange = false;

    TrackController trackController;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trackController = GetComponentInParent<TrackController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("AttackL") && playerInRange && !trackController.active)
        {
            if (player.transform.parent == null)
            {
                trackController.active = true;
                player.transform.position = playerPositionTarget.position;
                player.transform.parent = transform;
            }
            else
            {
                player.transform.parent = null;
                player.transform.position = playerExitPosition.position;
            }
        }
    }
}
