using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 4f;
    public float idleFriction = 0.1f;
    public float attackOffsetScalar = 1.5f;
    public int hitPoints = 50;
    public float attackCooldownDuration = 0.25f;
    private bool isAttacking = false;

    private Vector2 movement;
    private Vector2 mousePos;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Camera cam;
    public GameObject meleeAttackPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            OnAttack();
        }
    }

    // Fixed update is called at fixed intervals
    void FixedUpdate()
    {
        // if movement inputs not zero, apply clamped velocity up to maximum speed. If mouse to left/right of player, flip sprite
        // otherwise, normalize velocity with 0 vector, simulated friction
        if (movement != Vector2.zero && !isAttacking)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movement * moveSpeed * Time.deltaTime), maxSpeed);
        } else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
        }
        // flip sprite based on mouse pos
        if (rb.transform.position.x < mousePos.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.transform.position.x > mousePos.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnAttack()
    {
        Vector2 selfToMouseVector = mousePos - rb.position;
        float selfToMouseAngle = Mathf.Atan2(selfToMouseVector.y, selfToMouseVector.x) * Mathf.Rad2Deg;
        Quaternion selfToMouseRotation = Quaternion.Euler(new Vector3(0, 0, selfToMouseAngle));

        Vector2 attackOffset = selfToMouseVector.normalized * attackOffsetScalar;
        Vector3 attackOffsetV3 = attackOffset;

        instantiateAttack(meleeAttackPrefab, rb.transform.position + attackOffsetV3, selfToMouseRotation);
        isAttacking = true;
        StartCoroutine(attackCooldown());
    }

    void instantiateAttack(GameObject attackPrefab, Vector2 attackPosn, Quaternion selfToMouseRotation)
    {
        Instantiate(attackPrefab, attackPosn, selfToMouseRotation);
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownDuration);
        isAttacking = false;
    }


}
