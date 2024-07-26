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
        _ctx.CurrentPlayerSpeed = _ctx.CrouchPlayerSpeed;      
        _ctx.Controller.height = _ctx.CrouchHeight;
    }

    public override void ExitState()
    {
        _ctx.Controller.height = _ctx.NormalHeight;
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
