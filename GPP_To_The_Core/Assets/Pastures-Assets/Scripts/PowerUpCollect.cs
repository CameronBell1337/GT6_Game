using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollect : MonoBehaviour
{
    public Canvas powerUpUses;

    public static bool powerUpCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        powerUpUses.gameObject.SetActive(false);
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
}
