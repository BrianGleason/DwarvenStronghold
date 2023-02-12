using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = new Vector3(0, 1, 0);
        //transform.position = new Vector2(transform.position.x, transform.position.y);

        healthBar.localScale = new Vector3(0.015f * this.GetComponent<Health>().hpPercent(), 0.005f, 1);        
    }
}
