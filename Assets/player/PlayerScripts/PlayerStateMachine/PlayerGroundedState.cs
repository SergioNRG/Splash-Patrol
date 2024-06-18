using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerGroundedState : PlayerBaseState
{

    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) 
    { 
        _isRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        _ctx.PlayerVelocityY = 0f;
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        if (_ctx.Move == Vector3.zero && !_ctx.IsSprinting)
        {
            SetSubState(_factory.Idle());
        }else if (_ctx.Move != Vector3.zero && !_ctx.IsSprinting)
        {
            SetSubState(_factory.Walk());
        }     
        else if(_ctx.Move != Vector3.zero && _ctx.IsSprinting)
        {
            SetSubState(_factory.Run());
        }
       
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        if (_ctx.PlayerVelocityY < 0) { _ctx.PlayerVelocityY = 0f; }
       
    }
    public override void CheckSwitchStates()
    {
        if (_ctx.IsJumping) { SwitchState(_factory.Jump()); }
        if (_ctx.IsCrouching && !_ctx.IsJumping)
        {
            SwitchState(_factory.Crouch());
        }
    }
}
