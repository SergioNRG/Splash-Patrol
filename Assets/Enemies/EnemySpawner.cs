
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int plusenemies = 2;
    [SerializeField] private int enemiestospawn = 0;
    [SerializeField] private int _wavesToNextLvl = 10;
    [SerializeField] private float waveDelay;
    [SerializeField] private GameObject _portalToLVL;
    [SerializeField] Transform[] spawnpoints;

    public List<GameObject> _activEnemies = new List<GameObject>();
    public int lvl = 1;

    private int wavecount = 0;   
    private Coroutine _coroutine;

    private Canvas _canvas;

    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }else { Destroy(gameObject); }
    }
    private void Start()
    {
        _canvas = _portalToLVL.GetComponentInChildren<Canvas>();    
        _portalToLVL.SetActive(false);
        _coroutine = StartCoroutine(WaveDelayCoroutine());
    }

    private void OnEnable()
    {
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
        mob.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
        mob.SetActive(true);
        _activEnemies.Add(mob);
    }

    private void SpawnBoss(string tag)
    {
        GameObject mob = ObjectPooler.instance.TakeFromPool(tag);
        mob.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;// mudar para boss sopt
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
                lvl++;
                StopMyCoroutine(_coroutine);
                Debug.Log("entrou aki");
                _portalToLVL.SetActive(true);
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
