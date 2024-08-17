using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
   : base(currentContext, playerStateFactory)
    {
       // _isRootState = true;
       // InitializeSubState();
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

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.MoveAndRotationRelativeToCamera();
    }

    public override void CheckSwitchStates()
    {

        if (_ctx.Move == Vector3.zero && !_ctx.IsCrouching)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.Move != Vector3.zero && !_ctx.IsSprinting && !_ctx.IsCrouching)
        {
            SwitchState(_factory.Walk());
        }
        else if (_ctx.Move != Vector3.zero && _ctx.IsSprinting && !_ctx.IsCrouching && !_ctx.IsJumping)
        {
            SwitchState(_factory.Run());
        }

    }
}
