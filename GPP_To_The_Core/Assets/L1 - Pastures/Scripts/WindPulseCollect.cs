using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPulseCollect : MonoBehaviour
{
    public Canvas windPulseTutorialCanvas;
    public Canvas windPulseUsesCanvas;

    public bool windPulseCollected = false;

    private WindPulseEffect effector;
    private bool staysOff = false;
    
    void Start()
    {
        windPulseTutorialCanvas.gameObject.SetActive(false);
        windPulseUsesCanvas.gameObject.SetActive(false);

        effector = GetComponent<WindPulseEffect>();
    }
    
    void Update()
    {
        if (windPulseCollected && effector.counting != 0)
        {
            windPulseUsesCanvas.gameObject.SetActive(true);
        }

        if (effector.counting == 0)
        {
           StartCoroutine(Time());
        }

        if (windPulseCollected && !staysOff)
        {
            windPulseTutorialCanvas.gameObject.SetActive(true);
            StartCoroutine(TimePress());
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WindPulsePowerUp"))
        {
            Destroy(other.gameObject);
            windPulseCollected = true;
        }
    }

    IEnumerator TimePress()
    {
        yield return new WaitForSeconds(10);
        windPulseTutorialCanvas.gameObject.SetActive(false);
        staysOff = true;
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(5);
        windPulseUsesCanvas.gameObject.SetActive(false);
    }
    
}
