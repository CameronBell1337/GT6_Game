using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void DealDamage(float _damage)
    {
        enemy.TakeDamage(_damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isSwingingSword = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetBool("SwingingSword");

        if (other.name == "2Hand-Sword-InHand" && isSwingingSword)
        {
            float swordDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>().swordDamage;
            enemy.TakeDamage(swordDamage);
        }
    }
}
