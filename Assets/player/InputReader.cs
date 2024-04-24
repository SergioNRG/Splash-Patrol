using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Input/Input Reader" , fileName = "InputReader")]

public class InputReader : ScriptableObject, PlayerInputs.IPlayerActions

{
    // create events to deal with the actions on player action map
    public event UnityAction<Vector2> OnMoveEvent;
    public event UnityAction<Vector2> OnLookEvent;
    public event UnityAction OnSprintEvent;
    public event UnityAction OnSprintCanceledEvent;
    public event UnityAction OnJumpEvent;
    public event UnityAction OnJumpCanceledEvent;   
    public event UnityAction OnAttackEvent;
    public event UnityAction OnAttackCanceledEvent;
    public event UnityAction OnAimEvent;
    public event UnityAction OnAimCanceledEvent;
    public event UnityAction OnCrouchEvent;
    public event UnityAction OnCrouchCanceledEvent;



    //create reference to generated C# class 
    private PlayerInputs _playerInputs;

    // instantiate a player input, set the callbacks and enable it 
    private void OnEnable()
    {
        
        if (_playerInputs == null)
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Player.SetCallbacks(this);
        }

        _playerInputs.Enable();
    }


    // disable the player inputs 
    public void OnDisable()
    {
        _playerInputs.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // check if the event is not null and invoke
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        OnLookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // check if the jumpevent is not null and if the context as performed
        if (OnJumpEvent != null && context.performed)
        {
            OnJumpEvent.Invoke();
        }


        // check if the jumpcancelledevent is not null and if the context as been canceled
        if (OnJumpCanceledEvent != null && context.canceled)
        {
            OnJumpCanceledEvent.Invoke();
        }
    } 

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (OnAttackEvent != null && context.performed)
        {
            OnAttackEvent.Invoke();
        }

        if (OnAttackCanceledEvent != null && context.canceled)
        {
            OnAttackCanceledEvent.Invoke();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (OnAimEvent != null && context.performed)
        {
            OnAimEvent.Invoke();
        }

        if (OnAimCanceledEvent != null && context.canceled)
        {
            OnAimCanceledEvent.Invoke();
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (OnCrouchEvent != null && context.performed)
        {
            OnCrouchEvent.Invoke();
        }

        if (OnAimCanceledEvent != null && context.canceled)
        {
            OnCrouchCanceledEvent.Invoke();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (OnSprintEvent != null && context.performed)
        {
            OnSprintEvent.Invoke();
        }

        if (OnSprintCanceledEvent != null && context.canceled)
        {
            OnSprintCanceledEvent.Invoke();
        }
    }
}
