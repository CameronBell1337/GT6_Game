using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject player;
    //public bool anotherCameraActive;
    public bool cutScene01Active;
    public bool cutScene02Active;
    public bool cutScene03Active;
    public bool cutScene04Active;
    public bool cutScene05Active;


    public GameObject mainVCamera;
    public GameObject[] vCams;

    void Start()
    {
        mainVCamera.SetActive(true);
        //anotherCameraActive = false;
        cutScene01Active = false;
        cutScene02Active = false;
        cutScene03Active = false;
        cutScene04Active = false;
        cutScene05Active = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cutScene01Active)
        {
            vCams[0].SetActive(cutScene01Active);
            vCams[1].SetActive(cutScene01Active);
            vCams[2].SetActive(cutScene01Active);
            mainVCamera.SetActive(false);
        }
        else
        {
            mainVCamera.SetActive(true);
            vCams[0].SetActive(cutScene01Active);
            vCams[1].SetActive(cutScene01Active);
            vCams[2].SetActive(cutScene01Active);
        }

        if (cutScene02Active)
        {
            vCams[3].SetActive(cutScene01Active);
            vCams[4].SetActive(cutScene01Active);
            vCams[5].SetActive(cutScene01Active);
            mainVCamera.SetActive(false);
        }
        else
        {
            mainVCamera.SetActive(true);
            vCams[3].SetActive(cutScene01Active);
            vCams[4].SetActive(cutScene01Active);
            vCams[5].SetActive(cutScene01Active);

        }

        if (cutScene03Active)
        {
            vCams[6].SetActive(cutScene01Active);
            vCams[7].SetActive(cutScene01Active);
            vCams[8].SetActive(cutScene01Active);
            mainVCamera.SetActive(false);
        }
        else 
        {
            mainVCamera.SetActive(true);
            vCams[6].SetActive(cutScene01Active);
            vCams[7].SetActive(cutScene01Active);
            vCams[8].SetActive(cutScene01Active);
        }

        if (cutScene04Active)
        {
            vCams[9].SetActive(cutScene01Active);
            vCams[10].SetActive(cutScene01Active);
            vCams[11].SetActive(cutScene01Active);
            mainVCamera.SetActive(false);
        }
        else
        {
            mainVCamera.SetActive(true);
            vCams[9].SetActive(cutScene01Active);
            vCams[10].SetActive(cutScene01Active);
            vCams[11].SetActive(cutScene01Active);

        }

        if (cutScene05Active)
        {
            vCams[12].SetActive(cutScene01Active);
            vCams[13].SetActive(cutScene01Active);
            vCams[14].SetActive(cutScene01Active);
            mainVCamera.SetActive(false);
        }
        else 
        {
            mainVCamera.SetActive(true);
            vCams[12].SetActive(cutScene01Active);
            vCams[13].SetActive(cutScene01Active);
            vCams[14].SetActive(cutScene01Active);

        }
    }
    IEnumerator VCamDelay()
    {
        yield return new WaitForSeconds(1);
        mainVCamera.SetActive(false);
    }
}
