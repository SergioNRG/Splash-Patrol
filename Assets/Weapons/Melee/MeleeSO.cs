using System.Linq;
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
    public AnimsController AnimsController;
   
    private float _lastAttackTime;
    private MonoBehaviour _activeMonoBehaviour;
    
    private Transform _camHolderTransform;
    private Animator _animator;
    // verify if they are all needed
    private Vector3 _currentRotation, _targetRotation, _targetPosition, _currentPosition, _initialPosition;


    #region STRINGS FOR ANIMATIONS NAMES  
    // animations data

    private string IdleAnim;
    private string AttackAnim;

    #endregion


    // string _currentAnimationState = IDLE;

    public void OnEnable()
    {
       
        IdleAnim = AnimsController.Anims.Single(IdleAnim => IdleAnim.AnimKey == "IDLE").AnimName;
        AttackAnim = AnimsController.Anims.Single(AttackAnim => AttackAnim.AnimKey == "ATTACK").AnimName;
    }

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
        AnimsController.ChangeAnimationState(_animator, IdleAnim, AttackAnim);
        _animator.SetFloat("AttackFreq", 1/ AttackConfig.attackSpeed);
        if (Time.time > AttackConfig.attackSpeed + _lastAttackTime)
        {
            AnimsController.ChangeAnimationState(_animator, AttackAnim, IdleAnim );
            _lastAttackTime = Time.time;
            if (Camera.main != null)
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, AttackConfig.attackDistance, AttackConfig.attackLayer))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                        {
                            damageable.ApplyDamage(AttackConfig.attackDamage);

                        }
                    }
                }
            }
        }
    }
}
