using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneTransitionTrigger : MonoBehaviour
{
    public levelLoader transition;
    void Start()
    {
        transition = FindObjectOfType<levelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transition.LoadNextLevel();
        }
    }
}
