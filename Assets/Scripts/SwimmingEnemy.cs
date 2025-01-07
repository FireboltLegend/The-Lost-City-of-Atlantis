using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingEnemy : MonoBehaviour
{
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float patrollingRange;
    [SerializeField] private float playerDetectionRange;
    [SerializeField] private float maxWaterLevel;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform player;

    public string mode;
    private Vector2 target;
    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
        mode = "Patrol";
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= playerDetectionRange)
        {
            mode = "Attack";
        }
        if (Vector2.Distance(transform.position, player.position) > playerDetectionRange)
        {
            mode = "Patrol";
        }

        spriteRenderer.flipX = rb.velocity.x > 0;
    }

    private void FixedUpdate()
    {
        if (mode == "Attack")
        {
            rb.AddForce((player.position - transform.position).normalized * attackSpeed);
        }
        else if (mode == "Patrol" && Vector2.Distance(transform.position, target) > 1f)
        {
            rb.AddForce((target - (Vector2)transform.position).normalized * patrolSpeed);
        }
        rb.position = new Vector2(rb.position.x, Mathf.Clamp(rb.position.y, -1000, maxWaterLevel));

    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            rb.velocity = Vector2.zero;
            target = new Vector2(Random.Range(-patrollingRange, patrollingRange), Random.Range(-patrollingRange, patrollingRange)) + startPos;
            yield return new WaitForSeconds(5f);
        }
    }
}
