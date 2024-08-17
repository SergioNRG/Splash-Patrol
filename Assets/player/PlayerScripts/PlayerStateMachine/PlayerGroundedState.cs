using UnityEngine;


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
        _ctx.PlayerVelocityY = -5f;        
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
        }else if (_ctx.IsCrouching && !_ctx.IsJumping) { SetSubState(_factory.Crouch()); }
       
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        if (_ctx.PlayerVelocityY < 0) { _ctx.PlayerVelocityY = -5f; }
    }
    public override void CheckSwitchStates()
    {
        if (_ctx.IsJumping && !_ctx.IsCrouching) { SwitchState(_factory.Jump()); }
       /* if (_ctx.IsCrouching && !_ctx.IsJumping)
        {
            SwitchState(_factory.Crouch());
        }*/
    }
}
