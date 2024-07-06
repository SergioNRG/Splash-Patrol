
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int plusenemies = 2;
    public int numbenemies = 0;
    public int enemiestospawn = 0;

    [SerializeField] Transform[] spawnpoints;

    private int wavecount = 0;

    public float timetonextwave, timelapsed;
    public bool activatetimmer;

    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    private void Start()
    {
        timelapsed = 0;
        timetonextwave = 3;
        //WaveSpawner();
    }
    private void Update()
    {
        if (activatetimmer)
        {
            timelapsed += Time.deltaTime;
        }
        else
        {
            timelapsed = 0;
        }

        CheckEnemies();
    }
    public void WaveSpawner()
    {
        enemiestospawn += plusenemies;

        for (int i = 1; i <= enemiestospawn; i++)
        {
            SpawnEnemy();
        }

        numbenemies = enemiestospawn;
        wavecount++;
        Debug.Log(wavecount);
        if (wavecount % 10 == 0)
        {
            Debug.Log("SPAWN BOSS");
        }
        //Wavetxt.text = wavecount.ToString();

    }

    private void CheckEnemies()
    {
        if (numbenemies == 0)
        {

            activatetimmer = true;
            if (timelapsed >= timetonextwave)
            {
                WaveSpawner();
                activatetimmer = false;
            }

        }
    }

    private void SpawnEnemy()
    {
        List<string> keyList = new List<string>(ObjectPooler.instance.PoolDictionary.Keys);

        System.Random rand = new System.Random();
        string tag = keyList[rand.Next(keyList.Count-1)];
        GameObject mob = ObjectPooler.instance.TakeFromPool(tag);
        mob.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
        mob.SetActive(true);
    }

    private void SpawnBoss(string tag)
    {
        GameObject mob = ObjectPooler.instance.TakeFromPool(tag);
        mob.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;// mudar para boss sopt
        mob.SetActive(true);
    }
}
