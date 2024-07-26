using UnityEngine;


public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext,playerStateFactory){ }// pass the concrete states argumentes direct to base state constructer

   
    public override void EnterState()
    {
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
        if (_ctx.Move != Vector3.zero && _ctx.IsSprinting && !_ctx.IsCrouching && !_ctx.IsJumping)
        {
            SwitchState(_factory.Run());
        }
        else if(_ctx.Move == Vector3.zero) { SwitchState(_factory.Idle()); }
    }
}
