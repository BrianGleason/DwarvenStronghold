using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 50;
    public int health = 50;
    public Rigidbody2D rb;
    private EnemyMovement enemyScript;

    public GameObject deathTextPrefab;
    public GameObject deathText;

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

    public void HealDamage(int damage)
    {
        int missingHealth = maxHP - health;

        health += Mathf.Min(missingHealth, damage);
    }

    private void Die ()
    {
        Destroy(gameObject);

        if (enemyScript != null)
        {
            deathText = Instantiate(deathTextPrefab, enemyScript.transform.position, Quaternion.identity);
            TextMesh txt = deathText.transform.GetComponent<TextMesh>();
            txt.fontSize = 18;
            txt.text = "+$1";
            Destroy(deathText, 1f);
            SystemControl.instance.AddGold(1);
        }
    }

    public float hpPercent(object target = null)
    {
        return (float)health/(float)maxHP;
    }
}
