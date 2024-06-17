using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitializeSubState();
    } 


    public override void EnterState()
    {
        _ctx.PlayerVelocityY += Mathf.Sqrt(_ctx.JumpHeight * -3.0f * _ctx.GravityValue);
    }

    public override void ExitState()
    {
     
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Controller.isGrounded)
        {
            SwitchState(_factory.Grounded());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void InitializeSubState()
    {
        if (_ctx.Move == Vector3.zero && !_ctx.IsSprinting)
        {
            SetSubState(_factory.Idle());
        }
        else if (_ctx.Move != Vector3.zero && !_ctx.IsSprinting)
        {
            SetSubState(_factory.Walk());
        }
        else
        {
            SetSubState(_factory.Run());
        }
    }


}
