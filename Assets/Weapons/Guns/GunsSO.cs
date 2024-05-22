using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Guns/Gun", order = 0)]
public class GunsSO : WeaponSOBase,System.ICloneable
{
    public GunType Type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public DamageConfig DamageConfig;
    public ShootConfig ShootConfig;
    public TrailConfig TrailConfig;

    public GameObject _model;
    public LiquidAmmoDisplay AmmoDisplay;

    public int MaxAmmo;
    public int CurrentAmmo;
    private MonoBehaviour _activeMonoBehaviour;
    
    private float _lastShootTime;
    private ParticleSystem _shootSystem;
    private ObjectPool<TrailRenderer> _trailPool;


    private Vector3 _currentRotation, _targetRotation, _targetPosition, _currentPosition, _initialPosition;
    private Transform _camHolderTransform;

    public override void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        
        this._activeMonoBehaviour = activeMonoBehaviour;
        _lastShootTime = 0;
        // check if is working
        CurrentAmmo = MaxAmmo;
        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);


        _model = Instantiate(ModelPrefab);
        _model.transform.SetParent(Parent,false);
        _model.transform.localPosition = SpawnPoint;
        _model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        _shootSystem = _model.GetComponentInChildren<ParticleSystem>();
        AmmoDisplay = _model.GetComponentInChildren<LiquidAmmoDisplay>();
        AmmoDisplay.UpdateAmount(CurrentAmmo, MaxAmmo);
        _initialPosition = _model.transform.localPosition;

        // only works if the only camera on the scene are the player camera
        _camHolderTransform = GameObject.FindObjectOfType<Camera>().transform.parent;
    }



    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;
        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
      
        return trail;
    }

    private IEnumerator PlayTrail(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
    {
        recoil();
        TrailRenderer instance = _trailPool.Get();
        instance.gameObject.SetActive(true);
        instance .transform.position = startPoint;
        yield return null; // avoid position carry-over from last frame if reused

        instance.emitting = true;

        

        float distance = Vector3.Distance (startPoint, endPoint);
        float remainingDistance = distance;
        while( remainingDistance > 0 )
        {
            instance.transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = endPoint;

        if (hit.collider != null ) 
        {

            ////////////////////////////////
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable)) 
            {
                damageable.ApplyDamage(DamageConfig.GetDamageToApply(distance));
            }

   
        }


        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        _trailPool.Release(instance);
       
    }

    public override void Attack()
    {
        if (CanShoot())
        {
            if (Time.time > ShootConfig.FireRate + _lastShootTime)
            {
                _lastShootTime = Time.time;
                _shootSystem.Play();
                Vector3 shootDirection = _shootSystem.transform.forward
                    + new Vector3(Random.Range(-ShootConfig.Spread.x, ShootConfig.Spread.x),
                                  Random.Range(-ShootConfig.Spread.y, ShootConfig.Spread.y),
                                  Random.Range(-ShootConfig.Spread.z, ShootConfig.Spread.z));

                shootDirection.Normalize();

                if (Physics.Raycast(_shootSystem.transform.position, shootDirection, out RaycastHit hit, float.MaxValue, ShootConfig.HitMask))
                {
                    _activeMonoBehaviour.StartCoroutine(PlayTrail(_shootSystem.transform.position, hit.point, hit));
                }
                else
                {
                    _activeMonoBehaviour.StartCoroutine(PlayTrail(_shootSystem.transform.position, _shootSystem.transform.position + (shootDirection * TrailConfig.MissDistance), new RaycastHit()));

                }

                CurrentAmmo--;
                if (AmmoDisplay) 
                {
                    Debug.Log("Yes");
                    AmmoDisplay.UpdateAmount(CurrentAmmo, MaxAmmo); 
                }

                Debug.Log(CurrentAmmo);
            }
        }
      
       
    }

    public void Despawn()
    {
        _model.SetActive(false);
        Destroy(_model);    
        _trailPool.Clear();
        _shootSystem = null;
    }

    #region GET METHODS 

    public Vector3 GetRaycastOrigin()
    {
        Vector3 origin = _shootSystem.transform.position;
        return origin;
    }

    public Vector3 GetGunForward()
    {        
        return _shootSystem.transform.forward;
    }

    public bool CanShoot()
    {
        return (CurrentAmmo > 0);
    }

    public bool CanGetAmmo()
    {
        return CurrentAmmo < MaxAmmo;
    }

    #endregion

    #region RECOIL METHODS
    public void UpdateForWeaponRecoil()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, Time.deltaTime * ShootConfig._returnAmount);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, Time.fixedDeltaTime * ShootConfig._snappiness);
        _model.transform.localRotation = Quaternion.Euler(_currentRotation);
        _camHolderTransform.localRotation = Quaternion.Euler(_currentRotation);
        KickBack();
    }

    public void recoil()
    {
        _targetPosition -= new Vector3(0, 0, ShootConfig._kickBackz);
        _targetRotation += new Vector3(ShootConfig._recoilx, Random.Range(-ShootConfig._recoily, ShootConfig._recoily), Random.Range(-ShootConfig._recoilz, ShootConfig._recoilz));
    }
    public void KickBack()
    {
        _targetPosition = Vector3.Lerp(_targetPosition, _initialPosition, Time.deltaTime * ShootConfig._returnAmount);
        _currentRotation = Vector3.Lerp(_currentPosition, _targetPosition, Time.fixedDeltaTime * ShootConfig._snappiness);
        _model.transform.localPosition = _currentPosition;
    }


    #endregion

    public object Clone()
    {
        GunsSO gunsConfig = CreateInstance<GunsSO>();

        gunsConfig.Type = Type;
        gunsConfig.Name = Name;
        gunsConfig.DamageConfig = DamageConfig.Clone() as DamageConfig; 
        gunsConfig.ShootConfig = ShootConfig.Clone() as ShootConfig;
        gunsConfig.TrailConfig = TrailConfig.Clone() as TrailConfig;


        gunsConfig.ModelPrefab = ModelPrefab;
        gunsConfig.SpawnPoint = SpawnPoint; 
        gunsConfig.SpawnRotation = SpawnRotation;
        gunsConfig.MaxAmmo = MaxAmmo;
        gunsConfig.CurrentAmmo= CurrentAmmo;

        return gunsConfig;
    }
}
