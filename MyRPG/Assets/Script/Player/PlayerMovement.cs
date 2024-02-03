using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler OnWalking;
    public event EventHandler OnIdle;
    public event EventHandler OnAir;
    public event EventHandler OnSprinting;
    public event EventHandler OnJumping;

    [Header("Refrences")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform cameraOrientation;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float playerHeight;
    [SerializeField] private float groundDrag;

    [Space(10)]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;

    [Space(10)]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;

    #region Setter Getter

    public float InputMagnitude { get {return inputMagnitude; } }

    #endregion


    private float horizontalInput;
    private float verticalInput;
    private float inputMagnitude;

    private bool isGrounded;
    private bool canJump = true;
    private bool canDash = true;

    private Rigidbody rb;
    private Vector3 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.freezeRotation = true;

        gameInput.OnJumpPressed += GameInput_OnJumpPressed;
        gameInput.OnDashPressed += GameInput_OnDashPressed;
    }

    private void GameInput_OnDashPressed(object sender, System.EventArgs e)
    {
        Dash();

        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void GameInput_OnJumpPressed(object sender, System.EventArgs e)
    {

        Jump();

        Invoke(nameof(ResetJump), jumpCooldown);

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = gameInput.GetHorizontalNormalized();
        verticalInput = gameInput.GetVerticalNormalized();

        if (CheckIfIdle())
        {
            OnIdle?.Invoke(this, EventArgs.Empty);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(); // move player
        GroundCheck(); // Check if player touch the ground
        SpeedControl(); // Control player speed

    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDir = cameraOrientation.forward * verticalInput + cameraOrientation.right * horizontalInput;

        if (isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);

            inputMagnitude = Mathf.Clamp01(moveDir.magnitude /2);

            OnWalking?.Invoke(this, EventArgs.Empty);

            if (gameInput.GetSprintPressed())
            {
                rb.AddForce(moveDir.normalized * sprintSpeed * 10f, ForceMode.Force);

                inputMagnitude *= 2;

                OnSprinting?.Invoke(this, EventArgs.Empty);
            }

        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void OnDrawGizmos()
    {
        //DrawGizmos for ground
        Gizmos.color = isGrounded ? Color.green : Color.red; // if grounded color = green if not color = red
        Vector3 rayStart = transform.position;
        Vector3 rayDirection = Vector3.down;
        float rayLength = playerHeight * 0.5f ;

        Gizmos.DrawRay(rayStart, rayDirection * rayLength);
        

        //Draw
    }

    private void GroundCheck()
    {
        //check ground with shooting raycast below player
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
            OnAir?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity 
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Check if player can jump and touching ground 
        if (isGrounded && canJump)
        {
            OnJumping?.Invoke(this, EventArgs.Empty);

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            canJump = false;

        }
    }

    private void ResetJump()
    {
        //reset Jump cooldown
        canJump = true;
    }

    private void Dash()
    {
        if (canDash)
        {
            //check if player moving or not
            if (gameInput.GetVectorNormalized() == Vector2.zero)
            {
                rb.AddForce(transform.right * dashForce, ForceMode.Impulse);

                canDash = false;
            }
            else
            {
                rb.AddForce(moveDir * dashForce, ForceMode.Impulse);
                canDash = false;
            }
        }
    }

    private void ResetDash()
    {
        //reset Dash
        canDash = true;
    }

    private bool CheckIfIdle()
    {
        //Check if therers no movement
        if (rb.velocity == Vector3.zero)
        {
            inputMagnitude = Mathf.Clamp01(0);
            return true;
        }
        else
        {
            return false;
        }

    }

   
   
}