using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Health healthScript;
    public SpriteRenderer healthBar;
    public bool isBase;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = transform.position + Vector3.up * 2 / 3;
        healthBar.transform.localScale = new Vector3(0.06f * healthScript.hpPercent(), 0.1f, 1);
        if (isBase)
        {
            healthBar.transform.position = transform.position + Vector3.up * 2 + Vector3.right * 0.5f;
            healthBar.transform.localScale = new Vector3(0.2f * healthScript.hpPercent(), 0.3f, 1);
        }
    }
}
