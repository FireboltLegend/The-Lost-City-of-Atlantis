using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;
    [SerializeField] public float health;
    [SerializeField] public float oxygen;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider oxygenMeter;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool grounded;

    private float jumpBuffer;
    private float groundedBuffer;

    private bool swimming;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!swimming)
        {
            oxygen += 5 * Time.deltaTime;
            oxygen = Mathf.Clamp(oxygen, 0, 100);
            Run();
        }
        else
        {
            oxygen -= 5 * Time.deltaTime;
            oxygen = Mathf.Clamp(oxygen, 0, 100);
            Swim();
        }
        if (oxygen == 0)
            health -= 10 * Time.deltaTime;
        healthBar.value = health / 100;
        oxygenMeter.value = oxygen / 100;
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4) //Layer 4 is Water
        {
            swimming = true;
            rb.drag = 4;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4) //Layer 4 is Water
        {
            swimming = false;
            rb.drag = 0.1f;
        }
    }

    private void Run()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        groundedBuffer -= Time.deltaTime;
        jumpBuffer -= Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpBuffer <= 0)
            jumpBuffer = 0.1f;

        if (groundedBuffer > 0 && jumpBuffer > 0)
        {
            groundedBuffer = 0;
            jumpBuffer = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 3);

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", grounded);

        if (rb.velocity.x != 0)
            spriteRenderer.flipX = rb.velocity.x < 0;
    }

    private void Swim()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        jumpBuffer -= Time.deltaTime;
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpBuffer <= 0)
            jumpBuffer = 0.1f;

        if (jumpBuffer > 0)
        {
            jumpBuffer = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", grounded);

        if (rb.velocity.x != 0)
            spriteRenderer.flipX = rb.velocity.x < 0;
    }

    private void CheckIfGrounded()
    {
        grounded = false;
        Collider2D result = Physics2D.OverlapBox(rb.position + new Vector2(-0.05f, -0.75f), new Vector2(0.6f, 0.2f), 0, ground);
        if (Physics2D.OverlapBox(rb.position + new Vector2(-0.05f, -0.75f), new Vector2(0.6f, 0.2f), 0, ground))
        {
            grounded = true;
            groundedBuffer = 0.1f;
        }
        if (grounded && !result.isTrigger && jumpBuffer <= 0 && rb.velocity.y < Mathf.Abs(rb.velocity.x) && !swimming)
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }
}
