using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public LevelExitController levelExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            levelExitTrigger.has_key = true;
            Destroy(gameObject);
        }
    }
}
