using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackDecay : MonoBehaviour
{
    public float duration = 5f;
    public float sizeScalar = 1f;
    public int damage = 5;
    public Vector2 attackOrigin;
    public bool piercing = false;
    public bool exploding = false;
    public int explodeDamage = 10;
    public GameObject explodePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(decay());
    }

    IEnumerator decay()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        DealDamage(col);
        if (exploding)
        {
            Instantiate(explodePrefab, this.transform.position, Quaternion.identity);
            DealExplosion();
        }
        if (!piercing)
        {
            Destroy(gameObject);
        }
    }

    void DealDamage(Collider2D col)
    {
        Health targetHealth = col.GetComponent<Health>();
        if (targetHealth)
        {
            targetHealth.TakeDamage(damage, attackOrigin);
        }
    }

    void DealExplosion()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 1.5f);
        Debug.Log(this.transform.position);
        foreach (var hitCollider in hitColliders)
        {
            Health targetHealth = hitCollider.GetComponent<Health>();
            if (targetHealth)
            {
                targetHealth.TakeDamage(explodeDamage, new Vector2(this.transform.position.x, this.transform.position.y));
            }
        }
    }

    public void InitializeConstants(float newDuration, int newDamage, float newSizeScalar, Vector2 playerPos)
    {
        duration = newDuration;
        damage = newDamage;
        sizeScalar = newSizeScalar;
        attackOrigin = playerPos;
    }
}
