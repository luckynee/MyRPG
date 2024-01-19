using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    private PlayerControlAction playerControlAction;

    public event EventHandler OnJumpPressed;
    public event EventHandler OnDashPressed;

    private void Awake()
    {
        playerControlAction = new PlayerControlAction();

        playerControlAction.Enable();

        playerControlAction.Player.Jump.performed += Jump_performed;
        playerControlAction.Player.Dash.performed += Dash_performed;
    }

    private void Dash_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDashPressed?.Invoke(this, EventArgs.Empty);   
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetVectorNormalized()
    {
        Vector2 inputVector = playerControlAction.Player.Move.ReadValue<Vector2>();

        return inputVector;
    }

    public float GetHorizontalNormalized()
    {
        return GetVectorNormalized().x;
    }

    public float GetVerticalNormalized()
    {
        return GetVectorNormalized().y;
    }
}
