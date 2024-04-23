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
    public event UnityAction OnJumpEvent;
    public event UnityAction OnJumpCancelledEvent;


    //create reference to generated C# class 
    private PlayerInputs _playerInputs;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (_playerInputs == null)
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Player.SetCallbacks(this);
        }

        _playerInputs.Enable();
    }

    public void OnDisable()
    {
        _playerInputs.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // check if the event is not null and invoke
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (OnJumpEvent != null && context.performed)
        {
            OnJumpEvent.Invoke();
        }

        if (OnJumpCancelledEvent != null && context.canceled)
        {
            OnJumpCancelledEvent.Invoke();
        }
    }
}
