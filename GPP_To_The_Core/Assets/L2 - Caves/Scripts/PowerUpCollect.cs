using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollect : MonoBehaviour
{
    public static bool isMagnet = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            isMagnet = true;
        }
    }
}
