using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public float attackDamage = 0;
    public float moveSpeed = 0;
    public float rotateSpeed = 0;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
