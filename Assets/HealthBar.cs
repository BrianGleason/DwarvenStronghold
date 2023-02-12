using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Health healthScript;
    public Transform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.position = transform.position + Vector3.up / 2;
        healthBar.localScale = new Vector3(0.06f * healthScript.hpPercent(), 0.1f, 1);
        Debug.Log(healthBar.localScale);
    }
}
