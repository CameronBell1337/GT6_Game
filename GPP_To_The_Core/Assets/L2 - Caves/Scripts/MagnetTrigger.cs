using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetTrigger : MonoBehaviour
{
    private Transform player;
    private PowerUpCollect collectScript;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        collectScript = player.GetComponent<PowerUpCollect>();
    }
    
    void Update()
    {
        if (collectScript.isMagnet)
        {
            StartCoroutine(SetMagnetOff());
            if (Vector3.Distance(transform.position, player.position) < 5)
            {
              transform.position = Vector3.MoveTowards(transform.position, player.position, .3f);  
            }

            if (transform.position == player.position)
            {
                Destroy(gameObject);
            }
            
        }
    }

    IEnumerator SetMagnetOff()
    {
        yield return new WaitForSeconds(45);
        collectScript.isMagnet = false;
    }
}