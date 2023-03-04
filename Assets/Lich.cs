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
    public float homingCooldownDuration = 20;
    public GameObject homingProjectilePrefab;

    public Transform baseTransform = null;
    public float[] tombstonePositionYOptions = { -3f, 3f };
    public Slider healthBar;

    public float lichSpeed = 0.25f;
    public Animator animator;
    public bool stopped = false;

    public Camera camera;
    public float shakeDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        setBaseTransform();
        animator = GetComponent<Animator>();
        camera = Camera.main;


    }

    // Update is called once per frame
    void Update()
    {
        if (!tombstoneCooldown && !stopped)
        {
            tombstoneCooldown = true;
            StartCoroutine(shaking());
            StartCoroutine(SummonTombstones());
        }
        if (!wavyCooldown)
        {
            wavyCooldown = true;
            StartCoroutine(FireWavy());
        }
        if (!homingCooldown && !stopped)
        {
            homingCooldown = true;
            StartCoroutine(FireHoming());
        }
        if (!stopped && baseTransform)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, baseTransform.position, Time.deltaTime * lichSpeed);
        }

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
        stopped = true;
        // first cast
        animator.SetBool("CastingHoming", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("CastingHoming", false);
        Instantiate(homingProjectilePrefab, transform.position + new Vector3(
            -1f,
            1f,
            0f),
            Quaternion.identity);
        yield return new WaitForSeconds(0.75f);
        // second cast
        animator.SetBool("CastingHoming", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("CastingHoming", false);
        Instantiate(homingProjectilePrefab, transform.position + new Vector3(
            -1f,
            0f,
            0f),
            Quaternion.identity);
        yield return new WaitForSeconds(0.75f);
        // third cast
        animator.SetBool("CastingHoming", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("CastingHoming", false);
        Instantiate(homingProjectilePrefab, transform.position + new Vector3(
            -1f,
            -1f,
            0f),
            Quaternion.identity);
        stopped = false;
        // cooldown timer
        yield return new WaitForSeconds(homingCooldownDuration);
        homingCooldown = false;
    }

    IEnumerator SummonTombstones()
    {
        stopped = true;
        animator.SetBool("CastingSummon", true);
        yield return new WaitForSeconds(0.75f);
        animator.SetBool("CastingSummon", false);
        Instantiate(tombStonePrefab, new Vector3(
            Random.Range(-2.5f, 7f),
            Random.Range(-3f, 3f),
            0f),
            Quaternion.identity);
        stopped = false;
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

    IEnumerator shaking()
    {
        yield return new WaitForSeconds(0.75f);
        Vector3 startPosition = camera.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            Vector3 randomOffset = Random.insideUnitSphere;
            camera.transform.position = startPosition + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
            yield return null;
        }
        camera.transform.position = startPosition;
    }
}
