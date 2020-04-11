using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitController : MonoBehaviour
{
    public TrackController finalSpline;

    [HideInInspector]
    public bool has_key = false;
    bool inRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inRange = false;
        }
    }

    private void Update()
    {
        if (inRange)
        {
            if (has_key)
            {
                // TODO: EXIT LEVEL
            }
            else
            {
                finalSpline.direction *= -1;
                inRange = false;
            }
        }
    }
}
