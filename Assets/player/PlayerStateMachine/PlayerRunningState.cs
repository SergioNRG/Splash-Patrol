using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public PlayerRunningState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
 

    public override void EnterState()
    {
        Debug.Log("HELLO FROM Run");
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
        Debug.Log(_ctx.CurrentPlayerSpeed);
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Move == Vector3.zero)
        {
            SwitchState(_factory.Idle());
        }else if (_ctx.Move != Vector3.zero && !_ctx.IsSprinting)
        {
            SwitchState(_factory.Walk());
        }
    }
   /* private void MoveAndRotationRelativeToCamera()
    {
        Vector3 forward = _ctx.CamTransform.forward;
        Vector3 right = _ctx.CamTransform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = _ctx.Move.z * forward;
        Vector3 rightRelativeHorizontalInput = _ctx.Move.x * right;


        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;

        _ctx.Controller.Move(cameraRelativeMovement * Time.deltaTime * _ctx.CurrentPlayerSpeed);
        _ctx.PlayerTransform.rotation = Quaternion.LookRotation(forward);
    }*/

}
