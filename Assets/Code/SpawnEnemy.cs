using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public int currentWave;
    public int countDown;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
        StartCoroutine(WaveOne());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector2 spawnPos()
    {
        return new Vector2(9.3f, Random.Range(-4f, 4f));
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
        SpawnEnemyOfType(2, 1);
        yield return new WaitForSeconds(10);


        while (FindObjectsOfType<EnemyMovement>().Length > 0)
        {
            yield return null;
        }

        countDown = 30;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }

        StartCoroutine(WaveTwo());
    }

    IEnumerator WaveTwo()
    {
        currentWave = 2;

        SpawnEnemyOfType(0, 3);
        SpawnEnemyOfType(2, 2);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 5);
        SpawnEnemyOfType(1, 1);
        SpawnEnemyOfType(2, 4);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 7);
        SpawnEnemyOfType(1, 2);
        SpawnEnemyOfType(2, 4);
        yield return new WaitForSeconds(10);

        while (FindObjectsOfType<EnemyMovement>().Length > 0)
        {
            yield return null;
        }

        countDown = 30;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }
        
        StartCoroutine(WaveThree());
    }

    IEnumerator WaveThree()
    {
        currentWave = 3;
        // SPAWN HERE
        // 20 basic enemies 5 + 6 + 9
        // 6 base enemies 1 + 2 + 3
        // 9 archers 3 + 3 + 3
        // DASH ENEMIES
        SpawnEnemyOfType(0, 5);
        SpawnEnemyOfType(1, 1);
        SpawnEnemyOfType(2, 3);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 6);
        SpawnEnemyOfType(1, 2);
        SpawnEnemyOfType(2, 3);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 9);
        SpawnEnemyOfType(1, 3);
        SpawnEnemyOfType(2, 3);
        yield return new WaitForSeconds(10);


        while (FindObjectsOfType<EnemyMovement>().Length > 0)
        {
            yield return null;
        }

        countDown = 30;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }

        StartCoroutine(Boss());
    }

    IEnumerator Boss()
    {
        currentWave = 4;
        // Spawn boss
        yield return null;
    }
}