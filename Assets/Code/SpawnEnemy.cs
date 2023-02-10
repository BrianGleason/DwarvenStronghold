using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    private int enemyCount;
    public int waveNumber = 2;
    private int curwave;

    public float timeBetweenEnemySpawn;
    public float timeBetweenWaves;
    public int totalWaves;

    bool spawningWave;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWave(waveNumber));
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
            Instantiate(enemyPrefab, pos, enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(timeBetweenEnemySpawn); //We wait here to give a bit of time between each enemy spawn
        }
        spawningWave = false;
    }
}