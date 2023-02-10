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

    // Start is called before the first frame update
    void Start()
    {
        enemyClose = false;
        rangedProjectileScript = GetComponent<RangedProjectile>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(attackCooldown());
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
    }

    private void Attack()
    {
        attackOnCooldown = true;
        rangedProjectileScript.Fire(this.transform.position, closestEnemy.position, attackOffset);
    }

    (Transform, float) FindClosestEnemy()
    {
        enemies = FindObjectsOfType<EnemyMovement>();
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
        return (closest, closestDistance);
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownDuration);
        attackOnCooldown = false;
    }
}