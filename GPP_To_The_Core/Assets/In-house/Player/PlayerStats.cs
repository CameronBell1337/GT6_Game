using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*[HideInInspector]*/ public float health;

    [SerializeField] private float maxHeath;
    private Animator anim;
    private PlayerInput input;
    private PlayerMovement movementScript;

    void Start()
    {
        health = maxHeath;
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Death
        if (health <= 0 && !anim.GetBool("Dead"))
        {
            health = 0;

            input.canInput = false;
            input.KillInput();

            input.respawnOverride = true;

            anim.SetBool("Dead", true);
            anim.SetTrigger("Died");
        }

        // Respawn
        if (input.inputRespawn)
        {
            health = maxHeath;

            input.canInput = true;

            input.respawnOverride = false;

            anim.SetBool("Dead", false);
            anim.SetTrigger("Revived");

            transform.position = Vector3.zero;
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
