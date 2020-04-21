using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollect : MonoBehaviour
{
    public Canvas powerUpUses;
    public Canvas powerUpPress;

    public static bool powerUpCollected = false;

    private bool staysOff = false;
    // Start is called before the first frame update
    void Start()
    {
        powerUpUses.gameObject.SetActive(false);
        powerUpPress.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpCollected && PowerUpEffect.counting != 0)
        { 
            powerUpUses.gameObject.SetActive(true);
        }

        if (PowerUpEffect.counting == 0)
        {
           StartCoroutine(Time()); 
        }

        if (powerUpCollected && !staysOff)
        {
            powerUpPress.gameObject.SetActive(true);
            StartCoroutine(TimePress());
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            powerUpCollected = true;
        }
    }
    
    IEnumerator Time()
    {
        yield return new WaitForSeconds(5);
        powerUpUses.gameObject.SetActive(false);
    }
    
    IEnumerator TimePress()
    {
        yield return new WaitForSeconds(10);
        powerUpPress.gameObject.SetActive(false);
        staysOff = true;
    }
    
}
