using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*[HideInInspector]*/ public float health;

    [SerializeField] private float maxHeath;
    private PlayerInput input;
    private PlayerMovement movementScript;

    void Start()
    {
        health = maxHeath;
        input = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Death
        if (health == 0)
        {
            health = -1;

            input.canInput = false;

            input.KillInput();

            input.respawnOverride = true;
        }

        // Respawn
        if (input.inputRespawn)
        {
            transform.position = Vector3.zero;

            health = maxHeath;

            input.respawnOverride = false;
            input.canInput = true;
        }
    }

    public void TakeDamage(Transform _enemy, float _damage)
    {
        if (health > 0)
        {
            health -= _damage;

            movementScript.KnockBack(_enemy);
        }
    }
}
