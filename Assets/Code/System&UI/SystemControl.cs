using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemControl : MonoBehaviour
{
    public int gold;
    public static SystemControl instance;
    private SpawnEnemy waveStats;

    // Start is called before the first frame update
    void Start()
    {
        gold = 30;
        waveStats = FindObjectOfType<SpawnEnemy>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveStats.currentWave == 4
            && FindObjectsOfType<EnemyMovement>().Length + FindObjectsOfType<DashEnemy>().Length == 0
            && FindObjectOfType<Lich>() == null)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void AddGold(int gain)
    {
        gold += gain;
    }

    public void UseGold(int loss)
    {
        gold -= loss;
    }
}
