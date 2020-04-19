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

    public void DealDamage(Transform _enemy, float _damage)
    {
        switch(_enemy.tag)
        {
            case "Slime":
                {
                    //slimeStats.TakeDamage(_damage);
                    break;
                }
        }
    }
}
