using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class txtScript : MonoBehaviour
{
    public GameObject uiTXTHints;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(HintTxtDelay());
        }
    }

    IEnumerator HintTxtDelay()
    {
        uiTXTHints.SetActive(true);

        yield return new WaitForSeconds(3);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        Destroy(uiTXTHints);
        Destroy(gameObject);
    }
}
