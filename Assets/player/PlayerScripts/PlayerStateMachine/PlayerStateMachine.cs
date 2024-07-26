using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour
{
    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;

    [Header("Values to adjust")]

    [SerializeField] private float _basePlayerSpeed = 4.0f;
    [SerializeField] private float _sprintPlayerSpeed = 6.0f;
    [SerializeField] private float _crouchingPlayerSpeed = 1.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;



    private float _currentPlayerSpeed;
    private float _normalHeight = 2f;
    private float _crouchHeight = 1f;
    private float _normalCenter = 1f;
    private float _crouchCenter = 0.65f;
    private float _maxStamina = 100.0f;
    [SerializeField] private float _staminaLoseAmount; // just to test
    [SerializeField] private float _staminaRegenAmount; // just to test
    private float _currentStamina;
    [SerializeField] private Slider _staminaSLider;

    private Vector3 _move;
    private float _moveX;
    private float _moveZ;

    private Vector3 _playerVelocity;
    private bool _isJumping;
    private bool _isCrouching;
    private bool _isSprinting;

    private CharacterController _controller;
    private Transform _camTransform;
    private Transform _playerTransform;

    private PlayerBaseState _currentState;
    private PlayerStateFactory _stateFactory;  // state fcatory

    public Coroutine RegenStaminaRoutine;

    #region GET's and SET's
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

    public float MaxStamina { get { return _maxStamina; } set { _maxStamina = value; } }
    public float CurrentStamina { get { return _currentStamina; } set { _currentStamina = value; } }

    public float StaminaLoseAmount { get { return _staminaLoseAmount; } set { _staminaLoseAmount = value; } }
    public float StaminaRegenAmount { get { return _staminaRegenAmount; } set { _staminaRegenAmount = value; } }

    public Slider StaminaSlider { get { return _staminaSLider; } set { _staminaSLider = value; } }
    public float CrouchPlayerSpeed { get { return _crouchingPlayerSpeed; } }

    public bool IsCrouching {  get { return _isCrouching; } }

    public float NormalHeight { get { return _normalHeight; } set { _normalHeight = value; } }
    public float CrouchHeight {  get { return _crouchHeight; } set { _crouchHeight = value; } }
    public float NormalCenter {  get { return _normalCenter; } set { _normalCenter = value; } }
    public float CrouchCenter {  get { return _crouchCenter; } set { _crouchCenter = value; } }

    #endregion

    #region  Subcribe methods from InputReader SO Events

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
        if (_currentStamina > 0)
        {
            _isSprinting = true;
        }
        
    }

    private void OnSprintCanceled()
    {
        _isSprinting = false;
        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0;

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
        _isCrouching = true;
    }
    private void OnCrouchCanceled()
    {
        _isCrouching = false;
    }

    #endregion 

    private void Awake()
    {
        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.Grounded();
        _currentState.EnterState();
       
    }

    void Start()
    {
        UIManager.instance.DeactivateCursor();
        _camTransform = Camera.main.transform;
        _playerTransform = transform;
        _controller = GetComponent<CharacterController>();
        _controller.height = _normalHeight;
        _currentPlayerSpeed = _basePlayerSpeed;
        _isJumping = false;
        _currentStamina = _maxStamina;
        _staminaSLider.maxValue = _maxStamina;
        _staminaSLider.value = _maxStamina;

    }

    void Update()
    {
        ApplyGravity();
        _currentState.UpdateStates();
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

    public IEnumerator RegenRoutine()
    {
        if (_currentStamina < _maxStamina)
        {
            yield return new WaitForSeconds(0.1f);
            _currentStamina += _staminaRegenAmount * 0.1f;
            _staminaSLider.value = _currentStamina;
            yield return RegenRoutine();
        }else if (_currentStamina >= _maxStamina) 
        {
            _currentStamina = _maxStamina;
        }     
    }

}
