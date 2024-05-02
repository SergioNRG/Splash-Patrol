using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private enum AimState
    {
        Idle,
        Aiming,
        NotAiming
    }

    private AimState _currentAimState;

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




    //private bool _isAiming;

    private float _timeToStopNotAiming = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        _currentAimState = AimState.Idle;
        _normalSpeedX = _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
        _normalSpeedY = _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;
        _cam.m_Lens.FieldOfView = _normalFov;
        //_isAiming = false;
        Debug.Log(_normalSpeedX);

    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentAimState)
        {
            case AimState.Idle:
                break;

            case AimState.Aiming:
                if (_cam.m_Lens.FieldOfView.ToString() != _aimFov.ToString())
                {
                    _timeToStopNotAiming = 0;
                    _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _aimFov, _zoomSpeed * Time.deltaTime);
                    _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _aimSpeedX;
                    _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _aimSpeedY;
                    Debug.Log("oioi");
                }
                break;

            case AimState.NotAiming:
                if (_cam.m_Lens.FieldOfView.ToString() != _normalFov.ToString())// _timeToStopNotAiming < 0.45f)
                {
                    _timeToStopNotAiming += Time.deltaTime;
                    _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _normalFov, _zoomSpeed * Time.deltaTime);
                    _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _normalSpeedX;
                    _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _normalSpeedY;
                    Debug.Log("fuifui");
                }else { _currentAimState = AimState.Idle; }
                break;
        }
        //AimCheck();
    }

    #region Input Events From SO
    private void OnEnable()
    {
        _inputReader.OnAttackEvent += OnAttack;
        _inputReader.OnAttackCanceledEvent += OnAttackCanceled;
        _inputReader.OnAimEvent += OnAim;
        _inputReader.OnAimCanceledEvent += OnAimCancelled;
    }

    private void OnDisable()
    {
        _inputReader.OnAttackEvent -= OnAttack;
        _inputReader.OnAttackCanceledEvent -= OnAttackCanceled;
        _inputReader.OnAimEvent -= OnAim;
        _inputReader.OnAimCanceledEvent -= OnAimCancelled;
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


    private void OnAttack()
    {
        Debug.Log("Attacking");
    }

    private void OnAttackCanceled()
    {
        Debug.Log(" Stop Attacking");
    }

    #endregion

    #region METHODS

   /* private void AimCheck()
    {
        if (_isAiming && (int)_cam.m_Lens.FieldOfView > _aimFov)
        {
            _timeToStop = 0;
            _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _aimFov, _zoomSpeed * Time.deltaTime);
            _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _aimSpeedX;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _aimSpeedY;
            Debug.Log("oioi");
        }
        else if (!_isAiming && _timeToStop < 0.5f)
        {
            _timeToStop += Time.deltaTime;
            _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _normalFov, _zoomSpeed * Time.deltaTime);
            _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _normalSpeedX;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _normalSpeedY;
            Debug.Log("fuifui");
        }
    }*/

 



    #endregion
}
