﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetTrigger : MonoBehaviour
{
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player1 V2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PowerUpCollect.isMagnet)
        {
            StartCoroutine(SetMagnetOff());
            if (Vector3.Distance(transform.position, Player.position) < 5)
            {
              transform.position = Vector3.MoveTowards(transform.position, Player.position, .3f);  
            }

            if (transform.position == Player.position)
            {
                Destroy(gameObject);
            }
            
        }
    }

    IEnumerator SetMagnetOff()
    {
        yield return new WaitForSeconds(15);
        PowerUpCollect.isMagnet = false;
    }
}