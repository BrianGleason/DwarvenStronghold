using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{

    public Transform targetTransform;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        findTargetTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetTransform)
        {
            findTargetTransform();
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)targetTransform.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = 1 * rotateAmount * rotateSpeed;
        rb.velocity = -1 * transform.right * speed;
    }

    void findTargetTransform()
    {
        var target = GameObject.FindGameObjectWithTag("Player");
        if (!target)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Base").transform;
        }
        else
        {
            targetTransform = target.transform;
        }
    }
}
