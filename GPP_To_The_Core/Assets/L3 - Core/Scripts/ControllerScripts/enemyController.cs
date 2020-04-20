using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    [Range(0.0f, 30.0f)]
    public float followRadius = 10.0f;

    public float smoothDamp = 5.0f;
    public float gravity = -38.0f;

    float velocityY;
    Transform followTarget;
    NavMeshAgent agent;

    void Start()
    {
        followTarget = playerManager.instance.playerCharacter.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(followTarget.position, transform.position);
        velocityY += Time.deltaTime * gravity;

        if (distance <= followRadius)
        {
            agent.SetDestination(followTarget.position);

            if(distance <= agent.stoppingDistance)
            {
                LookAtTarget();
            }
        }
    }

    void LookAtTarget()
    {
        Vector3 direction = (followTarget.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * smoothDamp);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
