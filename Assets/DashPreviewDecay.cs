using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPreviewDecay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dashDecay());
    }

    IEnumerator dashDecay()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
