using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HealerDwarf : MonoBehaviour
{
    bool allyWithinHealRange = false;
    int speed = 1;
    public float attackOffset = 0.01f;
    public float healCooldownDuration = 2f;
    public float healChannelDuration = 0.5f;
    public int healAmount = 5;
    public GameObject healTextPrefab;

    public Vector2 moveBackSpot;

    Transform closestDamagedAlly;
    Health closestDamagedAllyHealthScript;
    float distanceFromClosestDamagedAlly = float.MinValue;

    private bool healOnCooldown = true;

    public EnemyMovement[] enemies;
    public Animator animator;
    public SpawnEnemy waveStats;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(healCooldown());
        waveStats = FindObjectOfType<SpawnEnemy>();

        System.Random rand = new System.Random();
        moveBackSpot = new Vector2((float)(rand.NextDouble() * 2 - 5), (float)(rand.NextDouble() * 5 - 2.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!closestDamagedAlly || !closestDamagedAllyHealthScript || closestDamagedAllyHealthScript.health >= closestDamagedAllyHealthScript.maxHP)
        {
            closestDamagedAlly = null;
            closestDamagedAllyHealthScript = null;
            distanceFromClosestDamagedAlly = float.MinValue;
            FindClosestDamagedAlly();
        }
        if (closestDamagedAlly)
        {
            distanceFromClosestDamagedAlly = Vector2.Distance(transform.position, closestDamagedAlly.position);
            allyWithinHealRange = (distanceFromClosestDamagedAlly < 3) && (transform.position.x + 1 < closestDamagedAlly.position.x);
            if (!allyWithinHealRange)
            {
                Move();
                animator.SetBool("moving", true);
            }
            else if (!healOnCooldown)
            {
                Heal();
                StartCoroutine(healCooldown());
                animator.SetBool("moving", false);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }

        if (waveStats.waitingForNext)
        {
            if (this.transform.position.x > moveBackSpot[0])
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, moveBackSpot, Time.deltaTime);
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void Move()
    {
        if (closestDamagedAlly != null)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, closestDamagedAlly.position + new Vector3(-2f, 0f, 0f), speed * Time.deltaTime);
        }
    }

    private void FindClosestDamagedAlly()
    {
        var gameObjectArray = FindObjectsOfType<GameObject>();
        int allyLayer = LayerMask.NameToLayer("Ally");
        float minDistance = float.MaxValue;
        Transform closestDamagedAllyTransform = null;
        Health healthScript = null;

        foreach (GameObject allyCandidate in gameObjectArray)
        {
            if (allyCandidate.layer != allyLayer) continue;

            Health candidateHealthScript = allyCandidate.GetComponent<Health>();
            if (candidateHealthScript && candidateHealthScript.health < candidateHealthScript.maxHP)
            {
                float distance = Vector2.Distance(transform.position, allyCandidate.transform.position);
                if (distance < minDistance)
                {
                    closestDamagedAllyTransform = allyCandidate.transform;
                    minDistance = distance;
                    healthScript = candidateHealthScript;
                }
            }
        }
        closestDamagedAlly = closestDamagedAllyTransform;
        closestDamagedAllyHealthScript = healthScript;
    }

    private void Heal()
    {
        animator.SetTrigger("Heal");
        healOnCooldown = true;
        StartCoroutine(heal());
    }

    IEnumerator heal()
    {
        yield return new WaitForSeconds(healChannelDuration);
        if (!closestDamagedAlly || !closestDamagedAllyHealthScript || closestDamagedAllyHealthScript.health >= closestDamagedAllyHealthScript.maxHP) yield break;
        closestDamagedAllyHealthScript.HealDamage(healAmount);
        var healText = Instantiate(healTextPrefab, closestDamagedAlly.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        TextMesh txt = healText.transform.GetComponent<TextMesh>();
        txt.fontSize = 18;
        txt.text = $"+{healAmount}hp";
        Destroy(healText, 1f);
    }

    IEnumerator healCooldown()
    {
        yield return new WaitForSeconds(healCooldownDuration);
        healOnCooldown = false;
    }
}