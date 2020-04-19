﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerAction : MonoBehaviour
{
    public float punchDamage;
    public float punchReach;
    public float punchChainWindowStart;
    public float punchChainWindowEnd;
    public float swordDamage;
    public float swordSwingChainWindowStart;
    public float swordSwingChainWindowEnd;

    [SerializeField] private GameObject sheathedSword;
    [SerializeField] private GameObject armedSword;
    private Animator anim;
    private CapsuleCollider col;
    private PlayerInput inputScript;
    private Camera mainCamera;
    private Attacks lastAttack;
    private float punchTimer;
    private bool punchQueued;
    private float armedTimer;
    private float swordSwingTimer;
    private bool swordSwingQueued;

    enum Attacks
    {
        noAttack,
        punch1,
        punch2,
        punch3,
        punch4,
        swingSword1,
        swingSword2,
        swingSword3,
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        inputScript = GetComponent<PlayerInput>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        lastAttack = Attacks.noAttack;
        punchTimer = 0;
        punchQueued = false;
        armedTimer = 0;
        swordSwingTimer = 0;
        swordSwingQueued = false;
    }

    void Update()
    {
        if (inputScript.inputAction1)
        {
            Attack();
        }

        if (inputScript.inputAction2)
        {
            // Swap weapons
            if (!anim.GetBool("Armed") && !IsAttacking())
            {
                lastAttack = Attacks.noAttack;

                anim.SetTrigger("DrawSword");

                StartCoroutine(DrawSword());
            }
            else
            {
                lastAttack = Attacks.noAttack;

                anim.SetTrigger("SheathSword");

                StartCoroutine(SheathSword());
            }
        }
        
        UpdateTimers();

        CheckTimers();
    }

    private void UpdateTimers()
    {
        if (anim.GetBool("Punching"))
        {
            punchTimer += Time.deltaTime;
        }

        if (anim.GetBool("Armed"))
        {
            armedTimer += Time.deltaTime;
        }

        if (anim.GetBool("SwingingSword"))
        {
            swordSwingTimer += Time.deltaTime;
        }
    }

    private void CheckTimers()
    {
        if (punchTimer >= punchChainWindowEnd)
        {
            anim.SetBool("Punching", false);
            punchTimer = 0;
        }

        if (swordSwingTimer >= swordSwingChainWindowEnd)
        {
            anim.SetBool("SwingingSword", false);
            swordSwingTimer = 0;
            lastAttack = Attacks.noAttack;
        }
    }

    private bool IsAttacking()
    {
        return !anim.GetCurrentAnimatorStateInfo(3).IsName("Passive");
    }

    private void Attack()
    {
        // Punch
        if (!anim.GetBool("Armed"))
        {
            if (!anim.GetBool("Punching"))
            {
                anim.SetBool("Punching", true);
                Punch();
            }

            else if (anim.GetBool("Punching") && !punchQueued &&
                     lastAttack != Attacks.noAttack &&
                     punchTimer >= punchChainWindowStart &&
                     punchTimer < punchChainWindowEnd)
            {
                punchQueued = true;
                Punch();
            }
        }

        // Swing Sword
        else
        {
            if (!anim.GetBool("SwingingSword"))
            {
                anim.SetBool("SwingingSword", true);
                SwingSword();
            }

            else if (anim.GetBool("SwingingSword") && !swordSwingQueued &&
                     swordSwingTimer >= swordSwingChainWindowStart &&
                     swordSwingTimer < swordSwingChainWindowEnd)
            {
                swordSwingQueued = true;
                SwingSword();
            }
        }
    }

    private void Punch()
    {
        switch (lastAttack)
        {
            case Attacks.noAttack:
                anim.SetTrigger("Punch1");
                lastAttack = Attacks.punch1;
                break;
            case Attacks.punch1:
                anim.SetTrigger("Punch2");
                lastAttack = Attacks.punch2;
                break;
            case Attacks.punch2:
                anim.SetTrigger("Punch3");
                lastAttack = Attacks.punch3;
                break;
            case Attacks.punch3:
                anim.SetTrigger("Punch4");
                lastAttack = Attacks.punch4;
                break;
            case Attacks.punch4:
                anim.SetTrigger("Punch1");
                lastAttack = Attacks.punch1;
                break;
        }
    }

    private void SwingSword()
    {
        switch (lastAttack)
        {
            case Attacks.noAttack:
                anim.SetTrigger("SwordSwing1");
                lastAttack = Attacks.swingSword1;
                break;
            case Attacks.swingSword1:
                anim.SetTrigger("SwordSwing2");
                lastAttack = Attacks.swingSword2;
                break;
            case Attacks.swingSword2:
                anim.SetTrigger("SwordSwing3");
                lastAttack = Attacks.swingSword3;
                break;
        }
    }

    private IEnumerator DrawSword()
    {
        anim.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(0.25f);

        armedSword.SetActive(true);
        sheathedSword.SetActive(false);
        anim.SetBool("Armed", true);
    }

    private IEnumerator SheathSword()
    {
        anim.SetLayerWeight(1, 0);

        yield return new WaitForSeconds(0.5f);

        sheathedSword.SetActive(true);
        armedSword.SetActive(false);
        anim.SetBool("Armed", false);
    }

    private void StartedPunch()
    {
        punchQueued = false;
        punchTimer = 0;
    }

    private void StartedSwordSwing()
    {
        swordSwingQueued = false;
        swordSwingTimer = 0;

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
