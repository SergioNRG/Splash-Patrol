
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int plusenemies = 2;
    [SerializeField] private int enemiestospawn = 0;
    [SerializeField] private int _wavesToNextLvl = 10;
    [SerializeField] private float waveDelay;
    [SerializeField] private GameObject _portalToLVL;
    [SerializeField] private Transform[] _spawnpoints;
    [SerializeField] private Transform _bossSpot;
    [SerializeField] private Transform _lvlPortalSpot;

    public List<GameObject> _activEnemies = new List<GameObject>();


    private int wavecount = 0;   
    private Coroutine _coroutine;

    private Canvas _canvas;

    public delegate void ChangeLvlEvent();
    public event ChangeLvlEvent OnLvlChanged;

    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this; 
        }else { Destroy(gameObject); }
    }
    private void Start()
    {
        _canvas = _portalToLVL.GetComponentInChildren<Canvas>();    
       // _portalToLVL.SetActive(false);
        _coroutine = StartCoroutine(WaveDelayCoroutine());
        OnLvlChanged += GameManager.Instance.ChangeLvl;
    }

  /*  private void OnEnable()
    {
       
    }*/

    private void OnDisable()
    {
        OnLvlChanged -= GameManager.Instance.ChangeLvl;
    }
    private void Update()
    {

    }
    public void WaveSpawn()
    {
        enemiestospawn += plusenemies;          
        for (int i = 1; i <= enemiestospawn; i++)           
        {              
            SpawnEnemy();           
        }

        wavecount++;

        if ((wavecount % _wavesToNextLvl == 0) && (wavecount != 0))           
        {                
            Debug.Log("SPAWN BOSS");                
            SpawnBoss("GolemBoss");           
        }

        StopMyCoroutine(_coroutine);
        
        Debug.Log("WAVE " + wavecount);            
        //Wavetxt.text = wavecount.ToString();
    }

    private IEnumerator WaveDelayCoroutine()
    {
        yield return new WaitForSeconds(waveDelay);
        WaveSpawn();
    }


    private void SpawnEnemy()
    {
        List<string> keyList = new List<string>(ObjectPooler.instance.PoolDictionary.Keys);

        System.Random rand = new System.Random();
        string tag = keyList[rand.Next(keyList.Count-1)];
        GameObject mob = ObjectPooler.instance.TakeFromPool(tag);
        mob.transform.position = _spawnpoints[Random.Range(0, _spawnpoints.Length)].transform.position;
        mob.SetActive(true);
        _activEnemies.Add(mob);
    }

    private void SpawnBoss(string tag)
    {
        GameObject mob = ObjectPooler.instance.TakeFromPool(tag);
        mob.transform.position = _bossSpot.transform.position;        
        mob.transform.rotation = _bossSpot.transform.rotation;
        // mob.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;// mudar para boss sopt
        mob.SetActive(true);
        _activEnemies.Add(mob);
    }

    public void HandleEnemyKilled(Vector3 pos,GameObject enemy)
    {       
        _activEnemies.Remove(enemy);
        if (_activEnemies.Count <= 0)
        {
            if (wavecount % _wavesToNextLvl == 0)
            {
                OnLvlChanged?.Invoke();
                StopMyCoroutine(_coroutine);
                Debug.Log("entrou aki");
                //_portalToLVL.SetActive(true);
                //Transform spot = enemy.GetComponent<NavMeshAgent>().transform;
                Instantiate(_portalToLVL,_lvlPortalSpot.position, Quaternion.identity);
                //_canvas.transform.position = new Vector3(_portalToLVL.transform.position.x, _portalToLVL.transform.position.y + 5, _portalToLVL.transform.position.z);
            }
            else
            {
                _coroutine = StartCoroutine(WaveDelayCoroutine());
                Debug.Log("entrou coroutina");
            }
        }
    }

    private void StopMyCoroutine(Coroutine coroutine)
    {
        if (coroutine != null) {StopCoroutine(coroutine); }
    }
}
