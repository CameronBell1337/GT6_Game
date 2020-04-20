using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    //Enemy scripts that handle damage
    //private SlimeStats slimeStats;

    private void Start()
    {
        //Set up enemy scripts that handle damage
        //slimeStats = GetComponent<SlimeStats>();
    }

    public void DealDamage(float _damage)
    {
        switch(transform.tag)
        {
            case "Slime":
                {
                    //slimeStats.TakeDamage(_damage);
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isSwingingSword = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetBool("SwingingSword");

        if (other.name == "2Hand-Sword-InHand" && isSwingingSword)
        {
            float swordDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>().swordDamage;

            switch (transform.tag)
            {
                case "Slime":
                    {
                        //slimeStats.TakeDamage(swordDamage);
                        break;
                    }
            }
        }
    }
}
