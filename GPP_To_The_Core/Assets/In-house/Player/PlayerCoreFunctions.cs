using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCoreFunctions : MonoBehaviour
{
    public respawnCheckpoint respawnPoint;

    [SerializeField] private GameObject sheathedSword;
    private bool hasSwordEquipped;
    private Animator anim;
    private PlayerInput input;
    private PlayerMovement movementScript;

    void Start()
    {
        respawnPoint = FindObjectOfType<respawnCheckpoint>();

        hasSwordEquipped = false;
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Death
        if (PlayerStats.health <= 0 && !anim.GetBool("Dead"))
        {
            PlayerStats.health = 0;

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
            PlayerStats.health = PlayerStats.maxHeath;

            input.canInput = true;

            input.respawnOverride = false;
            if (anim.GetBool("Dead"))
            {
                anim.SetTrigger("Revived");
            }
            anim.SetBool("Dead", false);


            transform.position = respawnPoint.currentCheckpoint;
        }

        // Carrying sword enabled/disabled
        if (PlayerStats.hasSword && !hasSwordEquipped)
        {
            hasSwordEquipped = true;
            sheathedSword.SetActive(true);
        }
        else if (!PlayerStats.hasSword && hasSwordEquipped)
        {
            hasSwordEquipped = false;
            sheathedSword.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name != "L4 - End Scene")
        {
            PlayerStats.timer += Time.deltaTime;
        }
    }

    private void Respawn()
    {
        PlayerStats.health = PlayerStats.maxHeath;

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
        if (PlayerStats.health > 0)
        {
            PlayerStats.health -= _damage;

            movementScript.KnockBack(_enemy);
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        Respawn();

    }
}
