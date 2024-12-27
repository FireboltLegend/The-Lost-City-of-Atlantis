using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool grounded;

    private float jumpBuffer;
    private float groundedBuffer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        groundedBuffer -= Time.deltaTime;
        jumpBuffer -= Time.deltaTime;
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpBuffer <= 0)
            jumpBuffer = 0.1f;

        if (groundedBuffer > 0 && jumpBuffer > 0)
        {
            groundedBuffer = 0;
            jumpBuffer = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", grounded);

        if (rb.velocity.x != 0)
            spriteRenderer.flipX = rb.velocity.x < 0;
    }

    private void FixedUpdate()
    {
        grounded = false;
        Collider2D result = Physics2D.OverlapBox(rb.position + new Vector2(-0.05f, -0.75f), new Vector2(0.6f, 0.2f), 0, ground);
        if (Physics2D.OverlapBox(rb.position + new Vector2(-0.05f, -0.75f), new Vector2(0.6f, 0.2f), 0, ground))
        {
            grounded = true;
            groundedBuffer = 0.1f;
        }
        if (grounded && !result.isTrigger && jumpBuffer <= 0 && rb.velocity.y < Mathf.Abs(rb.velocity.x))
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }
}
