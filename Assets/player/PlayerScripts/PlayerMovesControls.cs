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
    
    [SerializeField] private float _basePlayerSpeed = 2.0f;
    [SerializeField] private float _sprintPlayerSpeed = 4.0f;
    [SerializeField] private float _crouchingPlayerSpeed = 1.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private bool _isPlayerGrounded;



    private float _currentPlayerSpeed;
    private float _normalHeight = 2f;
    private float _crouchHeight = 1f;
   
    private Vector3 _move;
    private Vector3 _mouseMov;
    private Vector3 _playerVelocity; 
    private bool _isJumping;
    private bool _isCrouching;
    private bool _isSprinting; 

    private CharacterController _controller;
    private Transform _camTransform;

   
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _camTransform = Camera.main.transform;
        _controller = GetComponent<CharacterController>();
        _currentPlayerSpeed = _basePlayerSpeed;
        _isJumping = false;
    }

    private void Update()
    {
        CheckIfGrounded();
        MoveAndRotationRelativeToCamera();
        Jump();
    }


    #region  Subcribe methods from InputReader SO

    // subscribe to events on SO
    private void OnEnable()
    {
        _inputReader.OnMoveEvent += OnMove;
        _inputReader.OnSprintEvent += OnSprint;
        _inputReader.OnSprintCanceledEvent += OnSprintCanceled;
        _inputReader.OnJumpEvent += OnJump;
        _inputReader.OnJumpCanceledEvent += OnJumpCancelled;       
        _inputReader.OnCrouchEvent += OnCrouch;
        _inputReader.OnCrouchCanceledEvent += OnCrouchCanceled;
    }

   

    // unsubscribe to the events in SO
    private void OnDisable()
    {
        _inputReader.OnMoveEvent -= OnMove;
        _inputReader.OnSprintEvent -= OnSprint;
        _inputReader.OnSprintCanceledEvent -= OnSprintCanceled;
        _inputReader.OnJumpEvent -= OnJump;
        _inputReader.OnJumpCanceledEvent -= OnJumpCancelled;        
        _inputReader.OnCrouchEvent -= OnCrouch;
        _inputReader.OnCrouchCanceledEvent -= OnCrouchCanceled;
    }

    #endregion


    #region METHODS THAT SUBSCRIBE THE EVENTS
    private void OnMove(Vector2 movement)
    {
        _move = new Vector3(movement.x, 0f, movement.y);
    }
    private void OnSprint()
    {
        if (_isPlayerGrounded && !_isCrouching)
        {
            _currentPlayerSpeed = _sprintPlayerSpeed;
            Debug.Log("Sprinting");
        }
        
    }

    private void OnSprintCanceled()
    {
        if (!_isCrouching)
        {
            _currentPlayerSpeed = _basePlayerSpeed;
            Debug.Log("Stop Sprinting");
        }
        
    }


    private void OnJump()
    {
        _isJumping = true;

    }
    private void OnJumpCancelled()
    {
        _isJumping = false;

    }

    private void OnCrouch()
    {
        if (_isPlayerGrounded)
        {
            _isCrouching = true;
            _controller.height = _crouchHeight;
            _currentPlayerSpeed = _crouchingPlayerSpeed;
            Debug.Log("Crouching");
        }
        
    }
    private void OnCrouchCanceled()
    {
        _isCrouching = false;
        _controller.height = _normalHeight;
        _currentPlayerSpeed = _basePlayerSpeed;
        Debug.Log(" Stop Crouching");
    }

    #endregion   


    #region METHODS
    private void MoveAndRotationRelativeToCamera()
    {
        Vector3 forward = _camTransform.forward;
        Vector3 right = _camTransform.right;

        forward.y = 0;
        right.y = 0;
        //forward = forward.normalized;
        //right = right.normalized;

        Vector3 forwardRelativeVerticalInput = _move.z * forward;
        Vector3 rightRelativeHorizontalInput = _move.x * right;


        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput; 

        _controller.Move(cameraRelativeMovement.normalized * Time.deltaTime * _currentPlayerSpeed);
        transform.rotation = Quaternion.LookRotation(forward);
    }

 
    private void Jump()
    {
        if (_isJumping && _isPlayerGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

    }
    private void CheckIfGrounded()
    {
        _isPlayerGrounded = _controller.isGrounded;
        if (_isPlayerGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
    }

    #endregion

}
