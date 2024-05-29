using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapons", menuName = "Weapons/MeleeWeapons/MeleeWeapon", order = 0)]
public class MeleeSO : WeaponSOBase
{
    [Header("Weapon Data")]
    public MeleeWeaponType MelleType;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;
    public GameObject Model;

    [Header("Config ScriptableObjects")]
    public MeleeAttackConfigSO AttackConfig;

   
    private float _lastAttackTime;
    private MonoBehaviour _activeMonoBehaviour;
    
    private Transform _camHolderTransform;
    private Animator _animator;
    // verify if they are all needed
    private Vector3 _currentRotation, _targetRotation, _targetPosition, _currentPosition, _initialPosition;


    #region CONSTANTS FOR ANIMATIONS NAMES  
    // animations data
    public const string IDLE = "Idle";
    public const string ATTACK = "PoliceBatAttack";
    #endregion


   // string _currentAnimationState = IDLE;


    
    public  override void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this._activeMonoBehaviour = activeMonoBehaviour;
        _lastAttackTime = 0;
        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent, false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);


        _initialPosition = Model.transform.localPosition;

        // only works if the only camera on the scene are the player camera
        _camHolderTransform = GameObject.FindObjectOfType<Camera>().transform.parent;

        _animator= Model.GetComponent<Animator>();
        
    }
    public override void Attack()
    {
        Utilities.ChangeAnimationState(_animator,IDLE,ATTACK);
        _animator.SetFloat("AttackFreq", 1/ AttackConfig.attackSpeed);
        if (Time.time > AttackConfig.attackSpeed + _lastAttackTime)
        {
            Utilities.ChangeAnimationState(_animator, ATTACK ,IDLE);
            _lastAttackTime = Time.time;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, AttackConfig.attackDistance, AttackConfig.attackLayer))
            {
                Debug.Log("entering");
                if (hit.collider != null)
                {
                    Debug.Log("swiping");
                    ////////////////////////////////
                    if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.ApplyDamage(AttackConfig.attackDamage);
                       
                    }
                }
            }
        }
    }

    /*public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (_currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        _currentAnimationState = newState;
        _animator.Play(_currentAnimationState);
        //_animator.CrossFadeInFixedTime(currentAnimationState,0.4f);
    }

    public bool ISAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else { return false; }
    }*/
}
