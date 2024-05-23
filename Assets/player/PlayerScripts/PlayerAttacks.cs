using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour
{
    private enum AimState
    {
        Idle,
        Aiming,
        NotAiming,
        ChangeWeapon
    }

    private AimState _currentAimState;

    [SerializeField] private Image _crosshair;

    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;

    [Header("Zoom Camera")]
    [SerializeField] private CinemachineVirtualCamera _cam;

    [Header("CamEffecttsSO")]
    private CameraEffectController _cameraEffectsScript;
    private PlayerGunSelector _playerGunSelector;

    [Header("Camera Values To Adjust")]
    [SerializeField] private int _aimFov = 30;
    [SerializeField] private int _normalFov = 60;
    [SerializeField] private float _zoomSpeed = 15;
    [SerializeField] private float _aimSpeedY = 30;
    [SerializeField] private float _aimSpeedX = 50;
    [SerializeField] private float _normalSpeedY ;
    [SerializeField] private float _normalSpeedX ;

   /* private int _weaponPos;
    private Vector2 _scroll;*/

    private bool _isScrolling = false;
    private bool _isAttacking;


    
    // Start is called before the first frame update
    void Start()
    {
        _playerGunSelector = GetComponent<PlayerGunSelector>(); 
        _currentAimState = AimState.Idle;
        _normalSpeedX = _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
        _normalSpeedY = _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;
        _cam.m_Lens.FieldOfView = _normalFov;
        _isAttacking = false;
        // _weaponPos = 0;

        /*for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i] != null)
            {
                if (_weapons[i] == _weapons[_weaponPos]) { _weapons[_weaponPos].SetActive(true); }
                else { _weapons[i].SetActive(false); }
                Debug.Log(_weapons[i]);
            }
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCrosshair();
        if (_isAttacking)
        {           
            if (_playerGunSelector.ActiveGun != null)
            {
                _playerGunSelector.ActiveGun.Attack();              
                _playerGunSelector.ActiveGun.UpdateForWeaponRecoil();
            }else { _playerGunSelector.ActiveMelee.Attack(); }
        }
       
        switch (_currentAimState)
        {
            case AimState.Idle:
                if (_isScrolling) { _currentAimState = AimState.ChangeWeapon; }
                break;
                
            case AimState.Aiming:
                if (_isScrolling) { _currentAimState = AimState.ChangeWeapon; }
                else { ZoomIn(); }
                break;

            case AimState.NotAiming:
                if (_cam.m_Lens.FieldOfView.ToString() != _normalFov.ToString()){ ZoomOut(); }
                if (_isScrolling) { _currentAimState = AimState.ChangeWeapon;}
                else if (_cam.m_Lens.FieldOfView.ToString() == _normalFov.ToString()){ _currentAimState = AimState.Idle; }
                break;

            case AimState.ChangeWeapon:
               // if (_cam.m_Lens.FieldOfView.ToString() != _normalFov.ToString()) { ZoomOut(); }
               //SelectWeapon();               
                break;

        }
        
    }

   /* private void LateUpdate()
    {

        if (_isAttacking)
        {
            if (ActiveGun != null)
            {
                ActiveGun.Attack();
            }
        }

    }*/

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
        _currentAimState = AimState.Aiming;
    }
    private void OnAimCancelled()
    {
        _currentAimState = AimState.NotAiming;
    }

    private void OnWeaponSelector(Vector2 scroll)
    {
       // _scroll = scroll.normalized ;
        _isScrolling = true;
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

   /* private void SelectWeapon()
    {
        if (_scroll.y < 0)
        {
            _weaponPos--;
            Debug.Log(_weaponPos);

        }

        if (_scroll.y > 0)
        {
            _weaponPos++;
            Debug.Log(_weaponPos);
        }

        _weaponPos = Mathf.Clamp(_weaponPos, 0, _weapons.Length - 1);
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i] != null)
            {
                if (_weapons[i] == _weapons[_weaponPos]) { _weapons[_weaponPos].SetActive(true); }
                else { _weapons[i].SetActive(false); }
            }
        }
        _isScrolling = false;

    }*/

    private void ZoomIn() 
    {
        if (_cam.m_Lens.FieldOfView.ToString() != _aimFov.ToString())
        {
            _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _aimFov, _zoomSpeed * Time.deltaTime);
            _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _aimSpeedX;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _aimSpeedY;
        }

    }

    private void ZoomOut()
    {
       
        _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _normalFov, _zoomSpeed * Time.deltaTime);
        _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _normalSpeedX;
        _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _normalSpeedY;
    }


    #endregion
}
