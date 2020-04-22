using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDAndRunTutorial : MonoBehaviour
{
    public Canvas text;
    public Canvas run;
    private PlayerInput input;
    private int counting = 0;
    
    void Start()
    {
        text.gameObject.SetActive(true);
        run.gameObject.SetActive(false);
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        if (counting == 0 && Vector3.Distance(input.inputMove, Vector3.zero) > 0)
        {
            counting++;

            StartCoroutine(Walk());
        }

        if (counting == 2 && input.inputRunSpeed > 0)
        {
            counting++;

            StartCoroutine(Run());
        }
        
        
    }

    IEnumerator Walk()
    {
        yield return new WaitForSeconds(1f);

        text.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        counting++;

        run.gameObject.SetActive(true);
    }
    
    IEnumerator Run()
    {
        yield return new WaitForSeconds(1f);

        run.gameObject.SetActive(false);
    }

}
