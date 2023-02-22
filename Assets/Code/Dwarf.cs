using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{
    bool enemyClose;
    int hitpoint;
    int atk;
    int speed = 1;
    public float attackOffset = 0.01f;
    public float attackCooldownDuration = 2f;
    RangedProjectile rangedProjectileScript;
    Rigidbody2D rb;
    Transform closestEnemy;
    private bool attackOnCooldown = true;

    public EnemyMovement[] enemies;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyClose = false;
        rangedProjectileScript = GetComponent<RangedProjectile>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(attackCooldown());

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = FindClosestEnemy().Item1;
        if (FindClosestEnemy().Item2 < 3)
        {
            enemyClose = true;
        }
        else
        {
            enemyClose = false;
        }

        animator.SetBool("Moving", !enemyClose);

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
            transform.position = Vector2.MoveTowards(this.transform.position, closestEnemy.position, speed * Time.deltaTime);
        }

        if (transform.position.x >= 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + Vector3.left * 2, speed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
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
        Transform closest = null;
        float closestDistance = 5f;
        foreach (EnemyMovement enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closest = enemy.transform;
                closestDistance = distance;
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