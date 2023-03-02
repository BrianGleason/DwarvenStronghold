using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{
    public float range;
    public Vector2 moveBackSpot;
    public bool enemyClose;
    public float attackOffset = 0.01f;
    public float attackCooldownDuration = 2f;
    RangedProjectile rangedProjectileScript;
    Rigidbody2D rb;
    Transform closestEnemy;
    private bool attackOnCooldown = true;

    public EnemyMovement[] enemies;
    public DashEnemy[] enemiesDash;
    public SpawnEnemy waveStats;
    public Lich boss;
    
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyClose = false;
        rangedProjectileScript = GetComponent<RangedProjectile>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(attackCooldown());

        waveStats = FindObjectOfType<SpawnEnemy>();
        animator = GetComponent<Animator>();

        System.Random rand = new System.Random();
        range = (float)(rand.NextDouble() * 5 + 5);
        moveBackSpot = new Vector2((float)(rand.NextDouble() * 3 - 4), (float)(rand.NextDouble() * 5 - 2.5f));
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = FindClosestEnemy().Item1;
        if (FindClosestEnemy().Item2 < range)
        {
            enemyClose = true;
        }
        else
        {
            enemyClose = false;
        }

        animator.SetBool("Moving", !enemyClose && closestEnemy != null);

        if (!enemyClose) {
            Move();
        }   else {
            if (!attackOnCooldown)
            {
                Attack();
                StartCoroutine(attackCooldown());
            }
        }

    }

    private void Move()
    {
        if (closestEnemy != null)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, closestEnemy.position, Time.deltaTime);
            if (this.transform.position.x > closestEnemy.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        else
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

    private void Attack()
    {
        animator.SetTrigger("attackTrigger");
        attackOnCooldown = true;
        StartCoroutine(fire());
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(0.3f);
        rangedProjectileScript.Fire(this.transform.position, closestEnemy.position, attackOffset);
    }

    (Transform, float) FindClosestEnemy()
    {
        enemies = FindObjectsOfType<EnemyMovement>();
        enemiesDash = FindObjectsOfType<DashEnemy>();
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (EnemyMovement enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closest = enemy.transform;
                closestDistance = distance;
            }
        }
        foreach (DashEnemy enemy in enemiesDash)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closest = enemy.transform;
                closestDistance = distance;
            }
        }
        if (waveStats.currentWave == 4)
        {
            boss = FindObjectOfType<Lich>();
            float distanceB = Vector2.Distance(transform.position, boss.transform.position);
            if (distanceB < closestDistance)
            {
                closest = boss.transform;
                closestDistance = distanceB;
            }
        }

        return (closest, closestDistance);
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownDuration);
        attackOnCooldown = false;
    }
}