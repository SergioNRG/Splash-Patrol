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


    private Vector3 _move;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
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
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        Move();

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        
        

       /* if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }*/

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Move()
    {
        controller.Move(_move * Time.deltaTime * playerSpeed);
    }
    private void CheckIfGrounded()
    {

    }

    #region Metods that subscribe the events
    private void OnMove(Vector2 movement)
    {
        _move = new Vector3(movement.x, 0,movement.y);
    }

    private void OnLook(Vector2 arg0)
    {
        Debug.Log("looking");
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
        Debug.Log("is jumping");
    }
    private void OnJumpCancelled()
    {
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
