using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float offsetHeight;
    public float offsetDistance;
    public float rotationSpeed;
    public float smoothing;
    public float aimSensitivityX;
    public float aimSensitivityY;
    public LayerMask groundLayers;
    public float collisionCamPadding;
    public float nsewModeCamSmoothing;
    public int cameraMode;
    [HideInInspector] public Vector3 baseOffset;
    [HideInInspector] public bool cameraOverride;

    private GameObject cameraTarget;
    private PlayerInput playerInputScript;
    private Vector3 offset;
    private float rotateX;
    //private float rotateY;
    private RaycastHit cameraCollisionHit;
    private bool controllerAimReset;
    private bool keyboardAimReset;

    enum cameraModeNames
    {
        thirdPersonFollow,
        nsewSoftLock
    }

    void Start()
    {
        cameraMode = 0;
        baseOffset = new Vector3(0, offsetHeight, -offsetDistance);
        cameraOverride = false;
        cameraTarget = GameObject.FindGameObjectWithTag("Player");
        playerInputScript = cameraTarget.GetComponent<PlayerInput>();
        offset = baseOffset;
        rotateX = 0;
        //rotateY = 0;
        controllerAimReset = true;
        keyboardAimReset = true;

        // Lock mouse to screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Switch between camera modes
        if (playerInputScript.inputSwitchCam)
        {
            if (cameraMode == (int)cameraModeNames.thirdPersonFollow)
            {
                cameraMode = (int)cameraModeNames.nsewSoftLock;
                offset = baseOffset;

                // Rotate camera to lock to closest NSEW direction
                Vector3 levelBodyPos = cameraTarget.transform.position;
                float playerToCameraYDif = transform.position.y - levelBodyPos.y;
                levelBodyPos += new Vector3(0, playerToCameraYDif, 0);
                Vector3 dir = ((cameraTarget.transform.position + baseOffset) - levelBodyPos).normalized;

                //Check each angle
                // North
                float angleToNorth = Vector3.Angle(dir, Vector3.forward);
                float closestDirectionAngle = angleToNorth;
                float angleToRotate = Vector3.SignedAngle(dir, Vector3.forward, Vector3.up); ;

                // South
                float angleToSouth = Vector3.Angle(dir, -Vector3.forward);
                if (angleToSouth < closestDirectionAngle)
                {
                    closestDirectionAngle = angleToSouth;
                    angleToRotate = Vector3.SignedAngle(dir, -Vector3.forward, Vector3.up);
                }

                // East
                float angleToEast = Vector3.Angle(dir, Vector3.right);
                if (angleToEast < closestDirectionAngle)
                {
                    closestDirectionAngle = angleToEast;
                    angleToRotate = Vector3.SignedAngle(dir, Vector3.right, Vector3.up);
                }

                // West
                float angleToWest = Vector3.Angle(dir, -Vector3.right);
                if (angleToWest < closestDirectionAngle)
                {
                    closestDirectionAngle = angleToWest;
                    angleToRotate = Vector3.SignedAngle(dir, -Vector3.right, Vector3.up);
                }

                // Update offset angle
                offset = Quaternion.AngleAxis(angleToRotate, Vector3.up) * offset;
            }
            else if (cameraMode == (int)cameraModeNames.nsewSoftLock)
            {
                cameraMode = (int)cameraModeNames.thirdPersonFollow;
                baseOffset = offset;
            }
        }
    }

    void FixedUpdate()
    {
        // Get centre pos of camera target
        Vector3 midBodyPos = cameraTarget.transform.position;
        midBodyPos += new Vector3(0, cameraTarget.GetComponent<Collider>().bounds.center.y -
                                  cameraTarget.GetComponent<Collider>().bounds.min.y, 0);
        // Get rotation
        rotateX = playerInputScript.inputAim.x * aimSensitivityX;
        //rotateY = playerInputScript.inputAim.y * aimSensitivityY;

        if (!cameraOverride)
        {
            switch (cameraMode)
            {
                default:
                case (int)cameraModeNames.thirdPersonFollow:
                    {
                        // Detect camera collision
                        if (detectCameraCollision())
                        {
                            Vector3 dir = ((cameraTarget.transform.position + baseOffset) - midBodyPos).normalized;
                            Vector3 paddingVec3 = dir * collisionCamPadding;
                            offset = (cameraCollisionHit.point + paddingVec3) - cameraTarget.transform.position;
                        }
                        else
                        {
                            offset = baseOffset;
                        }

                        // Rotate camera offset
                        offset = Quaternion.AngleAxis(rotateX * rotationSpeed, Vector3.up) * offset;
                        baseOffset = Quaternion.AngleAxis(rotateX * rotationSpeed, Vector3.up) * baseOffset;

                        // Smooth lerp camera between last position and new position
                        transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position + offset,
                                                          smoothing * Time.deltaTime);

                        break;
                    }
                case (int)cameraModeNames.nsewSoftLock:
                    {
                        // Check for let go of controller joystick && keybaord buttons before rotating again
                        if (playerInputScript.inputAim.x <= 0.75 &&
                            playerInputScript.inputAim.x >= -0.75)
                        {
                            controllerAimReset = true;
                        }
                        if (playerInputScript.inputKBAimHorizontal < 1 &&
                            playerInputScript.inputKBAimHorizontal > -1)
                        {
                            keyboardAimReset = true;
                        }

                        // Rotate camera offset
                        // Keyboard
                        if (playerInputScript.inputKBAimHorizontal == 1 && keyboardAimReset)
                        {
                            // Move right
                            keyboardAimReset = false;
                            offset = Quaternion.AngleAxis(90, Vector3.up) * offset;
                            baseOffset = offset;
                        }
                        else if (playerInputScript.inputKBAimHorizontal == -1 && keyboardAimReset)
                        {
                            // Move left
                            keyboardAimReset = false;
                            offset = Quaternion.AngleAxis(-90, Vector3.up) * offset;
                            baseOffset = offset;
                        }

                        // Controller
                        else if (playerInputScript.inputCtlrOnlyAim > 0.75 && controllerAimReset)
                        {
                            // Move right
                            controllerAimReset = false;
                            offset = Quaternion.AngleAxis(90, Vector3.up) * offset;
                            baseOffset = offset;
                        }
                        else if (playerInputScript.inputCtlrOnlyAim < -0.75 && controllerAimReset)
                        {
                            // Move left
                            controllerAimReset = false;
                            offset = Quaternion.AngleAxis(-90, Vector3.up) * offset;
                            baseOffset = offset;
                        }

                        // Smoooooth lerp camera between last position and new position
                        transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position + offset,
                                                          smoothing * nsewModeCamSmoothing * Time.deltaTime);

                        break;
                    }
            }

            // Make camera face player
            transform.LookAt(midBodyPos);
        }
    }

    bool detectCameraCollision()
    {
        Vector3 midBodyPos = cameraTarget.transform.position;
        midBodyPos += new Vector3(0, cameraTarget.GetComponent<Collider>().bounds.center.y -
                                  cameraTarget.GetComponent<Collider>().bounds.min.y, 0);
        Vector3 dir = ((cameraTarget.transform.position + baseOffset) - midBodyPos).normalized;
        float maxDist = Vector3.Distance(Vector3.zero, baseOffset);

        if (Physics.Raycast(midBodyPos, dir, out cameraCollisionHit, maxDist, groundLayers))
        {
            return true;
        }
        return false;
    }
}
