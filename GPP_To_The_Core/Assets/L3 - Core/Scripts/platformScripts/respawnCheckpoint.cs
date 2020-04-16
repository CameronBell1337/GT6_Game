using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnCheckpoint : MonoBehaviour
{
    public float maxFallDistance = -14.0f;
   public bool isRespawning = false;
    public Vector3 currentCheckpoint;

   
    void FixedUpdate()
    {
        if (transform.position.y <= maxFallDistance)
        {
            RespawnAtCheckpoint();
        }
    }
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        currentCheckpoint = newSpawnPoint;
    }

    void RespawnAtCheckpoint()
    {
        transform.position = currentCheckpoint;
    }

    
}
