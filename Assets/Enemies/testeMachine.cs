using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class testeMachine// : MonoBehaviour
{
    public enum State
    {
        Idle,
        Attacking,
    }

    protected State _currentState;// = State.Idle;


    public abstract void OnTriggerEnter(Collider other);

    public abstract void OnTriggerExit(Collider other);

    public abstract void Attack();

    public void ChangeState(State newState)
    {
        if (_currentState != newState)
        {
            _currentState = newState;
        }

    }

    public void StateMachine()
    {
        switch (_currentState)
        {
            default:
            case State.Idle:


                //can make some animation

                break;

            case State.Attacking:
                Attack();
                break;
        }
    }
}
