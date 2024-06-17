using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) { }
 

    public override void EnterState()
    {
        Debug.Log("HELLO FROM IDLE");
        //_ctx.MoveX = 0;
       // _ctx.MoveZ = 0;
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
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Move != Vector3.zero && _ctx.IsSprinting)
        {
            SwitchState(_factory.Run());
        }
        else if(_ctx.Move != Vector3.zero){ SwitchState(_factory.Walk());}
    }

}
