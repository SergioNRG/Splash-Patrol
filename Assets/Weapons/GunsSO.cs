using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]
public class GunsSO : ScriptableObject
{
    public GunType Type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public ShootConfig ShootConfig;
    public TrailConfig TrailConfig;

    private MonoBehaviour _activeMonoBehaviour;
    private GameObject _model;
    private float _lastShootTime;
    private ParticleSystem _shootSystem;
    private ObjectPool<TrailRenderer> _trailPool;

    //

    private Vector3 _currentRotation, _targetRotation, _targetPosition, _currentPosition, _initialPosition;
    Transform _camHolderTransform;

    public void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this._activeMonoBehaviour = activeMonoBehaviour;
        _lastShootTime = 0;
        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);

        _model = Instantiate(ModelPrefab);
        _model.transform.SetParent(Parent,false);
        _model.transform.localPosition = SpawnPoint;
        _model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        _shootSystem = _model.GetComponentInChildren<ParticleSystem>();

        _initialPosition = _model.transform.localPosition;

        // only works if the only cameras on the scene are the player cameras
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

        if (hit.collider != null ) { Debug.Log("hiting something"); }


        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        _trailPool.Release(instance);
        recoil();
    }

    public void Shoot()
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

            if (Physics.Raycast(_shootSystem.transform.position,shootDirection,out RaycastHit hit, float.MaxValue, ShootConfig.HitMask))
            {
                _activeMonoBehaviour.StartCoroutine(PlayTrail(_shootSystem.transform.position,hit.point,hit));
            }else
            {
                _activeMonoBehaviour.StartCoroutine(PlayTrail(_shootSystem.transform.position, _shootSystem.transform.position + (shootDirection * TrailConfig.MissDistance), new RaycastHit()));

            }
        }
       
    }

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
        Debug.Log("recoiling");
    }
    public void KickBack()
    {
        _targetPosition = Vector3.Lerp(_targetPosition, _initialPosition, Time.deltaTime * ShootConfig._returnAmount);
        _currentRotation = Vector3.Lerp(_currentPosition, _targetPosition, Time.fixedDeltaTime * ShootConfig._snappiness);
        _model.transform.localPosition = _currentPosition;
    }
    #endregion

}
