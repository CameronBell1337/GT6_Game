using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float baseWalkAnimSpeed;
    public float baseMovementSpeed;
    public float runSpeedMultiplier;
    public float runAnimSpeedMultiplier;
    public float jumpUpForce;
    public float jumpForwardForce;
    public float jumpForwardRunningForce;
    public LayerMask groundLayers;
    public float airGlideMultiplier;
    public float maxAirGlideSpeed;
    public float maxRunningJumpAirGlideSpeed;
    public float gravity;
    public float groundCheckRadius;
    public float wallCheckRadius;
    [Range(0.0f, 1.0f)] public float wallCheckHeightPerc;
    [HideInInspector] public bool hasLeftEdgeYet;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isRunningJump;
    [HideInInspector] public float movementSpeedMultiplier;
    [HideInInspector] public bool animationOverride;

    private PlayerInput inputScript;
    private Animator anim;
    private Rigidbody rb;
    private CapsuleCollider col;
    private Vector3 lastLookDirection;
    private ParticleSystem runningDustPS;
    private ParticleSystem landingPS;
    private ParticleSystem jumpingPS;

    void Start()
    {
        hasLeftEdgeYet = false;
        isJumping = false;
        isRunningJump = false;
        movementSpeedMultiplier = 1;
        animationOverride = false;
        inputScript = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        lastLookDirection = Vector3.zero;
        runningDustPS = transform.Find("Running Dust PS").GetComponent<ParticleSystem>();
        landingPS = transform.Find("Landing PS").GetComponent<ParticleSystem>();
        jumpingPS = transform.Find("Jumping PS").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        RotatePlayer();

        if (!animationOverride)
        {
            AnimatePlayer();
        }
        
        JumpCheck();
        FallCheck();
        ParticleEffectsCheck();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float movementSpeed;
        movementSpeed = (baseMovementSpeed + (inputScript.inputRunSpeed * runSpeedMultiplier)) *
                         movementSpeedMultiplier;
        if (!anim.GetBool("Falling"))
        {
            // Move normally if not in air
            rb.MovePosition(transform.position + (inputScript.inputMove.normalized * movementSpeed *
                                Time.fixedDeltaTime));
        }
        else if (!isJumping)
        {
            // If falling and not jumping:
            // Add initial force to match origial movement force
            if (hasLeftEdgeYet)
            {
                rb.AddForce(inputScript.inputMove.normalized * movementSpeed, ForceMode.Impulse);
                hasLeftEdgeYet = false;
            }
            // Then airglide
            AirGlide();
        }
        else
        {
            AirGlide();
        }
    }

    private void RotatePlayer()
    {
        if (inputScript.inputMove != Vector3.zero)
        {
            lastLookDirection = inputScript.inputMove;
        }
        transform.rotation = Quaternion.LookRotation(lastLookDirection);
    }

    private void AnimatePlayer()
    {
        // Set Idle/Run animation states
        float animMovementSpeed = (baseWalkAnimSpeed * Vector3.Distance(transform.position,
                                   transform.position + inputScript.inputMove.normalized)) +
                                   (inputScript.inputRunSpeed * runAnimSpeedMultiplier);
        if (inputScript.inputMove != Vector3.zero)
        {
            anim.SetBool("Running", true);
            anim.SetFloat("RunSpeed", animMovementSpeed);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    private void JumpCheck()
    {
        if (inputScript.inputJump && IsGrounded())
        {
            if (IsGrounded() && IsTouchingNOnSides())
            {
                GetComponent<Collider>().material.dynamicFriction = 0.0f;
                GetComponent<Collider>().material.staticFriction = 0.0f;
                GetComponent<Collider>().material.frictionCombine = PhysicMaterialCombine.Minimum;
            }

            rb.velocity = Vector3.zero;

            if (inputScript.inputRunSpeed > 0)
            {
                rb.AddForce((inputScript.inputMove.normalized * jumpForwardRunningForce *
                             movementSpeedMultiplier) + (Vector3.up * jumpUpForce), ForceMode.Impulse);

                isRunningJump = true;
            }
            else
            {
                rb.AddForce((inputScript.inputMove.normalized * jumpForwardForce *
                             movementSpeedMultiplier) + (Vector3.up * jumpUpForce), ForceMode.Impulse);

                isRunningJump = false;
            }

            anim.SetBool("Falling", true);
            anim.SetTrigger("Jump");
            anim.SetLayerWeight(1, 1);
            isJumping = true;
            jumpingPS.Play();
        }
        // Ending falling animation
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("Fall") && IsGrounded() ||
                 anim.GetCurrentAnimatorStateInfo(1).IsName("Jump") && IsGrounded() ||
                 anim.GetCurrentAnimatorStateInfo(1).IsName("Jump Flip") && IsGrounded())
        {
            anim.SetBool("Falling", false);
            anim.SetLayerWeight(1, 0.5f);
            rb.velocity = Vector3.zero;
            isJumping = false;
        }
    }

    public void JumpEnd()
    {
        anim.SetTrigger("BeginFall");
    }

    public void Land()
    {
        anim.SetTrigger("Land");

        GetComponent<Collider>().material.dynamicFriction = 0.6f;
        GetComponent<Collider>().material.staticFriction = 0.6f;
        GetComponent<Collider>().material.frictionCombine = PhysicMaterialCombine.Average;

        if (!landingPS.isPlaying)
        {
            landingPS.Play();
        }
    }

    private void FallCheck()
    {
        if (!IsGrounded() && !anim.GetBool("Falling"))
        {
            anim.SetTrigger("Drop");
            anim.SetBool("Falling", true);
            anim.SetLayerWeight(1, 1);
            hasLeftEdgeYet = true;
            isRunningJump = false;
        }
    }

    private void ParticleEffectsCheck()
    {
        if (anim.GetBool("Running") && inputScript.inputRunSpeed > 0 &&
            anim.GetCurrentAnimatorStateInfo(1).IsName("Grounded"))
        {
            if (!runningDustPS.isPlaying)
            {
                runningDustPS.Play();
            }
        }
        else
        {
            if (runningDustPS.isPlaying)
            {
                runningDustPS.Stop();
            }
        }
    }

    private void AirGlide()
    {
        // Air glide if in air
        rb.AddForce(inputScript.inputMove.normalized * airGlideMultiplier * movementSpeedMultiplier *
                    Time.fixedDeltaTime);
        // Clamp max air glide speed
        float xzMag = Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.z * rb.velocity.z));
        if (isRunningJump)
        {
            if (xzMag > maxRunningJumpAirGlideSpeed * movementSpeedMultiplier)
            {
                Vector3 normalisedXZ = new Vector3(rb.velocity.x / xzMag,
                                                   rb.velocity.y,
                                                   rb.velocity.z / xzMag);
                Vector3 velocityAtMaxAirGlideSpeed = new Vector3(normalisedXZ.x * maxRunningJumpAirGlideSpeed *
                                                                 movementSpeedMultiplier,
                                                                 rb.velocity.y,
                                                                 normalisedXZ.z * maxRunningJumpAirGlideSpeed *
                                                                 movementSpeedMultiplier);
                rb.velocity = velocityAtMaxAirGlideSpeed;
            }
        }
        else
        {
            if (xzMag > maxAirGlideSpeed * movementSpeedMultiplier)
            {
                Vector3 normalisedXZ = new Vector3(rb.velocity.x / xzMag,
                                                   rb.velocity.y,
                                                   rb.velocity.z / xzMag);
                Vector3 velocityAtMaxAirGlideSpeed = new Vector3(normalisedXZ.x *
                                                                 maxAirGlideSpeed * movementSpeedMultiplier,
                                                                 rb.velocity.y,
                                                                 normalisedXZ.z *
                                                                 maxAirGlideSpeed * movementSpeedMultiplier);
                rb.velocity = velocityAtMaxAirGlideSpeed;
            }
        }
        // Increase gravity for video game feel
        rb.AddForce(new Vector3(0, -gravity, 0) * Time.fixedDeltaTime);
    }

    public bool IsGrounded()
    {
        Vector3 checkExtents = new Vector3(col.radius * groundCheckRadius, 0.05f, col.radius * groundCheckRadius);
        Quaternion zeroAngle = new Quaternion();
        return Physics.CheckBox(transform.position, checkExtents, zeroAngle, groundLayers);
    }

    public bool IsTouchingNOnSides()
    {
        Vector3 checkHalfExtents = new Vector3(col.radius * wallCheckRadius,
                                               col.height * wallCheckHeightPerc,
                                               col.radius * wallCheckRadius);
        Quaternion zeroAngle = new Quaternion();
        // Check for a box, positioning from top->down of body,
        //  that scales in height to the size of the player and the check height percentage variable
        return Physics.CheckBox(transform.position + (Vector3.up * ((col.height) -
                                                      (col.height * wallCheckHeightPerc * 0.5f))),
                                                      checkHalfExtents, zeroAngle, groundLayers);
    }

    public void KnockBack(Transform _enemy)
    {
        Vector3 hitDir = (transform.position - _enemy.position).normalized;
        hitDir.y = 0;

        Vector3 pushDir = transform.up * jumpUpForce * 0.40f + _enemy.forward * jumpForwardForce * 1.3f;

        rb.AddForce(pushDir, ForceMode.Impulse);
    }
}
