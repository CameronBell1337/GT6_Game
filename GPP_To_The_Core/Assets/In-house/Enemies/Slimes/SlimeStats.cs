using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStats : MonoBehaviour
{
    public float jumpRate;
    public bool spawnsMoreOnDeath;
    public int numberToSpawnOnDeath;
    public float spawnSpreadDistanceOnDeath;

    [SerializeField] private GameObject slimeToSpawnOnDeath;
    private Transform player;
    private Enemy enemyScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyScript = GetComponent<Enemy>();
    }

    public void spawnMoreOnDeath()
    {
        if (spawnsMoreOnDeath)
        {
            for (int i = 0; i < numberToSpawnOnDeath; i++)
            {
                Vector3 spawnPos = transform.position +
                    transform.right * ((-0.50f * (numberToSpawnOnDeath - 1)) + i) * spawnSpreadDistanceOnDeath;

                Instantiate(slimeToSpawnOnDeath, spawnPos, transform.rotation);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player)
        {
            player.GetComponent<PlayerStats>().TakeDamage(transform, enemyScript.attackDamage);
        }
    }
}
