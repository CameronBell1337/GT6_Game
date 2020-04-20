using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public bool inputJump;
    [HideInInspector] public bool inputAction1;
    [HideInInspector] public bool inputAction2;
    [HideInInspector] public Vector2 inputAim;
    [HideInInspector] public float inputKBAimHorizontal;
    [HideInInspector] public float inputCtlrOnlyAim;
    [HideInInspector] public Vector3 inputMove;
    [HideInInspector] public float inputRunSpeed;
    [HideInInspector] public bool inputSwitchCam;
    [HideInInspector] public bool inputRespawn;
    [HideInInspector] public bool canInput;
    [HideInInspector] public bool canAim;
    [HideInInspector] public bool movementLockedToHorizontal;
    [HideInInspector] public bool respawnOverride;
    public float aimControllerMultiplier;

    private GameObject followCamera;

    void Start()
    {
        canInput = true;
        canAim = true;
        followCamera = GameObject.FindGameObjectWithTag("MainCamera");
        movementLockedToHorizontal = false;
        respawnOverride = false;
    }

    void Update()
    {
        // If player can input/isn't frozen, update variables
        if (canInput)
        {
            // Get basic inputs
            inputJump = Input.GetButtonDown("Jump");
            inputAction1 = Input.GetButtonDown("Action 1");
            inputAction2 = Input.GetButtonDown("Action 2");
            inputRunSpeed = Input.GetAxisRaw("Run");
            inputSwitchCam = Input.GetButtonDown("Camera Mode Switch");
            //inputRespawn = Input.GetButtonDown("Respawn");

            if (canAim)
            {
                inputAim = new Vector2((Input.GetAxisRaw("Aim Horizontal") * aimControllerMultiplier) +
                                    Input.GetAxisRaw("Mouse X"),
                                    (Input.GetAxisRaw("Aim Vertical") * aimControllerMultiplier) +
                                    Input.GetAxisRaw("Mouse Y"));

                inputKBAimHorizontal = Input.GetAxisRaw("KB Aim Horizontal");
                inputCtlrOnlyAim = Input.GetAxisRaw("Aim Horizontal");
            }

            // Get movement input
            // Forward vector relative to follow camera along x-z plane
            Vector3 forward = followCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;
            // Right vector relative to follow camera
            Vector3 right = new Vector3(forward.z, 0, -forward.x);

            if (movementLockedToHorizontal)
            {
                inputMove = (Input.GetAxisRaw("Horizontal") * right);
            }
            else
            {
                inputMove = right * Input.GetAxisRaw("Horizontal") + forward * Input.GetAxisRaw("Vertical");
            }
        }
        else if (respawnOverride)
        {
            inputRespawn = Input.GetButtonDown("Respawn");
        }
    }

    public void KillMoveInput()
    {
        inputRunSpeed = 0;
        inputMove = Vector3.zero;
    }

    public void KillInput()
    {
        inputJump = false;
        inputAction1 = false;
        inputAction2 = false;
        inputAim = Vector2.zero;
        inputKBAimHorizontal = 0;
        inputCtlrOnlyAim = 0;
        inputMove = Vector3.zero;
        inputRunSpeed = 0;
        inputSwitchCam = false;
        inputRespawn = false;
    }
}
