using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
        StartCoroutine(WaveOne());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentWave);
    }

    private Vector2 spawnPos()
    {
        return new Vector2(11f, Random.Range(-4f, 4f));
    }

    private void SpawnEnemyOfType(int enemyType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemyPrefabs[enemyType], spawnPos(), enemyPrefabs[enemyType].transform.rotation);
        }
    }

    IEnumerator WaveOne()
    {
        SpawnEnemyOfType(0, 2);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 4);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 4);
        yield return new WaitForSeconds(10);

        while (FindObjectsOfType<EnemyMovement>().Length > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(30);
        
        StartCoroutine(WaveTwo());
    }

    IEnumerator WaveTwo()
    {
        currentWave = 2;

        SpawnEnemyOfType(0, 3);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 5);
        SpawnEnemyOfType(1, 1);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 7);
        SpawnEnemyOfType(1, 2);
        yield return new WaitForSeconds(10);

        while (FindObjectsOfType<EnemyMovement>().Length > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(30);

        StartCoroutine(WaveThree());
    }

    IEnumerator WaveThree()
    {
        currentWave = 3;
        // SPAWN HERE
        // 20 basic enemies 5 + 6 + 9
        // 6 base enemies 1 + 2 + 3
        // ? dash enemies
        SpawnEnemyOfType(0, 5);
        SpawnEnemyOfType(1, 1);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 6);
        SpawnEnemyOfType(1, 2);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 9);
        SpawnEnemyOfType(1, 3);
        yield return new WaitForSeconds(10);


        while (FindObjectsOfType<EnemyMovement>().Length > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(30);

        StartCoroutine(Boss());
    }

    IEnumerator Boss()
    {
        currentWave = 4;
        // Spawn boss
        yield return null;
    }
}