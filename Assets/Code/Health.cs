using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 50;
    public int health = 50;
    public Rigidbody2D rb;
    private EnemyMovement enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage, Vector2 atkOrigin)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        if (enemyScript != null)
        {
            enemyScript.ApplyStun();
        }
        rb.AddForce((rb.position - atkOrigin).normalized * 100);
    }

    void Die ()
    {
        Destroy(gameObject);
    }

    public float hpPercent(object target)
    {
        return (float)health/(float)maxHP;
    }
}
