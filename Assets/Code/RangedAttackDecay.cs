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
        Health targetHealth = col.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, attackOrigin);
        }
        if (piercing && targetHealth != null)
        {
            return;
        }
        Destroy(gameObject);
    }

    public void InitializeConstants(float newDuration, int newDamage, float newSizeScalar, Vector2 playerPos)
    {
        duration = newDuration;
        damage = newDamage;
        sizeScalar = newSizeScalar;
        attackOrigin = playerPos;
    }
}
