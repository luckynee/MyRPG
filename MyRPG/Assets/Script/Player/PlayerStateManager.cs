using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private enum PlayerState
    {
        Idle,
        Walking,
        Sprinting,
        OnAir,
        Dash,
    }

    private PlayerState playerState;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        playerMovement.OnWalking += PlayerMovement_OnWalking;
        playerMovement.OnIdle += PlayerMovement_OnIdle;
        playerMovement.OnAir += PlayerMovement_OnAir;
    }

    private void PlayerMovement_OnAir(object sender, System.EventArgs e)
    {
        playerState = PlayerState.OnAir;
    }

    private void PlayerMovement_OnIdle(object sender, System.EventArgs e)
    {
        playerState = PlayerState.Idle;
    }

    private void PlayerMovement_OnWalking(object sender, System.EventArgs e)
    {
        playerState = PlayerState.Walking;
    }

    private void Update()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Walking:
                break;
            case PlayerState.Sprinting:
                break;
            case PlayerState.OnAir:
                break;
            case PlayerState.Dash:
                break;
        }

        Debug.Log(playerState);
    }

    public bool IsIdle()
    {
        return playerState == PlayerState.Idle;
    }

    public bool IsWalking()
    {
        return playerState == PlayerState.Walking;
    }


}
