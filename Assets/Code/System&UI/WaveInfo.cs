using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    public TextMesh Text;
    public SpawnEnemy waveStats;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMesh>();
        waveStats = FindObjectOfType<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Current wave: " + waveStats.currentWave + "\nNext wave in ...";
        Text.fontSize = 20;
    }
}
