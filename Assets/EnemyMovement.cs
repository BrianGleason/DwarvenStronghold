using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float health;
    float movespeed;
    [SerializeField] float range;
    Rigidbody2D rb;
    Transform target;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        health = 1f;
        movespeed = 2.5f;
        range = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.rotation = ang;
            direction = dir;
        }
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (target & (distance > range))
        {
            rb.velocity = new Vector2(direction.x, direction.y) * movespeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
