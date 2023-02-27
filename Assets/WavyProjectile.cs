using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyProjectile : MonoBehaviour
{
    public float MoveSpeed = 4.0f;

    public float frequency = 10.0f;  // Speed of sine movement
    public float magnitude = 0.4f;   // Size of sine movement
    private Vector3 axis;

    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
        axis = transform.up;  // May or may not be the axis you want
        transform.localRotation = Quaternion.Euler(0, 180, 0);

    }

    void Update()
    {
        pos += transform.right * Time.deltaTime * MoveSpeed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
