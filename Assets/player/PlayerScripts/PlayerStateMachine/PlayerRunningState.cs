using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public PlayerRunningState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
 

    public override void EnterState()
    {
        _ctx.CurrentPlayerSpeed = _ctx.SprintPlayerSpeed;
    }

    public override void ExitState()
    {
      
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.MoveAndRotationRelativeToCamera();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Move == Vector3.zero)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.Move != Vector3.zero && !_ctx.IsSprinting)
        {
            SwitchState(_factory.Walk());
        }
    }
}
