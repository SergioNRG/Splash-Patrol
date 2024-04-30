using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [Header("InputReaderSO")]
    [SerializeField] private InputReader _inputReader;

    [Header("Zoom Camera")]
    [SerializeField] private CinemachineVirtualCamera _cam;

    [SerializeField] private float _aimFov = 30;
    private float _normalFov = 60;

    private bool _isAiming;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam.m_Lens.FieldOfView = _normalFov;
        _isAiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAiming)
        {
            _cam.m_Lens.FieldOfView = Mathf.Clamp(Mathf.Lerp(_cam.m_Lens.FieldOfView, _aimFov, 5 * Time.deltaTime), _aimFov, _normalFov);
        }
      
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
        _isAiming = true;

    }
    private void OnAimCancelled()
    {
        _isAiming = false;
        _cam.m_Lens.FieldOfView = _normalFov;
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
