using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext,playerStateFactory){ }// pass the concrete states argumentes direct to base state constructer

   
    public override void EnterState()
    {
        //Debug.Log("HI FROM Walk");
        _ctx.CurrentPlayerSpeed = _ctx.BasePlayerSpeed;
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
        if (_ctx.Move != Vector3.zero && _ctx.IsSprinting )
        {
            SwitchState(_factory.Run());
        }
        else if(_ctx.Move == Vector3.zero) { SwitchState(_factory.Idle()); }
    }
}
