using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    public bool falling;
    private Rigidbody2D rb;
    [SerializeField] private float startDelay;
    [SerializeField] private float interval;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ToggleFalling());
    }

    private void Update()
    {
        if (!falling)
        {
            rb.AddForce(rb.gravityScale * 4 * Vector2.up);
        }
    }

    private IEnumerator ToggleFalling()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            yield return new WaitForSeconds(interval);
            falling = !falling;
        }
    }
}
