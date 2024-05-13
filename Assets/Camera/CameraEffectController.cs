using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectController : MonoBehaviour
{
    // camera rotations
    private Vector3 _currentRotation, _targetRotation,_targetPosition,_currentPosition,_initialPosition;
    [SerializeField] Transform _camHolderTransform;

    [SerializeField] float _recoilx;
    [SerializeField] float _recoily;
    [SerializeField] float _recoilz;

    [SerializeField] float _kickBackz;

    [SerializeField] private  float _snappiness, _returnAmount;

    private void Start()
    {
        _initialPosition = transform.localPosition;
    }

    private void Update()
    {
        _targetRotation = Vector3.Lerp(_targetRotation,Vector3.zero,Time.deltaTime* _returnAmount);
        _currentRotation = Vector3.Slerp(_currentRotation,_targetRotation,Time.fixedDeltaTime * _snappiness);
        transform.localRotation = Quaternion.Euler(_currentRotation);   
        _camHolderTransform.localRotation = Quaternion.Euler(_currentRotation);
        KickBack();
    }

    public void recoil()
    {
        _targetPosition -= new Vector3(0,0,_kickBackz);
        _targetRotation += new Vector3(_recoilx, Random.Range(-_recoily, _recoily), Random.Range(- _recoilz, _recoilz));
        Debug.Log("recoiling");
    }
    public void KickBack()
    {
        _targetPosition = Vector3.Lerp(_targetPosition,_initialPosition,Time.deltaTime* _returnAmount);
        _currentRotation = Vector3.Lerp(_currentPosition,_targetPosition,Time.fixedDeltaTime * _snappiness);
        transform.localPosition = _currentPosition;
    }
}

