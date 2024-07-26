using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
//using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Guns/Gun", order = 0)]
public class GunsSO : WeaponSOBase,System.ICloneable
{
    [Header ("Guns Propertyies")]
    public GunType Type;
    public string Name;
    public GameObject ModelPrefab;
    public int MaxAmmo;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    [Header("Config ScriptableObjects")]
    public DamageConfig DamageConfig;
    public ShootConfig ShootConfig;
    public TrailConfig TrailConfig;

    public GameObject Model;
    private int CurrentAmmo;
    private LiquidAmmoDisplay AmmoDisplay;
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
        CurrentAmmo = MaxAmmo;

        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);


        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent,false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        _shootSystem = Model.GetComponentInChildren<ParticleSystem>();
        AmmoDisplay = Model.GetComponentInChildren<LiquidAmmoDisplay>();
        AmmoDisplay.UpdateAmount(CurrentAmmo, MaxAmmo);
        _initialPosition = Model.transform.localPosition;

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
                SoundManager.instance.PlayFXSound(SoundManager.instance.PopSound,1f); // play sound effect
                
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
                    AmmoDisplay.UpdateAmount(CurrentAmmo, MaxAmmo); 
                }
            }
        }
      
       
    }

    public void Despawn()
    {
        Model.SetActive(false);
        Destroy(Model);    
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
        Model.transform.localRotation = Quaternion.Euler(_currentRotation);
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
        Model.transform.localPosition = _currentPosition;
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
