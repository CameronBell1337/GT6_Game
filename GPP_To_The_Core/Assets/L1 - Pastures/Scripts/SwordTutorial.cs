using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordTutorial : MonoBehaviour
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
        if (input.inputAction2 && text.gameObject)
        {
            StartCoroutine(Attack());
        }
    }
    
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.4f);
        text.gameObject.SetActive(false);
        counting++;
    }
}
