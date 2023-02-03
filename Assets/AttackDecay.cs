using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDecay : MonoBehaviour
{
    public float duration = 1f;

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
}
