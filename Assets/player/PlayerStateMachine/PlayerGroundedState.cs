using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerGroundedState : PlayerBaseState
{

    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) { }
    public override void EnterState()
    {

        _ctx.PlayerVelocityY = 0f;
        //if (_ctx.PlayerVelocityY < 0) { _ctx.PlayerVelocityY = 0f; }
        // _isPlayerGrounded = _controller.isGrounded;

        //  _ctx.PlayerVelocityY += _ctx.GravityValue * Time.deltaTime;
        // _ctx.Controller.Move(_ctx.PlayerVelocity * Time.deltaTime);
        Debug.Log("HI FROM GROUNDED");
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
        if (_ctx.PlayerVelocityY < 0) { _ctx.PlayerVelocityY = 0f; }
       
        Debug.Log(_ctx.Controller.isGrounded);
        // _ctx.Controller.Move(Vector3.zero);
        //if (_ctx.PlayerVelocityY < 0) { _ctx.PlayerVelocityY = 0f; }
        /* if (_ctx.PlayerVelocityY < 0)
         {
             _ctx.PlayerVelocityY = 0f;
         }*/
        // _ctx.Controller.Move(Vector3.zero);
        /*_ctx.IsPlayerGrounded = _ctx.Controller.isGrounded;
        if (_ctx.IsPlayerGrounded && _ctx.PlayerVelocityY < 0)
        {
            _ctx.PlayerVelocityY = 0f;
        }*/

    }
    public override void CheckSwitchStates()
    {
        if (_ctx.IsJumping) { SwitchState(_factory.Jump()); }
    }

}
