using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public int currentWave;
    public int countDown;
    public bool waitingForNext;

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
        return new Vector2(9.3f, Random.Range(-4f, 2f));
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
        SpawnEnemyOfType(3, 1);
        SpawnEnemyOfType(0, 2);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 2);
        yield return new WaitForSeconds(2);
        SpawnEnemyOfType(0, 2);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 2);
        yield return new WaitForSeconds(2);
        SpawnEnemyOfType(0, 2);
        SpawnEnemyOfType(2, 1);
        yield return new WaitForSeconds(10);

        while (FindObjectsOfType<EnemyMovement>().Length + FindObjectsOfType<DashEnemy>().Length > 0)
        {
            yield return null;
        }

        waitingForNext = true;
        countDown = 30;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }

        waitingForNext = false;
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
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 1);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 1);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 1);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 1);
        yield return new WaitForSeconds(10);

        while (FindObjectsOfType<EnemyMovement>().Length + FindObjectsOfType<DashEnemy>().Length > 0)
        {
            yield return null;
        }

        waitingForNext = true;
        countDown = 30;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }

        waitingForNext = false;
        StartCoroutine(WaveThree());
    }

    IEnumerator WaveThree()
    {
        currentWave = 3;
        // SPAWN HERE
        // 20 basic enemies 5 + 6 + 9
        // 6 base enemies 1 + 2 + 3
        // 9 archers 3 + 3 + 3
        // 3 DASH ENEMIES
        SpawnEnemyOfType(0, 5);
        SpawnEnemyOfType(1, 1);
        SpawnEnemyOfType(2, 1);
        SpawnEnemyOfType(3, 1);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 2);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 3);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(0, 3);
        SpawnEnemyOfType(1, 2);
        SpawnEnemyOfType(2, 2);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 1);
        SpawnEnemyOfType(3, 1);
        yield return new WaitForSeconds(10);
        SpawnEnemyOfType(0, 4);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(0, 5);
        SpawnEnemyOfType(1, 3);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 1);
        yield return new WaitForSeconds(1);
        SpawnEnemyOfType(2, 2);
        SpawnEnemyOfType(3, 1);
        yield return new WaitForSeconds(10);


        while (FindObjectsOfType<EnemyMovement>().Length + FindObjectsOfType<DashEnemy>().Length > 0)
        {
            yield return null;
        }

        waitingForNext = true;
        countDown = 30;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }

        waitingForNext = false;
        StartCoroutine(Boss());
    }

    IEnumerator Boss()
    {
        currentWave = 4;
        // Spawn boss
        Instantiate(enemyPrefabs[4], new Vector2(0.4f, 1), Quaternion.identity);

        yield return null;
    }
}