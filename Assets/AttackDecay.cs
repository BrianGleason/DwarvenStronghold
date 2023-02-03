using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDecay : MonoBehaviour
{
    public float duration = 1f;
    public float sizeScalar = 1f;
    public int damage = 5;

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
        EnemyDummy enemy = col.GetComponent<EnemyDummy>();
        if (enemy != null)
        {
            enemy.TakeDamage(5);
        }
    }

    public void InitializeConstants(float newDuration, int newDamage, float newSizeScalar)
    {
        duration = newDuration;
        damage = newDamage;
        sizeScalar = newSizeScalar;
    }
}
