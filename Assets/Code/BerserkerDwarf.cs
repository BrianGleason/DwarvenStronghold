using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerDwarf : MonoBehaviour
{
    bool enemyWithinAttackRange = false;
    int speed = 1;
    public float attackOffset = 0.01f;
    public float dashCooldownDuration = 2f;
    public float dashDuration = 0.5f;
    public float dashChannelDuration = 0.5f;
    public bool isDashing = false;
    public float dashSpeed = 50f;
    public int dashDamage = 20;

    Transform closestEnemyTransform;
    Vector3 dashEndLocation;
    Health closestEnemyHealthScript;

    private bool dashOnCooldown = true;

    public EnemyMovement[] enemies;
    public GameObject meleeAttack;

    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        StartCoroutine(dashCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, dashEndLocation, dashSpeed * Time.deltaTime);
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
            //animator.SetBool("moving", true);
        }
        else if (!dashOnCooldown)
        {
            Dash();
            isDashing = true;
            dashEndLocation = this.transform.position + ((closestEnemyTransform.position - this.transform.position).normalized * 3);
            StartCoroutine(dashCooldown());
            //animator.SetBool("moving", false);
        }
        else
        {
            //animator.SetBool("moving", false);
        }
        
    }

    private void Move()
    {
        if (closestEnemyTransform != null)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, closestEnemyTransform.position, speed * Time.deltaTime);
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
                if (distance < minDistance)
                {
                    maxClosestEnemyTransform = enemyCandidate.transform;
                    minDistance = distance;
                    healthScript = candidateHealthScript;
                }
            }
        }
        closestEnemyTransform = maxClosestEnemyTransform;
        closestEnemyHealthScript = healthScript;
        Debug.Log("found new enemy transform");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDashing)
        {
            Health target = col.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(dashDamage, transform.position);
            }
        }
    }

    private void Dash()
    {
       // animator.SetTrigger("Heal");
        dashOnCooldown = true;
        StartCoroutine(dash());
    }

    IEnumerator dash()
    {
        yield return new WaitForSeconds(dashChannelDuration);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownDuration);
        dashOnCooldown = false;
    }
}