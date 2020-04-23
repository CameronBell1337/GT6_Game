using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpider : Enemy
{
    public Collider patrolArea;
    public float sightRadius = 0;
    public float jumpForce = 0;
    public float movementDelay = 0;
    public float attackDelay = 1;
    
    GameObject player;
    Rigidbody rigidbody;

    Vector3 movementPosition = Vector3.zero;
    bool playerInRange = false;
    float movementTimer = 0;
    float attackTimer = 0;
    public bool jumping = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        movementPosition = randomMovePosition();
    }

    private void Update()
    {
        playerInRange = Vector3.Distance(player.transform.position, transform.position) < sightRadius;

        if (playerInRange)
        {
            Vector3 rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position, transform.up), rotateSpeed * Time.deltaTime).eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            Vector3 velocity = (player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
            velocity.y = rigidbody.velocity.y;

            if (!jumping && Vector3.Distance(player.transform.position, transform.position) < 4)
            {
                jumping = true;
                velocity = (player.transform.position - transform.position).normalized * moveSpeed * 2 * Time.deltaTime;
                velocity.y = jumpForce;
            }

            rigidbody.velocity = velocity;
        }
        else
        {
            Vector3 rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementPosition - transform.position, transform.up), rotateSpeed * Time.deltaTime).eulerAngles;
            Vector3 velocity = (movementPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
            velocity.y = rigidbody.velocity.y;

            if (Vector3.Distance(transform.position, movementPosition) < 1)
            {
                rotation = transform.rotation.eulerAngles;
                velocity = Vector3.zero;
                movementTimer += Time.deltaTime;

                if (movementTimer >= movementDelay)
                {
                    movementTimer = 0;
                    movementPosition = randomMovePosition();
                }
            }

            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            rigidbody.velocity = velocity;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.GetComponent<PlayerCoreFunctions>().TakeDamage(transform, attackDamage);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            jumping = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            attackTimer += Time.deltaTime;

            if (attackTimer > attackDelay)
            {
                attackTimer = 0;
                other.GetComponent<PlayerCoreFunctions>().TakeDamage(transform, attackDamage);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            jumping = false;
        }
    }

    Vector3 randomMovePosition()
    {
        float x_pos = Random.Range(patrolArea.bounds.min.x, patrolArea.bounds.max.x);
        float z_pos = Random.Range(patrolArea.bounds.min.z, patrolArea.bounds.max.z);
        return new Vector3(x_pos, transform.position.y, z_pos);
    }
}
