using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovesControls : MonoBehaviour
{
    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;

    [Header("Values to adjust")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private bool groundedPlayer;

    private Vector3 _move;
    private Vector3 _mouseMov;
    private Vector3 _playerVelocity; 
    private bool _isJumping;

    private float desiredRotationSpeed = 0.1f;

    private CharacterController _controller;
    private Transform _camTransform;

    #region Input Events From SO

    // subscribe to events on SO
    private void OnEnable()
    {
        _inputReader.OnMoveEvent += OnMove;
        _inputReader.OnLookEvent += OnLook;
        _inputReader.OnSprintEvent += OnSprint;
        _inputReader.OnSprintCanceledEvent += OnSprintCanceled;
        _inputReader.OnJumpEvent += OnJump;
        _inputReader.OnJumpCanceledEvent += OnJumpCancelled;
        _inputReader.OnAimEvent += OnAim;
        _inputReader.OnAimCanceledEvent += OnAimCancelled;
        _inputReader.OnCrouchEvent += OnCrouch;
        _inputReader.OnCrouchCanceledEvent += OnCrouchCanceled;
        _inputReader.OnAttackEvent += OnAttack;
        _inputReader.OnAttackCanceledEvent += OnAttackCanceled;
    }

   

    // unsubscribe to the events in SO
    private void OnDisable()
    {
        _inputReader.OnMoveEvent -= OnMove;
        _inputReader.OnLookEvent -= OnLook;
        _inputReader.OnSprintEvent -= OnSprint;
        _inputReader.OnSprintCanceledEvent -= OnSprintCanceled;
        _inputReader.OnJumpEvent -= OnJump;
        _inputReader.OnJumpCanceledEvent -= OnJumpCancelled;
        _inputReader.OnAimEvent -= OnAim;
        _inputReader.OnAimCanceledEvent -= OnAimCancelled;
        _inputReader.OnCrouchEvent -= OnCrouch;
        _inputReader.OnCrouchCanceledEvent -= OnCrouchCanceled;
        _inputReader.OnAttackEvent -= OnAttack;
        _inputReader.OnAttackCanceledEvent -= OnAttackCanceled;
    }

    #endregion

    private void Start()
    {
        _camTransform = Camera.main.transform;
        _controller = GetComponent<CharacterController>();
        _isJumping = false;
    }

    private void Update()
    {
        CheckIfGrounded();
        Move();
        Jump();

        //RotateToCamera();



    }

    private void Move()
    {
        Vector3 forward = _camTransform.forward;
        Vector3 right = _camTransform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = _move.z *forward;
        Vector3 rightRelativeHorizontalInput = _move.x * forward;


        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput; 
        //_move = _camTransform.forward * _move.z + _camTransform.right * _move.x;
        _controller.Move(cameraRelativeMovement * Time.deltaTime * playerSpeed);
    }

 
    private void Jump()
    {
        if (_isJumping && groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

    }
    private void CheckIfGrounded()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
    }

    /*public void RotateToCamera()
    {
        _camTransform.forward = _mouseMov;
        //var camera = Camera.main;
        var forward = _camTransform.forward;
        var right = _camTransform.right;

         Vector3 desiredMoveDirection = forward;

        _camTransform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }*/

    #region Metods that subscribe the events
    private void OnMove(Vector2 movement)
    {
        _move = new Vector3(movement.x, 0f,movement.y);
        
    }

    private void OnLook(Vector2 lookAt)
    {
        _mouseMov =new Vector3(lookAt.x, lookAt.y,0) ;
       // Debug.Log("looking");
    }
    private void OnSprint()
    {
        Debug.Log("Sprinting");
    }

    private void OnSprintCanceled()
    {
        Debug.Log("Stop Sprinting");
    }


    private void OnJump()
    {
        _isJumping = true;
        Debug.Log("is jumping");
    }
    private void OnJumpCancelled()
    {
        _isJumping = false;
        Debug.Log("not jumping");
    }

    private void OnCrouch()
    {
        Debug.Log("Crouching");
    }
    private void OnCrouchCanceled()
    {
        Debug.Log(" Stop Crouching");
    }

    private void OnAim()
    {
        Debug.Log("Aiming");
    }
    private void OnAimCancelled()
    {
        Debug.Log(" stop Aiming");
    }


    private void OnAttack()
    {
        Debug.Log("Attacking");
    }

    private void OnAttackCanceled()
    {
        Debug.Log(" Stop Attacking");
    }

    #endregion   
}
