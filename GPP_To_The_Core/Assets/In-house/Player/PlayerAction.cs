using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerAction : MonoBehaviour
{
    public float punchDamage;
    public float punchReach;

    private Animator anim;
    private CapsuleCollider col;
    private PlayerInput inputScript;
    private PlayerStats stats;
    private int lastAttack;
    private Camera mainCamera;
    private Camera panningCamera;

    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        lastAttack = 0;
    }

    void Update()
    {
        // Check for action input
        if (inputScript.inputAction)
        {
            // Do an action
            if (CanAttack())
            {
                Attack();
            }
        }
    }

    private bool CanAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(2).IsName("Passive"))
        {
            return true;
        }
        return false;
    }

    private void Attack()
    {
        // Play animation
        switch (lastAttack)
        {
            case 0:
                anim.SetTrigger("Attack1");
                lastAttack = 1;
                break;
            case 1:
                anim.SetTrigger("Attack2");
                lastAttack = 2;
                break;
            case 2:
                anim.SetTrigger("Attack3");
                lastAttack = 3;
                break;
            case 3:
                anim.SetTrigger("Attack1");
                lastAttack = 1;
                break;
        }
    }

    private void Hit()
    {
        // Check to deal damage
        Vector3 origin = transform.position + Vector3.up * col.height * 0.40f;

        if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, punchReach) &&
                    hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //hit.transform.GetComponent<EnemyAttackHandler>().DealDamage(hit.transform, punchDamage);
        }
    }
}
