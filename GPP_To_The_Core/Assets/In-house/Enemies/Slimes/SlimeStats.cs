using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStats : MonoBehaviour
{
    public float jumpRate;
    public float damage;
    public bool spawnsMoreOnDeath;
    public int numberToSpawnOnDeath;
    public float spawnSpreadDistanceOnDeath;

    [SerializeField] private GameObject slimeToSpawnOnDeath;
    private SlimeMovement movementScript;
    private Transform player;

    private void Start()
    {
        movementScript = GetComponent<SlimeMovement>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Death
        if (health == 0)
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

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;

        movementScript.Knockback();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player)
        {
            player.GetComponent<PlayerStats>().TakeDamage(transform, damage);
        }
    }
}
