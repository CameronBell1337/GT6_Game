﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MinecartTrigger : MonoBehaviour
{
    [Header("Player Position References")]
    public Transform playerPositionTarget;
    public Transform playerExitPosition;

    [Header("UI Objects")]
    public CanvasGroup getInUI;
    public CanvasGroup getOutUI;

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

        getInUI.alpha = 0;
        getOutUI.alpha = 0;
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
        HandleUI();

        if (Input.GetButtonDown("Action 1") && playerInRange && !trackController.active)
        {
            if (player.transform.parent == null)
            {
                trackController.active = true;
                trackController.DisableColliders();
                player.transform.position = playerPositionTarget.position;
                player.transform.parent = transform;
                player.GetComponent<PlayerInput>().canInput = false;
                player.GetComponent<PlayerInput>().KillInput();
            }
            else
            {
                trackController.EnableColliders();
                player.transform.parent = null;
                player.transform.position = playerExitPosition.position;
                player.GetComponent<PlayerInput>().canInput = true;
            }
        }

        if (trackController.active)
        {
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

    void HandleUI()
    {
        if (playerInRange && !trackController.active)
        {
            // NOT IN MINECART
            if (player.transform.parent == null)
            {
                UIFadeIn(getInUI);
                UIFadeOut(getOutUI);
                return;
            }
            // IN MINECART
            else
            {
                UIFadeIn(getOutUI);
                UIFadeOut(getInUI);
                return;
            }
        }

        if (trackController.active)
        {
            UIFadeOut(getInUI);
            UIFadeOut(getOutUI);
        }
    }

    void UIFadeIn(CanvasGroup ui)
    {
        float new_alpha = Mathf.Lerp(ui.alpha, 2, 2 * Time.deltaTime);

        if (new_alpha > 1.95f)
        {
            new_alpha = 2;
        }

        ui.alpha = new_alpha;
    }

    void UIFadeOut(CanvasGroup ui)
    {
        float new_alpha = Mathf.Lerp(ui.alpha, 0, 2 * Time.deltaTime);

        if (new_alpha > 0.05f)
        {
            new_alpha = 0;
        }

        ui.alpha = new_alpha;
    }
}
