using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolumeTriggers : MonoBehaviour
{
    public bool canEnter;
    private Collider playerCol;

    private void Start()
    {
        canEnter = true;
        playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canEnter && other == playerCol && transform.name == "Start Trigger Volume")
        {
            canEnter = false;
            //gameObject.GetComponentInParent<SplinePlayerController>().BeginEnterSpline(true);
        }
        else if (canEnter && other == playerCol && transform.name == "End Trigger Volume")
        {
            canEnter = false;
            //gameObject.GetComponentInParent<SplinePlayerController>().BeginEnterSpline(false);
        }
        else if (!canEnter && other == playerCol)
        {
            //gameObject.GetComponentInParent<SplinePlayerController>().BeginExitSpline();
        }
    }
}
