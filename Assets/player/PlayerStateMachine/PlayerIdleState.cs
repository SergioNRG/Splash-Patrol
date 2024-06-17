using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    Vector3 forward;

    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) { }
 

    public override void EnterState()
    {
        Debug.Log("HI FROM IDLE");
        _ctx.MoveX = 0;
        _ctx.MoveZ = 0;
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
        forward = _ctx.CamTransform.forward;
        forward.y = 0f;
        forward = forward.normalized;
        _ctx.PlayerTransform.rotation = Quaternion.LookRotation(forward);
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Move != Vector3.zero && _ctx.IsSprinting && !_ctx.IsCrouching)
        {
            SwitchState(_factory.Run());
        }
        else if (_ctx.IsCrouching && !_ctx.IsSprinting)
        {
            SwitchState(_factory.Crouch());
        }
        else if(_ctx.Move != Vector3.zero && !_ctx.IsCrouching){ SwitchState(_factory.Walk());}
    }

}
