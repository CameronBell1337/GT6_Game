using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriggerText : MonoBehaviour
{
    public Canvas text;

    private int counting = 0;
    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && text.gameObject)
        {
            text.gameObject.SetActive(false);
            counting = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("jumpTrigger") && counting < 1)
        {
            text.gameObject.SetActive(true);
        }
    }
}
