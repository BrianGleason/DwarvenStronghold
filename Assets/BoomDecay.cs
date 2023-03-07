using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomDecay : MonoBehaviour
{
    public float decayTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting decay");
        StartCoroutine(decay());
    }

    IEnumerator decay()
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}
