using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombSpawner : MonoBehaviour
{

    public GameObject meleeSkelePrefab;
    public int spawnerCooldown = 5;
    private bool onCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCooldown());
    }

    private void SpawnEnemyOfType(GameObject type, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(type, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!onCooldown)
        {
            onCooldown = true;
            SpawnEnemyOfType(meleeSkelePrefab, 1);
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnerCooldown);
        onCooldown = false;
    }
}
