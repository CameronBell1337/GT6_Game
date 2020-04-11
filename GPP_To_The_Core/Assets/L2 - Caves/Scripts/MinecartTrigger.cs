using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MinecartTrigger : MonoBehaviour
{
    [Header("Player Position References")]
    public Transform playerPositionTarget;
    public Transform playerExitPosition;
    
    bool playerInRange = false;

    [HideInInspector]
    public TrackController trackController;
    GameObject player;
    Animator minecartAnimator;

    [Header("Tilting")]
    public float tiltSpeed = 3;
    Vector3 maxTiltPosition = new Vector3(0, 0.15f, 0);
    Vector3 maxTiltRotation = new Vector3(0, 0, 20);

    private void Awake()
    {
        trackController = GetComponentInParent<TrackController>();
        player = GameObject.FindGameObjectWithTag("Player");
        minecartAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
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

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            trackController.tiltPositionOffset = Vector3.Lerp(trackController.tiltPositionOffset, maxTiltPosition, tiltSpeed * Time.deltaTime);
            trackController.tiltRotationOffset = Vector3.Lerp(trackController.tiltRotationOffset, maxTiltRotation * trackController.direction, tiltSpeed * Time.deltaTime);
            trackController.tiltDirection = trackController.direction;
        }
        else if (Input.GetAxis("Horizontal") > 0.1f)
        {
            trackController.tiltPositionOffset = Vector3.Lerp(trackController.tiltPositionOffset, maxTiltPosition, tiltSpeed * Time.deltaTime);
            trackController.tiltRotationOffset = Vector3.Lerp(trackController.tiltRotationOffset, -maxTiltRotation * trackController.direction, tiltSpeed * Time.deltaTime);
            trackController.tiltDirection = -trackController.direction;
        }
        else
        {
            trackController.tiltPositionOffset = Vector3.Lerp(trackController.tiltPositionOffset, Vector3.zero, tiltSpeed * Time.deltaTime);
            trackController.tiltRotationOffset = Vector3.Lerp(trackController.tiltRotationOffset, Vector3.zero, tiltSpeed * Time.deltaTime);
            trackController.tiltDirection = 0;
        }
    }
}
