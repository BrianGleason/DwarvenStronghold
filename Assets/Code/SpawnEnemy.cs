using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private int enemyCount;
    public int waveNumber = 4;
    private int curwave;

    public float timeBetweenEnemySpawn;
    public float timeBetweenWaves;
    public int totalWaves;

    bool spawningWave;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWave(waveNumber));

        /*StartCoroutine(WaveOne());
        StartCoroutine(WaveTwo());
        StartCoroutine(WaveThree());
        StartCoroutine(WaveFour());
        StartCoroutine(Boss());*/
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyCount == 0 && !spawningWave && curwave!=totalWaves)
        {
            waveNumber++;
            curwave++;
            StartCoroutine(SpawnEnemyWave(waveNumber));
        }
    }

    IEnumerator SpawnEnemyWave(int enemiesToSpawn)
    {
        spawningWave = true;
        yield return new WaitForSeconds(timeBetweenWaves); //We wait here to pause between wave spawning
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float yloc = Random.Range(-4f, 4f);
            Vector2 pos = new Vector2(11f, yloc);
            GameObject enemySpawn = Instantiate(enemyPrefabs[0], pos, enemyPrefabs[0].transform.rotation);


            yield return new WaitForSeconds(timeBetweenEnemySpawn); //We wait here to give a bit of time between each enemy spawn
        }

        for (int i = 0; i < enemiesToSpawn - 1; i++)
        {
            float yloc = Random.Range(-4f, 4f);
            Vector2 pos = new Vector2(11f, yloc);
            GameObject enemySpawn = Instantiate(enemyPrefabs[1], pos, enemyPrefabs[1].transform.rotation);


            yield return new WaitForSeconds(timeBetweenEnemySpawn); //We wait here to give a bit of time between each enemy spawn
        }
        spawningWave = false;
    }


    private Vector2 spawnPos()
    {
        return new Vector2(11f, Random.Range(-4f, 4f));
    }

    IEnumerator WaveOne()
    {
        yield return null;
    }

    IEnumerator WaveTwo()
    {
        yield return null;
    }

    IEnumerator WaveThree()
    {
        yield return null;
    }

    IEnumerator WaveFour()
    {
        yield return null;
    }

    IEnumerator Boss()
    {
        return null;
    }
}