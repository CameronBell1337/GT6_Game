using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float mouseSens;
    public float camDistance;
    public Transform targetCam;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotSTime = 0.14f;

    Vector2 rotSVel;
    Vector3 currentRot;
    float xRot;
    float yRot;



    void LateUpdate()
    {
        xRot += Input.GetAxisRaw("Mouse X") * mouseSens;
        yRot -= Input.GetAxisRaw("Mouse Y") * mouseSens;
        yRot = Mathf.Clamp(yRot, pitchMinMax.x, pitchMinMax.y);

        currentRot = Vector2.SmoothDamp(currentRot, new Vector2(yRot, xRot), ref rotSVel, rotSTime);


        Vector2 targetRot = new Vector2(yRot, xRot);
        transform.eulerAngles = currentRot;

        transform.position = targetCam.position - transform.forward * camDistance;
    }
}
