using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerOnAirState : PlayerBaseState
{
    public PlayerOnAirState(PlayerStateMachine currentContext, PlayerStateFactory factory) : base(currentContext, factory) { }

    private IEnumerator ResetJumpCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        ResetJump();
    }

    public override void CheckSwitchState()
    {
        
        if(_ctx.IsGrounded)
        {
            SwitchState(_factory.Ground());
        }
    }

    public override void EnterState()
    {
        Jump();    
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
       
        CheckSwitchState();
    }

    void Jump()
    {
        _ctx.RigidBody.velocity = new Vector3(_ctx.RigidBody.velocity.x, 0f, _ctx.RigidBody.velocity.z);

        _ctx.RigidBody.AddForce(_ctx.RigidBody.transform.up * _ctx.JumpForce, ForceMode.Impulse);

        _ctx.CanJump = false;

        _ctx.ResetJump = _ctx.StartCoroutine(ResetJumpCooldown(_ctx.JumpCooldown));

      
    }

    private void ResetJump()
    {
        //reset Jump cooldown
        _ctx.CanJump = true;
    }
}
