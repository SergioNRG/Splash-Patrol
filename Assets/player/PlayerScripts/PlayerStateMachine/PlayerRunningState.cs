using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public PlayerRunningState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
 

    public override void EnterState()
    {
        _ctx.CurrentPlayerSpeed = _ctx.SprintPlayerSpeed;
        if (_ctx.RegenStaminaRoutine != null)
        {
            _ctx.StopCoroutine(_ctx.RegenStaminaRoutine);
            _ctx.RegenStaminaRoutine = null;
        }
       
    }

    public override void ExitState()
    {
        _ctx.RegenStaminaRoutine = _ctx.StartCoroutine(_ctx.RegenRoutine());
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.MoveAndRotationRelativeToCamera();
        _ctx.CurrentStamina -= _ctx.StaminaLoseAmount * Time.deltaTime;
        if (_ctx.CurrentStamina <= 0)
        {
            _ctx.CurrentStamina = 0;
            _ctx.CurrentPlayerSpeed = _ctx.BasePlayerSpeed;
        }
        _ctx.StaminaSlider.value = _ctx.CurrentStamina;
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Move == Vector3.zero)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.Move != Vector3.zero && !_ctx.IsSprinting)
        {
            SwitchState(_factory.Walk());
        }
    }
}
