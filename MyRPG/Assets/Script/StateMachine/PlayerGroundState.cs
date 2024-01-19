using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory factory) 
    : base(currentContext, factory) { }

   

    public override void CheckSwitchState()
    {
      
    }

    public override void EnterState()
    {
        Debug.Log("Ground State");
    }

    public override void ExitState()
    {
        Debug.Log("Exit ground");
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
       
        CheckSwitchState();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
