using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;

    [Header("Values to adjust")]

    [SerializeField] private float _basePlayerSpeed = 2.0f;
    [SerializeField] private float _sprintPlayerSpeed = 4.0f;
    [SerializeField] private float _crouchingPlayerSpeed = 1.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;
    //[SerializeField] 
    private bool _isPlayerGrounded;



    private float _currentPlayerSpeed;
    private float _normalHeight = 2f;
    private float _crouchHeight = 1f;
    private float _normalCenter = 1f;
    private float _crouchCenter = 0.65f;

    private Vector3 _move;
    private float _moveX;
    private float _moveZ;

    //private Vector3 _mouseMov;
    private Vector3 _playerVelocity;
    private bool _isJumping;
    private bool _isCrouching;
    private bool _isSprinting;

    private CharacterController _controller;
    private Transform _camTransform;
    private Transform _playerTransform;

    private PlayerBaseState _currentState;
    private PlayerStateFactory _stateFactory;  // state fcatory

    // gets and sets

    public Transform CamTransform { get { return _camTransform; } }
    public Transform PlayerTransform { get { return _playerTransform; } }

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsJumping { get { return _isJumping; } }
    public Vector3 PlayerVelocity { get { return _playerVelocity; } set { _playerVelocity = value; } }

    public float PlayerVelocityY { get { return _playerVelocity.y; } set { _playerVelocity.y = value; } }
    public float JumpHeight { get { return _jumpHeight; } set { _jumpHeight = value; } }
    public float GravityValue { get { return _gravityValue; } }
    public CharacterController Controller { get { return _controller; } set { _controller = value; } }

    public Vector3 Move { get { return _move; } }

    public float MoveX { get { return _moveX; } set { _moveX = value; } }
    public float MoveZ { get { return _moveZ; } set { _moveZ = value; } }

    public float CurrentPlayerSpeed { get { return _currentPlayerSpeed; } set { _currentPlayerSpeed = value; } }
    public float BasePlayerSpeed { get { return _basePlayerSpeed; } set { _basePlayerSpeed = value; } }

    public float SprintPlayerSpeed { get { return _sprintPlayerSpeed; } set { _sprintPlayerSpeed = value; }  }

    public bool IsSprinting {  get { return _isSprinting; } }

    public float CrouchPlayerSpeed { get { return _crouchingPlayerSpeed; } }

    public bool IsCrouching {  get { return _isCrouching; } }

    public float NormalHeight { get { return _normalHeight; } set { _normalHeight = value; } }
    public float CrouchHeight {  get { return _crouchHeight; } set { _crouchHeight = value; } }
    public float NormalCenter {  get { return _normalCenter; } set { _normalCenter = value; } }
    public float CrouchCenter {  get { return _crouchCenter; } set { _crouchCenter = value; } }

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
        _isSprinting = true;
       /* if (_isPlayerGrounded && !_isCrouching)
        {
            _currentPlayerSpeed = _sprintPlayerSpeed;
            Debug.Log("Sprinting");
        }*/

    }

    private void OnSprintCanceled()
    {
        _isSprinting = false;
       /* if (!_isCrouching)
        {
            _currentPlayerSpeed = _basePlayerSpeed;
            Debug.Log("Stop Sprinting");
        }*/

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
        _isCrouching = true;
        /* if (_isPlayerGrounded)
         {
             _isCrouching = true;
             _controller.height = _crouchHeight;
             _currentPlayerSpeed = _crouchingPlayerSpeed;
             Debug.Log("Crouching");
         }*/

    }
    private void OnCrouchCanceled()
    {
        _isCrouching = false;
       /* _controller.height = _normalHeight;
        _currentPlayerSpeed = _basePlayerSpeed;
        Debug.Log(" Stop Crouching");*/
    }

    #endregion 

    private void Awake()
    {
        //_controller = GetComponent<CharacterController>();
        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.Grounded();
        _currentState.EnterState();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _camTransform = Camera.main.transform;
        _playerTransform = transform;
        _controller = GetComponent<CharacterController>();
        _controller.height = _normalHeight;
        _currentPlayerSpeed = _basePlayerSpeed;
        _isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        _currentState.UpdateStates();
       // MoveAndRotationRelativeToCamera();
    }

    public void MoveAndRotationRelativeToCamera()
    {
        Vector3 forward = _camTransform.forward;
        Vector3 right = _camTransform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = _move.z * forward;
        Vector3 rightRelativeHorizontalInput = _move.x * right;


        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;

        _controller.Move(cameraRelativeMovement * Time.deltaTime * _currentPlayerSpeed);
        transform.rotation = Quaternion.LookRotation(forward);
    }

    public void ApplyGravity()
    {
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

}
