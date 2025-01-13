using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask water;
    [SerializeField] public float health;
    [SerializeField] public float oxygen;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider oxygenMeter;
    [SerializeField] private float invincibilityTime;
    [SerializeField] public int level;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject audioSource;

	private Rigidbody2D rb;
	private Animator animator;
	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	private CircleCollider2D circleCollider;
	private bool grounded;

    private float jumpBuffer;
    private float groundedBuffer;
    private float damageTimer;

    [HideInInspector] public bool swimming;
    private Transform currentPlatform;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		circleCollider = GetComponent<CircleCollider2D>();
	}

    private void Update()
    {
        damageTimer -= Time.deltaTime;
        if (!swimming)
        {
            oxygen = 100;
            Run();
        }
        else
        {
            oxygen -= 1.5f * Time.deltaTime;
            oxygen = Mathf.Clamp(oxygen, 0, 100);
        }
        if (oxygen == 0)
            health -= 10 * Time.deltaTime;
        healthBar.value = health / 100;
        oxygenMeter.value = oxygen / 100;

        if (health <= 0)
        {
            SceneManager.LoadScene("Death");
        }

        if (currentPlatform != null)
            rb.position += currentPlatform.GetComponent<MovablePlatform>().velocity;

		if(Input.GetKeyDown(KeyCode.P))
		{
			pauseMenu.SetActive(true);
			audioSource.SetActive(false);
		}
	}

	private void FixedUpdate()
	{
		CheckIfInWater();
		if (swimming)
			Swim();
		CheckIfGrounded();
		animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
		animator.SetBool("Grounded", grounded);
		animator.SetBool("Swimming", swimming);
	}

    public void Damage(float damage)
    {
        // print("player damaged");
        if (damageTimer <= 0)
        {
            health -= damage;
            damageTimer = invincibilityTime;
        }
    }

	public void Resume()
	{
		pauseMenu.SetActive(false);
		audioSource.SetActive(true);
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

		spriteRenderer.flipY = false;
		if (rb.velocity.x != 0)
			spriteRenderer.flipX = rb.velocity.x < 0;
	}

	private void Swim()
	{
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		rb.AddForce(input * speed * 5);

		animator.speed = 0.5f;
		if (input.sqrMagnitude == 0)
			animator.speed = 0;

		if (rb.velocity.sqrMagnitude > 0)
			rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
		spriteRenderer.flipX = false;
		if (rb.velocity.x < 0)
			spriteRenderer.flipX = true;
	}

	private void CheckIfGrounded()
	{
		grounded = false;
		Collider2D result = Physics2D.OverlapBox(rb.position + new Vector2(-0.05f, -0.75f), new Vector2(0.6f, 0.2f), 0, ground);
		if (Physics2D.OverlapBox(rb.position + new Vector2(-0.05f, -0.75f), new Vector2(0.6f, 0.2f), 0, ground))
		{
			if (!swimming)
				rb.rotation = 0;
			grounded = true;
			groundedBuffer = 0.1f;
		}
		if (grounded && !result.isTrigger && jumpBuffer <= 0 && rb.velocity.y < Mathf.Abs(rb.velocity.x) && !swimming)
			rb.velocity = new Vector2(rb.velocity.x, 0);
	}

	private void CheckIfInWater()
	{
		if (Physics2D.OverlapBox((Vector2)transform.position + boxCollider.offset, boxCollider.size, transform.rotation.z, water))
		{
			boxCollider.enabled = false;
			circleCollider.enabled = true;
			rb.gravityScale = 0.1f;
			swimming = true;
			rb.drag = 4;
		}
		else
		{
			boxCollider.enabled = true;
			circleCollider.enabled = false;
			rb.gravityScale = 2;
			swimming = false;
			rb.drag = 0.1f;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 31) //Platform layer
        {
            currentPlatform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentPlatform = null;
    }
}
