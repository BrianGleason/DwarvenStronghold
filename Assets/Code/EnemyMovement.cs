using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.Timeline;

public class EnemyMovement : MonoBehaviour
{
    public float movespeed;
    [SerializeField] float range;
    Rigidbody2D rb;
    Transform target;
    Vector2 direction;
    RangedProjectile rangedProjectileScript;
    SecondaryRanged SecondaryProjectile;
    public float attackOffset = 1f;
    public GameObject meleeAttackPrefab;
    private bool isAttacking = false;
    public float attackOffsetScalar = 1.5f;
    public float attackSizeScalar = 1f;
    public float attackDuration = 0.1f;
    public float attackCooldownDuration = 2.50f;
    public string targ;
    public int attackDamage = 10;
    public float knockback = 1;
    public bool stunned = false;
    public bool selfDestruct;
    public bool ranged;
    public GameObject ExplosionPrefab;
    public SpriteRenderer sprit;
    public Placeholder[] enemies;
    private float distance;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprit = GetComponent<SpriteRenderer>();
        rangedProjectileScript = GetComponent<RangedProjectile>();
        SecondaryProjectile = GetComponent<SecondaryRanged>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (selfDestruct == true) {
            target = GameObject.FindWithTag(targ).transform;
            distance = Vector3.Distance(target.transform.position, transform.position);
        }
        else
        {
            target = FindClosestEnemy().Item1;
            distance = FindClosestEnemy().Item2;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        direction = dir;

        if (stunned)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Moving", false);
            return;
        }

        if (target & (distance > range))
        {
            animator.SetBool("Moving", true);
            rb.velocity = new Vector2(direction.x, direction.y) * movespeed;
            if (direction.x < 0)
            {
                sprit.flipX = true;
            }
            else
            {
                sprit.flipX = false;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Moving", false);
            if (!isAttacking)
            {
                if (selfDestruct == true)
                {
                    StartCoroutine(BlowUp());
                }
                else if(ranged == true){

                    Debug.Log(distance);

                    if (distance > 1.5f)
                    {
                        Debug.Log("Primary");
                        StartCoroutine(fire());
                    }
                    else
                    {
                        Debug.Log("Secondary");
                        StartCoroutine(fire2());
                    }
                    
                    animator.SetTrigger("Attack");
                    isAttacking = true;
                    StartCoroutine(attackCooldown());
                }
                else
                {
                    //animator.SetTrigger("Attack");
                    OnAttack();
                }
            }
        }
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(0.3f);

        rangedProjectileScript.Fire(this.transform.position, target.position, attackOffset);
               
    }

    IEnumerator fire2()
    {
        yield return new WaitForSeconds(0.3f);

        SecondaryProjectile.Fire(this.transform.position, target.position, attackOffset);

    }

    void OnAttack()
    {
        Vector2 targ = new Vector2(target.transform.position.x, target.transform.position.y);
        Vector2 selfToMouseVector = targ - rb.position;
        float selfToMouseAngle = Mathf.Atan2(selfToMouseVector.y, selfToMouseVector.x) * Mathf.Rad2Deg;
        Quaternion selfToMouseRotation = Quaternion.Euler(new Vector3(0, 0, selfToMouseAngle));

        Vector2 attackOffset = selfToMouseVector.normalized * attackOffsetScalar;
        Vector3 attackOffsetV3 = attackOffset;

        //StartCoroutine(attackdelay());
        instantiateAttack(meleeAttackPrefab, rb.transform.position + attackOffsetV3, selfToMouseRotation);
        
        isAttacking = true;
        StartCoroutine(attackCooldown());
    }

    void instantiateAttack(GameObject attackPrefab, Vector2 attackPosn, Quaternion selfToMouseRotation)
    {
        animator.SetTrigger("Attack");
        StartCoroutine(attackdelay());
        GameObject attack = Instantiate(attackPrefab, attackPosn, selfToMouseRotation);
        AttackDecay attackScript = attack.GetComponent<AttackDecay>();
        if (attackScript != null)
        {
            attackScript.InitializeConstants(attackDuration, attackDamage, attackSizeScalar, rb.position);
        }
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownDuration);
        isAttacking = false;
    }

    IEnumerator attackdelay()
    {
        yield return new WaitForSeconds(0.8f);
    }

    public void ApplyStun()
    {
        stunned = true;
        StartCoroutine(stun());
    }

    IEnumerator stun()
    {
        yield return new WaitForSeconds(1);
        stunned = false;
    }

    IEnumerator BlowUp() {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        Health x = GameObject.FindWithTag(targ).GetComponent<Health>();
        x.TakeDamage(25, Vector2.zero);
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    (Transform, float) FindClosestEnemy()
    {
        enemies = FindObjectsOfType<Placeholder>();
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (Placeholder enemy in enemies)
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