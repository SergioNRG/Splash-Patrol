using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] spawnpoints;
    [SerializeField] TextMeshProUGUI Wavetxt;

    public int plusenemies = 2;
    public int numbenemies = 0;
    public int enemiestospawn=0;

    private int wavecount = 0;
    public float timetonextwave,timelapsed;
    public bool activatetimmer;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        timelapsed = 0;
        timetonextwave = 3;
        WaveSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        if (activatetimmer)
        {
            timelapsed += Time.deltaTime;
        }else
        {
            timelapsed = 0;
        }


        if (numbenemies == 0 )
        {
            
            activatetimmer = true;
            if(timelapsed >= timetonextwave)
            {
                WaveSpawner();
                activatetimmer = false;
            }
            
        }
    }

    private void SpawnEnemy()
    {
        enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);
        enemy.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
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
        Wavetxt.text = wavecount.ToString();
        
    }
}
