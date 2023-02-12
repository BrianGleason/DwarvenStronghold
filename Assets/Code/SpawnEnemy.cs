using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    //public Sprite healthBarSprite;


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
            GameObject enemySpawn = Instantiate(enemyPrefab, pos, enemyPrefab.transform.rotation);

            // Health bar
            //GameObject healthBar = new GameObject("Health Bar");
            //healthBar.transform.SetParent(enemySpawn.transform);
            //healthBar.transform.localPosition = new Vector3(0, 1, 0);
            //healthBar.transform.localScale = new Vector3(1, 1, 1);
            //healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
            //Image barImage = healthBar.AddComponent<Image>();
            //barImage.sprite = healthBarSprite;
            //barImage.color = Color.red;
            //HealthBar healthBarScript = enemySpawn.AddComponent<HealthBar>();
            //healthBarScript.healthBar = healthBar.transform;

            yield return new WaitForSeconds(timeBetweenEnemySpawn); //We wait here to give a bit of time between each enemy spawn
        }
        spawningWave = false;
    }
}