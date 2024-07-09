
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int plusenemies = 2;
    public int enemiestospawn = 0;

    [SerializeField] Transform[] spawnpoints;
    public List<GameObject> _activEnemies = new List<GameObject>();
    private int wavecount = 0;
    public float waveDelay;

    private Coroutine _coroutine;
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
        _coroutine = StartCoroutine(WaveDelayCoroutine());
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
        Debug.Log("WAVE " + wavecount);
        if (wavecount % 10 == 0)
        {
            Debug.Log("SPAWN BOSS");
            SpawnBoss("GolemBoss");
        }

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
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
            _coroutine = StartCoroutine(WaveDelayCoroutine());
            Debug.Log("entrou coroutina");
        }
    }
}
