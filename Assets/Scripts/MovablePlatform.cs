using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] wayPoints;
    private Vector3[] points;
    private Rigidbody2D rb;

    public Vector2 velocity;
    private Vector2 prevPosition;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        points = new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            points[i] = wayPoints[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = Vector3.MoveTowards(rb.position, points[index], speed * Time.deltaTime);
        if (Vector2.Distance(rb.position, points[index]) < 0.05f)
            index++;
        if (index >= points.Length)
            index = 0;

        velocity = (Vector2)transform.position - prevPosition;
        prevPosition = transform.position;
    }
}
