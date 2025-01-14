using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;
    public float damage = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            PlayerController playerHealth = collision.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
