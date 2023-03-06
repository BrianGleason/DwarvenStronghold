using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerDwarf : MonoBehaviour
{
    bool enemyWithinAttackRange = false;
    int speed = 1;
    public float attackOffset = 0.01f;
    public float dashCooldownDuration = 2f;
    public float dashDuration = 3.0f;
    public float dashChannelDuration = 0.75f;
    public bool isDashing = false;
    public float dashSpeed = 3f;
    public int dashDamage = 10;

    public Vector2 moveBackSpot;

    Transform closestEnemyTransform;
    Vector3 dashEndLocation;
    Health closestEnemyHealthScript;

    private bool dashOnCooldown = true;
    Health berserkerHealthScript;

    public EnemyMovement[] enemies;
    public GameObject meleeAttack;
    public SpawnEnemy waveStats;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(dashCooldown());
        berserkerHealthScript = GetComponent<Health>();


        waveStats = FindObjectOfType<SpawnEnemy>();
        System.Random rand = new System.Random();
        moveBackSpot = new Vector2((float)(rand.NextDouble() * 2 - 1.5f), (float)(rand.NextDouble() * 5 - 2.5f));
    }

    // Update is called once per frame
    void Update()
    {
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

        if (isDashing)
        {
            transform.position = Vector2.MoveTowards(this.transform.position,
                new Vector3(Mathf.Min(dashEndLocation.x, 8.5f), dashEndLocation.y, dashEndLocation.z), dashSpeed * Time.deltaTime);
            return;
        }
        if (!closestEnemyTransform)
        {
            FindClosestEnemy();
            return;
        }
        else if (closestEnemyHealthScript)
        {
            if (closestEnemyHealthScript.health <= 0)
            {
                FindClosestEnemy();
                return;
            }
        }
        
        float distanceFromClosestEnemy = Vector2.Distance(transform.position, closestEnemyTransform.position);
        enemyWithinAttackRange = distanceFromClosestEnemy < 2;
        if (!enemyWithinAttackRange)
        {
            Move();
            animator.SetBool("isMoving", true);
        }
        else if (!dashOnCooldown)
        {
            dashEndLocation = this.transform.position + ((closestEnemyTransform.position - this.transform.position).normalized * 3);
            Dash();
            StartCoroutine(dashCooldown());
            animator.SetBool("isMoving", false);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void Move()
    {
        if (closestEnemyTransform != null)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, 
                new Vector3(Mathf.Min(closestEnemyTransform.position.x, 8f), closestEnemyTransform.position.y, closestEnemyTransform.position.z), 
                speed * Time.deltaTime);
        }
    }

    private void FindClosestEnemy()
    {
        var gameObjectArray = FindObjectsOfType<GameObject>();
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        float minDistance = float.MaxValue;
        Transform maxClosestEnemyTransform = null;
        Health healthScript = null;

        foreach (GameObject enemyCandidate in gameObjectArray)
        {
            if (enemyCandidate.layer != enemyLayer) continue;

            Health candidateHealthScript = enemyCandidate.GetComponent<Health>();
            if (candidateHealthScript && candidateHealthScript.health > 0)
            {
                float distance = Vector2.Distance(transform.position, enemyCandidate.transform.position);
                if (distance < minDistance && enemyCandidate.tag != "Lobster")
                {
                    maxClosestEnemyTransform = enemyCandidate.transform;
                    minDistance = distance;
                    healthScript = candidateHealthScript;
                }
            }
        }
        closestEnemyTransform = maxClosestEnemyTransform;
        closestEnemyHealthScript = healthScript;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDashing)
        {
            Health target = col.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(dashDamage, transform.position);
                if (berserkerHealthScript.health < berserkerHealthScript.maxHP)
                {
                    berserkerHealthScript.health += 2;
                }
            }
        }
    }

    private void Dash()
    {
        dashOnCooldown = true;
        StartCoroutine(dash());
    }

    IEnumerator dash()
    {
        yield return new WaitForSeconds(dashChannelDuration);
        isDashing = true;
        animator.SetBool("dashTrigger", true);
        yield return new WaitForSeconds(dashDuration);
        animator.SetBool("dashTrigger", false);
        isDashing = false;
    }

    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownDuration);
        dashOnCooldown = false;
    }
}