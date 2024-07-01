using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] rocks;
    public GameObject[] enemies;
    private Vector2 screenBounds;
   // private GameManagerScript gameManagerScript;
    Coroutine spEnemies;
    Coroutine spMeteors;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
       // gameManagerScript = GameObject.FindObjectOfType<GameManagerScript>();
        spMeteors = StartCoroutine(spawnMeteor());
        spEnemies = StartCoroutine(spawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
       /* if (gameManagerScript.bossisdead == true )
        {
            StopCoroutine(spEnemies);
            StopCoroutine(spMeteors);
        }*/
    }


    public IEnumerator spawnMeteor()
    {
        float x = Random.Range(0.5f, 0.8f);
        float y = Random.Range(0.5f, 0.8f);
        float z = Random.Range(0.5f, 0.8f);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width - 1), Random.Range(Screen.height * 2, (Screen.height * 2) + 2), (Camera.main.transform.position.z + 60)));

        //GameObject rock = Instantiate(rocks[Random.Range(0,rocks.Length)], pos, Quaternion.identity);

        GameObject rock = ObjectPooler.instance.TakeFromPool(SelectRock());

        if ( rock != null)
        {
            rock.transform.position = new Vector3(pos.x, pos.y, 0);
            rock.transform.localScale = new Vector3(x, y, z);
            rock.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        yield return spawnMeteor();
    }


    public IEnumerator spawnEnemies()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width - 1), Random.Range(Screen.height * 2, (Screen.height * 2) + 2), (Camera.main.transform.position.z + 60)));
       // GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], pos, Quaternion.Euler(90,90,90));
       
        GameObject enemy = ObjectPooler.instance.TakeFromPool(SelectEnemy());
        if (enemy != null)
        {
            enemy.transform.rotation = Quaternion.Euler(90, 90, 90);
            enemy.transform.position = new Vector3(pos.x, pos.y, 0);
            enemy.transform.localScale = new Vector3(3, 3, 3);
            enemy.SetActive(true);
        }
       

        yield return new WaitForSeconds(2f);
        yield return spawnEnemies();
    }

    public string SelectRock()
    {
        int rockType = Random.Range(0, rocks.Length);

        switch (rockType)
        {
            default:
                return null;
            case 0:
                return "rock1";

            case 1:
                return "rock2";

            case 2:
                return "rock3";

        }
    }

    public string SelectEnemy()
    {
        int enemyType = Random.Range(0, enemies.Length);

        switch (enemyType)
        {
            default:
                return null;
            case 0:
                return "enemy1";

            case 1:
                return "enemy2";
        }
    }
}
