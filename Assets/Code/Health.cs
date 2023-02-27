using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHP = 50;
    public int health = 50;
    public bool isBase;
    public Rigidbody2D rb;
    private EnemyMovement enemyScript;

    public GameObject deathTextPrefab;
    public GameObject deathText;
    public Slider healthBarSlider = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<EnemyMovement>();
        if (healthBarSlider)
        {
            healthBarSlider.maxValue = maxHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarSlider)
        {
            healthBarSlider.value = health;
        }
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
        if (isBase)
        {
            fade.gameOver = true;
        }

        Destroy(gameObject);

        if (enemyScript != null)
        {
            deathText = Instantiate(deathTextPrefab, enemyScript.transform.position, Quaternion.identity);
            TextMesh txt = deathText.transform.GetComponent<TextMesh>();
            txt.fontSize = 18;
            System.Random rand = new System.Random();
            int goldDrop = rand.Next(8, 15);
            if (enemyScript.ExplosionPrefab != null)
            {
                goldDrop += goldDrop;
            }
            txt.text = "+$" + goldDrop.ToString();
            Destroy(deathText, 1f);
            SystemControl.instance.AddGold(goldDrop);
        }
    }

    public float hpPercent(object target = null)
    {
        return (float)health/(float)maxHP;
    }
}
