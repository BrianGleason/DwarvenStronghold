using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipScript : MonoBehaviour
{
    public TextMesh text;
    public SpawnEnemy spawnEnemyScript;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
        text.text = "";
        spawnEnemyScript = FindObjectOfType<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnEnemyScript)
        {
            return;
        }
        if (spawnEnemyScript.countDown > 0 && text.text == "")
        {
            text.text = "Hit 'Enter' to skip ahead to next wave";
        }
        else if (spawnEnemyScript.countDown <= 0 && text.text == "Hit 'Enter' to skip ahead to next wave")
        {
            text.text = "";
        }
    }
}
