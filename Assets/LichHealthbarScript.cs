using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichHealthbarScript : MonoBehaviour
{
    GameObject lich; 
    // Start is called before the first frame update
    void Start()
    {
        lich = GameObject.FindWithTag("Lich");
    }

    // Update is called once per frame
    void Update()
    {
        if (!lich)
        {
            DestroyAllEnemies();
            Destroy(gameObject);
        }
    }

    private void DestroyAllEnemies()
    {
        var gameObjectArray = FindObjectsOfType<GameObject>();
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        float minDistance = float.MaxValue;
        Transform maxClosestEnemyTransform = null;
        Health healthScript = null;

        foreach (GameObject enemyCandidate in gameObjectArray)
        {
            if (enemyCandidate.layer != enemyLayer) continue;

            Destroy(enemyCandidate);
        }
    }
}
