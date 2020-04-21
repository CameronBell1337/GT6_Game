using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateCheckpoint : MonoBehaviour
{
    public Vector3 currentCheckpoint;
    public respawnCheckpoint respawnPoint;

    void Start()
    {
        respawnPoint = FindObjectOfType<respawnCheckpoint>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            respawnPoint.SetSpawnPoint(transform.position);
            Destroy(gameObject);
        }
    }

}
