using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
   
    
    public bool cutScene01Active;
    public bool cutScene02Active;
    public bool cutScene03Active;
    public bool cutScene04Active;
    public bool cutScene05Active;

    public GameObject player;
    private bool anotherCameraActive;

    public GameObject mainVCamera;
    public GameObject[] vCams;

    void Start()
    {
        //mainVCamera.SetActive(true);

        player = GameObject.FindGameObjectWithTag("Player");

        anotherCameraActive = false;

        cutScene01Active = false;
        cutScene02Active = false;
        cutScene03Active = false;
        cutScene04Active = false;
        cutScene05Active = false;
    }

    void FixedUpdate()
    {
        if(cutScene01Active || cutScene02Active || cutScene03Active || cutScene04Active || cutScene05Active)
        {
            anotherCameraActive = true;
        }
        else if (!cutScene01Active || !cutScene02Active || !cutScene03Active || !cutScene04Active || !cutScene05Active)
        {
            anotherCameraActive = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(anotherCameraActive)
        {
            //StartCoroutine(VCamDelay());
            mainVCamera.SetActive(false);
        }
        
        if(!anotherCameraActive)
        {
            //StartCoroutine(VCamDelay());
            mainVCamera.SetActive(true);
        }

        if (cutScene01Active && vCams.Length > 0)
        {
            vCams[0].SetActive(cutScene01Active);
            vCams[1].SetActive(cutScene01Active);
            vCams[2].SetActive(cutScene01Active);
            //mainVCamera.SetActive(false);
        }
        else if(!anotherCameraActive && vCams.Length > 0)
        {
            //StartCoroutine(VCamDelay());
            vCams[0].SetActive(cutScene01Active);
            vCams[1].SetActive(cutScene01Active);
            vCams[2].SetActive(cutScene01Active);
        }

        if (cutScene02Active && vCams.Length >= 3)
        {
            vCams[3].SetActive(cutScene02Active);
            vCams[4].SetActive(cutScene02Active);
            vCams[5].SetActive(cutScene02Active);
            //mainVCamera.SetActive(false);
        }
        else if (!anotherCameraActive && vCams.Length >= 3)
        {
            //StartCoroutine(VCamDelay());
            vCams[3].SetActive(cutScene02Active);
            vCams[4].SetActive(cutScene02Active);
            vCams[5].SetActive(cutScene02Active);

        }

        if (cutScene03Active && vCams.Length >= 6)
        {
            vCams[6].SetActive(cutScene03Active);
            vCams[7].SetActive(cutScene03Active);
            vCams[8].SetActive(cutScene03Active);
            //mainVCamera.SetActive(false);
        }
        else if (!anotherCameraActive && vCams.Length >= 6)
        {
            //StartCoroutine(VCamDelay());
            vCams[6].SetActive(cutScene03Active);
            vCams[7].SetActive(cutScene03Active);
            vCams[8].SetActive(cutScene03Active);
        }

        if (cutScene04Active && vCams.Length >= 9)
        {
            vCams[9].SetActive(cutScene04Active);
            vCams[10].SetActive(cutScene04Active);
            vCams[11].SetActive(cutScene04Active);
            //mainVCamera.SetActive(false);
        }
        else if (!anotherCameraActive && vCams.Length >= 9)
        {
            //StartCoroutine(VCamDelay());
            vCams[9].SetActive(cutScene04Active);
            vCams[10].SetActive(cutScene04Active);
            vCams[11].SetActive(cutScene04Active);

        }

        if (cutScene05Active && vCams.Length >= 12)
        {
            vCams[12].SetActive(cutScene05Active);
            vCams[13].SetActive(cutScene05Active);
            vCams[14].SetActive(cutScene05Active);
            //mainVCamera.SetActive(false);
        }
        else if (!anotherCameraActive && vCams.Length >= 12)
        {
            //StartCoroutine(VCamDelay());
            vCams[12].SetActive(cutScene05Active);
            vCams[13].SetActive(cutScene05Active);
            vCams[14].SetActive(cutScene05Active);

        }

        
    }
    IEnumerator VCamDelay()
    {
        player.GetComponent<PlayerInput>().canAim = false;
        yield return new WaitForSeconds(1);
        player.GetComponent<PlayerInput>().canAim = true;
    }
}
