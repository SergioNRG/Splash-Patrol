using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;


    #region Input Events From SO

    // subscribe to events on SO
    private void OnEnable()
    {
        _inputReader.OnMoveEvent += OnMove;
        _inputReader.OnJumpEvent += OnJump;
        _inputReader.OnJumpCancelledEvent += OnJumpCancelled;
    }


    // unsubscribe to the events in SO
    private void OnDisable()
    {
        _inputReader.OnMoveEvent -= OnMove;
        _inputReader.OnJumpEvent -= OnJump;
        _inputReader.OnJumpCancelledEvent -= OnJumpCancelled;
    }

    #endregion

    private void OnMove(Vector2 movement)
    {
        Debug.Log("moving");
    }

    private void OnJump()
    {
        Debug.Log("is jumping");
    }
    private void OnJumpCancelled()
    {

        Debug.Log("not jumping");
    }

    private void CheckIfGrounded()
    {

    }
}
