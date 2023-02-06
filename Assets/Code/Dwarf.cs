using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{
    bool enemyClose;
    int hitpoint;
    int atk;
    int speed = 1;

    public EnemyMovement[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemyClose = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            Attack();
        }


    }

    private void Move()
    {
        Transform closestEnemy = FindClosestEnemy().Item1;
        if (closestEnemy != null)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, closestEnemy.position, speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        return;
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
}