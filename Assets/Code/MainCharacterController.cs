using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainCharacterController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 4f;
    public float idleFriction = 0.1f;
    public float attackOffsetScalar = 1.5f;
    public float attackSizeScalar = 1f;
    public float attackDuration = 0.25f;
    public float attackCooldownDuration = 0.25f;
    public int hitPoints = 50;
    public int attackDamage = 10;
    private bool isAttacking = false;
    private bool showing = false;


    private Vector2 movement;
    private Vector2 mousePos;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Camera cam;  // ?
    public GameObject meleeAttackPrefab;
    public Animator animator;
    public TextMesh text;

    public SpriteRenderer healthbar;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthbar.transform.parent = transform;
        DontDestroyOnLoad(gameObject);

        text = Instantiate(text, new Vector2(-6f, -4.5f), Quaternion.identity);
        text.text = "";
        text.fontSize = 12;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        animator.SetFloat("speed", movement.sqrMagnitude);
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            OnAttack();
        }

        if (transform.position.x > 8.5)
        {
            transform.position = new Vector2(8.5f, transform.position.y);
        }

        healthbar.transform.position = transform.position + Vector3.up / 2;
        healthbar.transform.localScale = new Vector3(0.06f * this.GetComponent<Health>().hpPercent(), 0.1f, 1);

        if (transform.position.y <= -4.4 && !showing)
        {
            showing = true;
            text.text = "What is this cave..? Press L to enter... If you dare.";
        }
        if (transform.position.y > 4.4 && showing)
        {
            text.text = "";
            showing = false;
        }

        if (transform.position.y <= -4.4 && Input.GetKeyDown(KeyCode.L))
        {
            cam.transform.position = new Vector3(0, -11, 0);

            // Pause enemy spawning and all allies and enemies currently active??
            // Instantiate the statue and the lobster.
            // Text description that shows up when close to statue, lobster drops 500g upon death.
            // Only available to enter once?
        }
    }

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
        animator.SetTrigger("attackTrigger");
        Vector2 playerPos = rb.position;
        Vector2 selfToMouseVector = mousePos - playerPos;
        float selfToMouseAngle = Mathf.Atan2(selfToMouseVector.y, selfToMouseVector.x) * Mathf.Rad2Deg;
        Quaternion selfToMouseRotation = Quaternion.Euler(new Vector3(0, 0, selfToMouseAngle));

        Vector2 attackOffset = selfToMouseVector.normalized * attackOffsetScalar;
        Vector3 attackOffsetV3 = attackOffset;

        instantiateAttack(meleeAttackPrefab, rb.transform.position + attackOffsetV3, selfToMouseRotation, playerPos);
        isAttacking = true;
        StartCoroutine(attackCooldown());
    }

    void instantiateAttack(GameObject attackPrefab, Vector2 attackPosn, Quaternion selfToMouseRotation, Vector2 playerPos)
    {
        GameObject attack = Instantiate(attackPrefab, attackPosn, selfToMouseRotation);
        AttackDecay attackScript = attack.GetComponent<AttackDecay>();
        if (attackScript != null)
        {
            attackScript.InitializeConstants(attackDuration, attackDamage, attackSizeScalar, playerPos);
        }
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownDuration);
        isAttacking = false;
    }

}
