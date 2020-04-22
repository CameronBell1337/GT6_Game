using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTutorial : MonoBehaviour
{
    public Canvas text;

    private int counting = 0;
    private PlayerInput input;

    void Start()
    {
        text.gameObject.SetActive(false);
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }
    
    void Update()
    {

        if (input.inputJump && text.gameObject)
        {
            text.gameObject.SetActive(false);
            counting = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("jumpTutorialTrigger") && counting < 1)
        {
            text.gameObject.SetActive(true);
        }
    }
}
