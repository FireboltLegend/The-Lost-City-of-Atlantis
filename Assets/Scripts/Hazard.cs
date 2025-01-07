using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] private bool continuous;
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (continuous)
                player.health -= damage * Time.deltaTime;
            else
                player.Damage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (continuous)
                player.health -= damage * Time.deltaTime;
            else
                player.Damage(damage);
        }
    }

}
