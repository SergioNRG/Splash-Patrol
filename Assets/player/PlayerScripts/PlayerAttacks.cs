using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour
{

    [SerializeField] private Image _crosshair;

    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;

    [Header("Normal Camera")]
    [SerializeField] private CinemachineVirtualCamera _normalCam;

    [Header("Aim Camera")]
    [SerializeField] private CinemachineVirtualCamera _aimCam;

    [Header("CamEffecttsSO")]
    private CameraEffectController _cameraEffectsScript;
    private PlayerGunSelector _playerGunSelector;

    private Vector2 _scroll;


    private bool _isAttacking;
   
    void Start()
    {
        _playerGunSelector = GetComponent<PlayerGunSelector>(); 
        _isAttacking = false;
    }

    void Update()
    {
        UpdateCrosshair();
        if (_isAttacking)
        {           
            if (_playerGunSelector.ActiveGun != null && _playerGunSelector.ActiveGun.Model.activeInHierarchy)
            {
                _playerGunSelector.ActiveGun.Attack();              
                _playerGunSelector.ActiveGun.UpdateForWeaponRecoil();
            }else { _playerGunSelector.ActiveMelee.Attack(); }
        }
    }

    private void UpdateCrosshair()
    {
        Vector3 gunTip = _playerGunSelector.ActiveGun.GetRaycastOrigin();
        Vector3 gunForward = _playerGunSelector.ActiveGun.GetGunForward();
        Vector3 hitPoint = gunTip + gunForward *20;

       

        if (Physics.Raycast(gunTip,gunForward,out RaycastHit hit, float.MaxValue,_playerGunSelector.ActiveGun.ShootConfig.HitMask))
        {
            hitPoint = hit.point;
        }

        Vector3 screenSpaceLocation = Camera.main.WorldToScreenPoint(hitPoint);
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_crosshair.transform.parent,screenSpaceLocation,null,out Vector2 localPoint))
        {
            _crosshair.rectTransform.anchoredPosition = localPoint;
            
        }
        else { _crosshair.rectTransform.anchoredPosition = Vector3.zero; }
    }



    #region  Subcribe methods from InputReader SO
    private void OnEnable()
    {
        _inputReader.OnAttackEvent += OnAttack;
        _inputReader.OnAttackCanceledEvent += OnAttackCanceled;
        _inputReader.OnAimEvent += OnAim;
        _inputReader.OnAimCanceledEvent += OnAimCancelled;
        _inputReader.OnWeaponSelectorEvent += OnWeaponSelector;

    }

  

    private void OnDisable()
    {
        _inputReader.OnAttackEvent -= OnAttack;
        _inputReader.OnAttackCanceledEvent -= OnAttackCanceled;
        _inputReader.OnAimEvent -= OnAim;
        _inputReader.OnAimCanceledEvent -= OnAimCancelled;
        _inputReader.OnWeaponSelectorEvent -= OnWeaponSelector;
    }

    #endregion

    #region METHODS THAT SUBSCRIBE THE EVENTS
    private void OnAim()
    {
        _aimCam.Priority = 20;
    }
    private void OnAimCancelled()
    {
        _aimCam.Priority = 9;
    }

    private void OnWeaponSelector(Vector2 scroll)
    {
        _scroll = scroll.normalized ;
        OnAimCancelled();
        WeaponSwap();
    }


    private void OnAttack()
    {
        _isAttacking = true;       
    }

    private void OnAttackCanceled()
    {
        _isAttacking = false;           
    }

    #endregion

    #region METHODS

    private void WeaponSwap()
    {
        if (_scroll.y < 0)
        {
            _playerGunSelector.ActiveMelee.Model.SetActive(true);
            _playerGunSelector.ActiveGun.Model.SetActive(false);
        }
        if (_scroll.y > 0)
        {
            _playerGunSelector.ActiveMelee.Model.SetActive(false);
            _playerGunSelector.ActiveGun.Model.SetActive(true);
        }
    }

    #endregion
}
