using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;

    public splineFollowPath currentWaypoint;
    
    //public Transform cameraMainPos;
    public bool isCutSceneActive = false;
    public bool isSplineActive = false;
    public bool isPlatformSceneActive = false;
    public bool platform1 = false;
    public bool platform2 = false;
    public bool platform3 = false;

    public GameObject mainVCamera;
    public GameObject[] vCams;
    



    void Start()
    {
        currentWaypoint = FindObjectOfType<splineFollowPath>();
        //isCutSceneActive = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isSplineActive)
        {
            vCams[3].SetActive(false);
            vCams[4].SetActive(false);
            vCams[5].SetActive(false);
            mainVCamera.SetActive(true);

        }
        if (isSplineActive)
        {
            mainVCamera.SetActive(false);
            if (currentWaypoint.waypointsIndex == 0 || currentWaypoint.waypointsIndex == 1)
            {
                vCams[3].SetActive(true);
            }
            if (currentWaypoint.waypointsIndex == 2 || currentWaypoint.waypointsIndex == 3 || currentWaypoint.waypointsIndex == 4)
            {
                vCams[4].SetActive(true);
                vCams[3].SetActive(false);
            }
            if (currentWaypoint.waypointsIndex > 4)
            {
                vCams[5].SetActive(true);
                vCams[4].SetActive(false);
            }
        }
        
        if (!isCutSceneActive)
        {
            vCams[1].SetActive(false);
            vCams[2].SetActive(false);
            mainVCamera.SetActive(true);
            /*transform.position = cameraMainPos.position;
            transform.rotation = cameraMainPos.rotation;
            mainCamera.fieldOfView = 70.0f;*/
        }
        if (isCutSceneActive)
        {
            vCams[1].SetActive(true);
            vCams[2].SetActive(true);
            mainVCamera.SetActive(false);
        }

        if(!isPlatformSceneActive)
        {
            vCams[6].SetActive(false);
            vCams[7].SetActive(false);
            vCams[8].SetActive(false);
            mainVCamera.SetActive(true);
        }
        if(isPlatformSceneActive)
        {
            mainVCamera.SetActive(false);
            if (platform1)
            {
                vCams[6].SetActive(true);
                
            }
            if(platform2)
            {
                vCams[7].SetActive(true);
            }
            if(platform3)
            {
                vCams[8].SetActive(true);
            }
        }

    }
}
