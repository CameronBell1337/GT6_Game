using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackTriggerText : MonoBehaviour
{

    private int counting = 0;
    public Canvas text;
    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && text.gameObject)
        {
            text.gameObject.SetActive(false);
            counting ++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("attackTrigger") && counting < 1)
        {
            text.gameObject.SetActive(true);
        }
    }
}
