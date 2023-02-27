using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lich : MonoBehaviour
{

    public bool tombstoneCooldown = false;
    public int tombstoneCooldownDuration = 20;
    public GameObject tombStonePrefab;

    public bool wavyCooldown = false;
    public float wavyCooldownDuration = 0.25f;
    public GameObject WavyProjectilePrefab;

    public bool homingCooldown = false;
    public float homingCooldownDuration = 10;
    public GameObject homingProjectilePrefab;

    public Transform baseTransform = null;
    public float[] tombstonePositionYOptions = { -3f, 3f };
    public Slider healthBar;

    public float lichSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        setBaseTransform();

    }

    // Update is called once per frame
    void Update()
    {
        if (!tombstoneCooldown)
        {
            tombstoneCooldown = true;
            StartCoroutine(SummonTombstones());
        }
        if (!wavyCooldown)
        {
            wavyCooldown = true;
            StartCoroutine(FireWavy());
        }
        if (!homingCooldown)
        {
            homingCooldown = true;
            StartCoroutine(FireHoming());
        }
        transform.position = Vector2.MoveTowards(this.transform.position, baseTransform.position, Time.deltaTime * lichSpeed);

    }

    IEnumerator FireWavy()
    {
        Instantiate(WavyProjectilePrefab, new Vector3(
            10f,
            Random.Range(-5f, 3.5f),
            0f),
            Quaternion.identity);
        yield return new WaitForSeconds(wavyCooldownDuration);
        wavyCooldown = false;
    }

    IEnumerator FireHoming()
    {
        Instantiate(homingProjectilePrefab, transform.position + new Vector3(
            -1f,
            0f,
            0f),
            Quaternion.identity);
        yield return new WaitForSeconds(homingCooldownDuration);
        homingCooldown = false;
    }

    IEnumerator SummonTombstones()
    {
        Instantiate(tombStonePrefab, new Vector3(
            Random.Range(-2.5f, 7f),
            tombstonePositionYOptions[Random.Range(0, tombstonePositionYOptions.Length)],
            0f),
            Quaternion.identity);
        yield return new WaitForSeconds(tombstoneCooldownDuration);
        tombstoneCooldown = false;
    }

    void setBaseTransform()
    {
        var gameObjectArray = FindObjectsOfType<GameObject>();

        foreach (GameObject baseCandidate in gameObjectArray)
        {
            if (baseCandidate.name == "Base")
            {
                baseTransform = baseCandidate.transform;
                return;
            }
        }
    }
}
