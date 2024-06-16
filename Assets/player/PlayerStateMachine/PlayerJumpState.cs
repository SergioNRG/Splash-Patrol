using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) { } 


    public override void EnterState()
    {
        //Jump();
        Debug.Log(" oi from jump");
        _ctx.PlayerVelocityY += Mathf.Sqrt(_ctx.JumpHeight * -3.0f * _ctx.GravityValue);
    }

    public override void ExitState()
    {
     
    }
    public override void CheckSwitchStates()
    {
        if (_ctx.Controller.isGrounded)
        {
            Debug.Log("sair do jump entrar no groung");
            SwitchState(_factory.Grounded());
        }
    }

 

    public override void UpdateState()
    {
       // Debug.Log("hi from jump update");
        CheckSwitchStates();

    }

    public override void InitializeSubState()
    {

    }
    private void Jump()
    {
        //if (_isJumping && _isPlayerGrounded)
       // {
            _ctx.PlayerVelocityY += Mathf.Sqrt(_ctx.JumpHeight * -3.0f * _ctx.GravityValue);
       // }

        _ctx.PlayerVelocityY +=_ctx.GravityValue * Time.deltaTime;
        _ctx.Controller.Move(_ctx.PlayerVelocity * Time.deltaTime);

    }

}
