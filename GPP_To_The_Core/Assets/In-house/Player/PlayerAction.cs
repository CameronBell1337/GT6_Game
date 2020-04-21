using System.Collections;
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
    public float swordAutoSheathTime;
    public float swordSwingChainWindowStart;
    public float swordSwingChainWindowEnd;
    public LayerMask enemyLayers;
    [HideInInspector] public bool canAttack;

    [SerializeField] private GameObject sheathedSword;
    [SerializeField] private GameObject armedSword;
    private Animator anim;
    private CapsuleCollider col;
    private PlayerInput inputScript;
    private PlayerStats stats;
    private Collider swordHitArea;
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
        canAttack = true;

        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        inputScript = GetComponent<PlayerInput>();
        stats = GetComponent<PlayerStats>();
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
        if (inputScript.inputAction1 && canAttack)
        {
            Attack();
        }

        if (inputScript.inputAction2 && !anim.GetBool("SwappingWeapon") && stats.hasSword)
        {
            // Swap weapons
            lastAttack = Attacks.noAttack;
            anim.SetBool("SwappingWeapon", true);

            if (!anim.GetBool("Armed"))
            {
                anim.SetTrigger("DrawSword");
            }
            else
            {
                anim.SetTrigger("SheathSword");
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
            lastAttack = Attacks.noAttack;
            swordSwingTimer = 0;
        }

        if (armedTimer >= swordAutoSheathTime)
        {
            lastAttack = Attacks.noAttack;
            anim.SetBool("SwappingWeapon", true);

            anim.SetTrigger("SheathSword");
            armedTimer = 0;
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
        armedTimer = 0;

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

    private void WeaponSwitch()
    {
        if (!anim.GetBool("Armed"))
        {
            // Draw sword
            armedSword.SetActive(true);
            sheathedSword.SetActive(false);

            anim.SetLayerWeight(1, 0.5f);

            anim.SetBool("Armed", true);
        }
        else
        {
            // Sheath sword
            sheathedSword.SetActive(true);
            armedSword.SetActive(false);

            anim.SetLayerWeight(1, 0);

            anim.SetBool("Armed", false);
        }
    }

    private void DoneSwappingWeapon() 
    {
        anim.SetBool("SwappingWeapon", false);
        armedTimer = 0;
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

    private void PunchHit()
    {
        // Check to deal damage
        Vector3 origin = transform.position + Vector3.up * col.height * 0.65f;
        if (Physics.SphereCast(origin, col.radius * 0.9f, transform.forward, out RaycastHit hit, punchReach, enemyLayers))
        {
            RaycastHit[] allHits = Physics.SphereCastAll(origin, col.radius * 0.9f, transform.forward, punchReach, enemyLayers);
            Debug.Log("Hit");
            foreach (RaycastHit eachHit in allHits)
            {
                eachHit.transform.GetComponent<EnemyAttackHandler>().DealDamage(punchDamage);
            }
        }
    }

    private void SwordHit()
    {
        
    }
}
