using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float jumpForce;
    public LayerMask groundLayers;
    public float detectionRange;

    private SlimeStats stats;
    private Rigidbody rb;
    private Transform player;
    private float jumpTimer;
    private bool justLanded;
    private float jumpRateRandom;

    void Start()
    {
        stats = GetComponent<SlimeStats>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        jumpTimer = 0;
        justLanded = false;
        jumpRateRandom = Random.Range(stats.jumpRate - 1.5f, stats.jumpRate + 1.5f);
    }

    void Update()
    {
        // Reset jump timer on landing
        if (IsGrounded() && !justLanded)
        {
            justLanded = true;
            jumpTimer = 0;
            jumpRateRandom = Random.Range(stats.jumpRate - 1.5f, stats.jumpRate + 1.5f);

        }
        if (!IsGrounded())
        {
            justLanded = false;
        }

        jumpTimer += Time.deltaTime;

        if (jumpTimer >= jumpRateRandom)
        {
            jumpTimer = 0;

            Vector3 jumpDir = Vector3.zero;

            // Detect is player is in range
            if (Vector3.Distance(transform.position, player.position) <= detectionRange)
            {
                Vector3 playerDir = (player.position - transform.position).normalized;
                playerDir.y = 0;

                Quaternion lookAtRot = Quaternion.LookRotation(playerDir);
                transform.rotation = lookAtRot;

                jumpDir = transform.up * jumpForce + playerDir * jumpForce * 0.53f;
            }
            else
            {
                //Free roam in random directions
                transform.rotation = Quaternion.AngleAxis(Random.Range(0, 359), Vector3.up);

                jumpDir = transform.up * jumpForce + transform.forward * jumpForce * 0.53f;
            }

            // Jump in predetermined direction^
            rb.AddForce(jumpDir, ForceMode.Impulse);
        }
    }

    public bool IsGrounded()
    {
        Vector3 offset = new Vector3(0, 0.05f, 0);
        Vector3 checkOrigin = transform.position + offset;

        if (Physics.Raycast(checkOrigin, Vector3.down, 0.1f, groundLayers))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
