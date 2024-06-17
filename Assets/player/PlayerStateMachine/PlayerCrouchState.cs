using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
   : base(currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("HI FROM CROUCH");
        _ctx.CurrentPlayerSpeed = _ctx.CrouchPlayerSpeed;      
        _ctx.Controller.height = _ctx.CrouchHeight;
       // _ctx.Controller.center = new Vector3(0, _ctx.CrouchCenter, 0);
    }

    public override void ExitState()
    {
        _ctx.Controller.height = _ctx.NormalHeight;
        //_ctx.Controller.center = new Vector3(0, _ctx.NormalCenter, 0);
        _ctx.CurrentPlayerSpeed = _ctx.BasePlayerSpeed;
    }

    public override void InitializeSubState()
    {
        if (_ctx.Move == Vector3.zero && !_ctx.IsSprinting)
        {
            SetSubState(_factory.Idle());
        }
        else if (_ctx.Move != Vector3.zero && !_ctx.IsSprinting )
        {
            SetSubState(_factory.Walk());
        }
        else if (_ctx.Move != Vector3.zero && _ctx.IsSprinting )
        {
            SetSubState(_factory.Run());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();  
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Controller.isGrounded && !_ctx.IsCrouching)
        {
            SwitchState(_factory.Grounded());
        }

    }
}
