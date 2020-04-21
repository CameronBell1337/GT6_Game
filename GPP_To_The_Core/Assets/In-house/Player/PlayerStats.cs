using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*[HideInInspector]*/ public float health;
    public bool hasSword;
    public respawnCheckpoint respawnPoint;

    [SerializeField] private float maxHeath;
    [SerializeField] private GameObject sheathedSword;
    private bool hasSwordEquipped;
    private Animator anim;
    private PlayerInput input;
    private PlayerMovement movementScript;

    void Start()
    {
        health = maxHeath;
        hasSwordEquipped = false;
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
        respawnPoint = FindObjectOfType<respawnCheckpoint>();
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

            StartCoroutine(DeathDelay());
        }

        // Respawn
        if (input.inputRespawn)
        {
            health = maxHeath;

            input.canInput = true;

            input.respawnOverride = false;
            if(anim.GetBool("Dead"))
            {
                anim.SetTrigger("Revived");
            }
            anim.SetBool("Dead", false);
            

            transform.position = respawnPoint.currentCheckpoint;
        }

        // Carrying sword enabled/disabled
        if (hasSword && !hasSwordEquipped)
        {
            hasSwordEquipped = true;
            sheathedSword.SetActive(true);
        }
        else if (!hasSword && hasSwordEquipped)
        {
            hasSwordEquipped = false;
            sheathedSword.SetActive(false);
        }
    }

    private void Respawn()
    {
        health = maxHeath;

        input.canInput = true;

        input.respawnOverride = false;
        if (anim.GetBool("Dead"))
        {
            anim.SetTrigger("Revived");
        }
        anim.SetBool("Dead", false);


        transform.position = respawnPoint.currentCheckpoint;
    }

    public void TakeDamage(Transform _enemy, float _damage)
    {
        if (health > 0)
        {
            health -= _damage;

            movementScript.KnockBack(_enemy);
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        Respawn();

    }
}
