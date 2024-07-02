
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
        //Wavetxt.text = wavecount.ToString();

    }

    private void SpawnEnemy()
    {
        GameObject mob = ObjectPooler.instance.TakeFromPool("GolemBoss");
        mob.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
        mob.SetActive(true);
    }
}
