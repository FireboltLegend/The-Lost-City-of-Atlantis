using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    [SerializeField] private float bobSpeed;
    [SerializeField] private float bobRange;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        timer += Random.Range(0f, 5f);
    }

    private void Update()
    {
        timer += Time.deltaTime * bobSpeed;
        rb.position = startPos + new Vector2(0, Mathf.Sin(timer) * bobRange);
    }
}
