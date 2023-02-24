using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    public TextMesh Text;
    public SpawnEnemy waveStats;

    public string cdtext;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMesh>();
        waveStats = FindObjectOfType<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveStats.waitingForNext)
        {
            cdtext = "...";
        }
        else
        {
            cdtext = waveStats.countDown.ToString();
        }
        Text.text = "Current wave: " + waveStats.currentWave + "\nNext wave in: " + cdtext;

        Text.fontSize = 20;
    }

}
