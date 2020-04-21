using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PunchTutorial : MonoBehaviour
{

    private int counting = 0;
    private PlayerInput input;
    public Canvas text;
    
    void Start()
    {
        text.gameObject.SetActive(false);
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        if (input.inputAction1 && text.gameObject)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("punchTutorialTrigger") && counting < 1)
        {
            text.gameObject.SetActive(true);
        }
    }
    
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.8f);
        text.gameObject.SetActive(false);
        counting++;
    }
}
