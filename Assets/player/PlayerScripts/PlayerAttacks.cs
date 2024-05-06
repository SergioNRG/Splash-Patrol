using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private enum AimState
    {
        Idle,
        Aiming,
        NotAiming,
    }

    private AimState _currentAimState;


    [Header("Weapons")]
    [SerializeField ] private GameObject[] _weapons;

    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;

    [Header("Zoom Camera")]
    [SerializeField] private CinemachineVirtualCamera _cam;


    [Header("Camera Values To Adjust")]
    [SerializeField] private int _aimFov = 30;
    [SerializeField] private int _normalFov = 60;
    [SerializeField] private float _zoomSpeed = 15;
    [SerializeField] private float _aimSpeedY = 30;
    [SerializeField] private float _aimSpeedX = 50;
    [SerializeField] private float _normalSpeedY ;
    [SerializeField] private float _normalSpeedX ;

    private int _weaponPos;
    private Vector2 _scroll;

    private bool _isScrolling = false;

   // private float _timeToStopNotAiming = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        _currentAimState = AimState.Idle;
        _normalSpeedX = _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
        _normalSpeedY = _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;
        _cam.m_Lens.FieldOfView = _normalFov;
        //_isAiming = false;
       // Debug.Log(_normalSpeedX);
        _weaponPos = 0;
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i] != null)
            {
                if (_weapons[i] == _weapons[_weaponPos]) { _weapons[_weaponPos].SetActive(true); }
                else { _weapons[i].SetActive(false); }
                Debug.Log(_weapons[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentAimState)
        {
            case AimState.Idle:
                break;

            case AimState.Aiming:
                if (_isScrolling)
                {
                    _currentAimState = AimState.NotAiming;
                }
                else { ZoomIn(); }
                
                break;

            case AimState.NotAiming:
                ZoomOut();
                
                break;

        }
        SelectWeapon();
    }

    #region Input Events From SO
    private void OnEnable()
    {
        _inputReader.OnAttackEvent += OnAttack;
        _inputReader.OnAttackCanceledEvent += OnAttackCanceled;
        _inputReader.OnAimEvent += OnAim;
        _inputReader.OnAimCanceledEvent += OnAimCancelled;
        _inputReader.OnReloadEvent += OnReload;
        _inputReader.OnReloadCanceledEvent += OnReloadCancelled;
        _inputReader.OnWeaponSelectorEvent += OnWeaponSelector;

    }

  

    private void OnDisable()
    {
        _inputReader.OnAttackEvent -= OnAttack;
        _inputReader.OnAttackCanceledEvent -= OnAttackCanceled;
        _inputReader.OnAimEvent -= OnAim;
        _inputReader.OnAimCanceledEvent -= OnAimCancelled;
        _inputReader.OnReloadEvent -= OnReload;
        _inputReader.OnReloadCanceledEvent -= OnReloadCancelled;
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
        _scroll = scroll.normalized ;
        _isScrolling = true;
    }


    private void OnAttack()
    {
        Debug.Log("Attacking");
    }

    private void OnAttackCanceled()
    {
        Debug.Log(" Stop Attacking");
    }

    private void OnReloadCancelled()
    {
        Debug.Log("reloading");
    }

    private void OnReload()
    {
        Debug.Log("stop reloading");
    }

    #endregion

    #region METHODS

    private void SelectWeapon()
    {

        if (_isScrolling)
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

            _weaponPos = Mathf.Clamp(_weaponPos, 0, _weapons.Length-1);
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i] != null)
                {
                    if (_weapons[i] == _weapons[_weaponPos]) { _weapons[_weaponPos].SetActive(true); }
                    else { _weapons[i].SetActive(false); }
                }
            }
        }  
          
    }

    private void Reload()
    {

    }

    private void ZoomIn() 
    {
        if (_cam.m_Lens.FieldOfView.ToString() != _aimFov.ToString())
        {
            //_timeToStopNotAiming = 0;
            _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _aimFov, _zoomSpeed * Time.deltaTime);
            _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _aimSpeedX;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _aimSpeedY;

        }

    }

    private void ZoomOut()
    {
        if (_cam.m_Lens.FieldOfView.ToString() != _normalFov.ToString())// _timeToStopNotAiming < 0.45f)
        {
            //_timeToStopNotAiming += Time.deltaTime;
            _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _normalFov, _zoomSpeed * Time.deltaTime);
            _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _normalSpeedX;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _normalSpeedY;
        }
        else 
        {
            _currentAimState = AimState.Idle;
            _isScrolling = false;
        }
    }





    #endregion
}
