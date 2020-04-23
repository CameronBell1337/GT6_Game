using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitController : MonoBehaviour
{
    public TrackController finalSpline;
    public levelLoader transition;

    public bool has_key = false;
    bool inRange = false;

    bool ending = false;

    void Start()
    {
        transition = FindObjectOfType<levelLoader>();
    }
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
                transition.LoadNextLevel();
                if (!ending)
                {
                    ending = true;
                    FindObjectOfType<SceneTransition>().LoadNextScene();
                }
            }
            else
            {
                finalSpline.direction *= -1;
                inRange = false;
            }
        }
    }
}
