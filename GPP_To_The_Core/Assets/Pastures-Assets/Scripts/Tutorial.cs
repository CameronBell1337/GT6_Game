using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Canvas text;
    private int counting = 0;

    public Canvas run;
    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(true);
        run.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Walk());
        }
        
        if (Input.GetKey(KeyCode.LeftShift) )
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(Run());
            }
            
        }
    }

    IEnumerator Walk()
    {
        yield return new WaitForSeconds(3);
        text.gameObject.SetActive(false);
        if (counting < 1)
        {
            run.gameObject.SetActive(true);  
            counting = 1 ;
        }
        
    }
    
    IEnumerator Run()
    {
        yield return new WaitForSeconds(3);
        run.gameObject.SetActive(false);
    }

}
