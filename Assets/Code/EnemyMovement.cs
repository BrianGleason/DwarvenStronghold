using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class EnemyMovement : MonoBehaviour
{
    public float movespeed;
    [SerializeField] float range;
    Rigidbody2D rb;
    Transform target;
    Vector2 direction;
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
    public GameObject ExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag(targ).transform;


    }

    // Update is called once per frame
    void Update()
    {

        if (target)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            direction = dir;
        }

    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (stunned)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (target & (distance > range))
        {
            rb.velocity = new Vector2(direction.x, direction.y) * movespeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (!isAttacking)
            {
                if (selfDestruct == true)
                {
                    Health x = GameObject.FindWithTag(targ).GetComponent<Health>();
                    x.TakeDamage(25, Vector2.zero);
                    Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    OnAttack();
                }

            }
        }

    }
    void OnAttack()
    {
        Vector2 targ = new Vector2(target.transform.position.x, target.transform.position.y);
        Vector2 selfToMouseVector = targ - rb.position;
        float selfToMouseAngle = Mathf.Atan2(selfToMouseVector.y, selfToMouseVector.x) * Mathf.Rad2Deg;
        Quaternion selfToMouseRotation = Quaternion.Euler(new Vector3(0, 0, selfToMouseAngle));

        Vector2 attackOffset = selfToMouseVector.normalized * attackOffsetScalar;
        Vector3 attackOffsetV3 = attackOffset;

        instantiateAttack(meleeAttackPrefab, rb.transform.position + attackOffsetV3, selfToMouseRotation);
        isAttacking = true;
        StartCoroutine(attackCooldown());
    }

    void instantiateAttack(GameObject attackPrefab, Vector2 attackPosn, Quaternion selfToMouseRotation)
    {
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

}
