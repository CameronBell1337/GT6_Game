using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineMovementController : MonoBehaviour
{
    public Animator anim;
    public float turnST = 0.14f;
    public int maxJumpCount = 2;
    public int currentJumpCount;
    public float speedSTime = 0.2f;
    float runSpeed = 9.0f;
    float walkSpeed = 5.0f;
    float velocityY;
    public float jumpForce = 5.5f;
    public float gravity = -38.0f;
    public float currentSpeed;
    public float horizontal;
    float duration = 0.5f;
    float characterSpeed;
    public float currentTime = 0.0f;
    float speedSVel;
    public bool doubleJumpEnabled;
    bool isDoubleJumping = false;

    public CharacterController col;
    public CapsuleCollider col2;

    Vector3 velocity;
    void Start()
    {
        anim = GetComponent<Animator>();
        doubleJumpEnabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        horizontal = 0.0f;
        col = GetComponent<CharacterController>();
        col2 = GetComponent<CapsuleCollider>();
    
    }

    void playerMovement()
    {
        velocityY += Time.deltaTime * gravity;
        horizontal = Input.GetAxisRaw("Horizontal");
        bool lStick = Input.GetKey("joystick button 8");
        bool shift = Input.GetKey("left shift");
        bool jumping = Input.GetKey("joystick button 0");
        bool rSticInUse = false;
        bool lStickInUse = false;


        anim.SetFloat("velY", velocityY, turnST, Time.deltaTime);
        anim.SetFloat("Hor", 0.0f, turnST, Time.deltaTime);
        anim.SetBool("isShiftPressed", (shift || lStick));

        if (horizontal >= 0)
        {
            anim.SetFloat("Vert", horizontal, turnST, Time.deltaTime);
            lStickInUse = true;

        }
        else if (horizontal <= 0)
        {
            anim.SetFloat("Vert", -horizontal, turnST, Time.deltaTime);
            lStickInUse = true;

        }

        Vector2 playerMov = new Vector2(horizontal, 0.0f);
        Vector2 playerDir = playerMov.normalized;

        characterSpeed = ((horizontal > 0 && (lStick || shift)) ? runSpeed : walkSpeed) * playerDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, characterSpeed, ref speedSVel, speedSTime);

        if (!col.isGrounded && velocityY < jumpForce)
        {

            anim.SetBool("isFalling", true);
            //anim.SetBool("isdoubleJumpTrue", false);
            //anim.SetBool("");

        }

        velocity = Vector3.up * velocityY;


        if (horizontal > 0 && !col.isGrounded)
        {
            velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        }

        if (!lStickInUse)
        {

            anim.SetBool("isUsingLStick", false);
        }
        else if (lStickInUse)
        {

            anim.SetBool("isUsingLStick", true);
        }

        col.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(col.velocity.x, col.velocity.z).magnitude;

        if (col.isGrounded)
        {
            velocityY = 0;
            anim.SetBool("isJumpPressed", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("isdoubleJumpTrue", false);
            isDoubleJumping = false;
            currentJumpCount = 0;
            currentTime = duration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        anim.SetInteger("jumpCounter", currentJumpCount);
        anim.SetBool("isDoubleJumping", isDoubleJumping);


        if (currentJumpCount == 2 && velocityY <= 10)
        {
            isDoubleJumping = false;
        }


        if (doubleJumpEnabled && maxJumpCount > currentJumpCount && Input.GetKeyDown("joystick button 0"))
        {
            jump();
        }
        else if (doubleJumpEnabled && maxJumpCount > currentJumpCount && Input.GetKeyDown("space"))
        {
            jump();
        }

        if (!doubleJumpEnabled && col.isGrounded && Input.GetKey("joystick button 0"))
        {
            jump();
        }
        else if (!doubleJumpEnabled && col.isGrounded && Input.GetKey("space"))
        {
            jump();
        }

        if (doubleJumpEnabled)
        {

            anim.SetBool("isdoubleJumpTrue", true);
        }
    }

    void jump()
    {
        if (velocityY > -30)
        {
            currentJumpCount++;
        }


        if (currentJumpCount <= maxJumpCount)
        {
            anim.SetBool("isJumpPressed", true);
            anim.SetBool("isJumping", true);
            if (currentJumpCount < maxJumpCount)
            {
                float jumpVel = Mathf.Sqrt(-2 * gravity * jumpForce);
                velocityY = 0;
                velocityY += jumpVel;
            }
            else if (currentJumpCount == 2)
            {
                isDoubleJumping = true;
                float jumpVel = Mathf.Sqrt(-2 * gravity * (jumpForce + 1));
                velocityY = 0;
                velocityY += jumpVel;
            }
        }
    }
}
