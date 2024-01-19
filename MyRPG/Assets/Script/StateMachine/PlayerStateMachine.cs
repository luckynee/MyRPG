using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform cameraOrientation;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Attributes")]
    //move attributes
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float playerHeight;
    [SerializeField] private float groundDrag;

    [Space(10)]
    //jump attributes
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;

    [Space(10)]
    //dash attributes
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;

    //State Variable
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //getter and seetter
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody RigidBody { get { return rb; } }
    public Coroutine ResetJump { set { resetJump = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsGrounded { get { return isGrounded; } }
    public bool CanJump { get { return canJump; } set { canJump = value; } }
    public float JumpCooldown {  get { return jumpCooldown; } }
    public float JumpForce { get { return jumpForce; } }
    public float AirMultiplier { get { return airMultiplier; } }
    public float GroundDrag { get { return groundDrag; } }


    private float horizontalInput;
    private float verticalInput;

    private bool isGrounded;
    private bool canJump = true;
    private bool canDash = true;
    private bool _isJumpPressed;

    private Coroutine resetJump;
    private Rigidbody rb;
    private Vector3 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //setup State
        _states = new PlayerStateFactory(this);
        _currentState = _states.Ground();
        _currentState.EnterState();
    }

    private void Start()
    {
        rb.freezeRotation = true;

        gameInput.OnJumpPressed += GameInput_OnJumpPressed;
        gameInput.OnDashPressed += GameInput_OnDashPressed;
    }

    private void GameInput_OnDashPressed(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void GameInput_OnJumpPressed(object sender, System.EventArgs e)
    {
        _currentState = _states.OnAir();
        _currentState.EnterState();

    }

    private void Update()
    {
        horizontalInput = gameInput.GetHorizontalNormalized();
        verticalInput = gameInput.GetVerticalNormalized();

        SpeedControl();
        GroundCheck();

        Debug.Log(canJump);

        _currentState.UpdateState();
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
        }
    }
}
    