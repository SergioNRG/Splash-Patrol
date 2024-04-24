using System;
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
        _inputReader.OnMoveEvent += Move;
        _inputReader.OnLookEvent += Look;
        _inputReader.OnJumpEvent += Jump;
        _inputReader.OnJumpCanceledEvent += JumpCancelled;
        _inputReader.OnAimEvent += Aim;
        _inputReader.OnAimCanceledEvent += AimCancelled;
        _inputReader.OnCrouchEvent += Crouch;
        _inputReader.OnCrouchCanceledEvent += CrouchCanceled;
        _inputReader.OnAttackEvent += Attack;
        _inputReader.OnAttackCanceledEvent += AttackCanceled;
    }

    // unsubscribe to the events in SO
    private void OnDisable()
    {
        _inputReader.OnMoveEvent -= Move;
        _inputReader.OnLookEvent -= Look;
        _inputReader.OnJumpEvent -= Jump;
        _inputReader.OnJumpCanceledEvent -= JumpCancelled;
        _inputReader.OnAimEvent -= Aim;
        _inputReader.OnAimCanceledEvent -= AimCancelled;
        _inputReader.OnCrouchEvent -= Crouch;
        _inputReader.OnCrouchCanceledEvent -= CrouchCanceled;
        _inputReader.OnAttackEvent -= Attack;
        _inputReader.OnAttackCanceledEvent -= AttackCanceled;
    }

    #endregion

    private void CheckIfGrounded()
    {

    }

    private void Move(Vector2 movement)
    {
        Debug.Log("moving");
    }

    private void Look(Vector2 arg0)
    {
        Debug.Log("looking");
    }


    private void Jump()
    {
        Debug.Log("is jumping");
    }
    private void JumpCancelled()
    {
        Debug.Log("not jumping");
    }

    private void Crouch()
    {
        Debug.Log("Crouching");
    }
    private void CrouchCanceled()
    {
        Debug.Log(" Stop Crouching");
    }

    private void Aim()
    {
        Debug.Log("Aiming");
    }
    private void AimCancelled()
    {
        Debug.Log(" stop Aiming");
    }


    private void Attack()
    {
        Debug.Log("Attacking");
    }

    private void AttackCanceled()
    {
        Debug.Log(" Stop Attacking");
    }


}
