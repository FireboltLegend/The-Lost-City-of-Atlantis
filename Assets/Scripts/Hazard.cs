using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool continuous;
    [SerializeField] private float delay;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() && continuous)
            collision.GetComponent<PlayerController>().health -= damage * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && continuous)
            collision.gameObject.GetComponent<PlayerController>().health -= damage * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && !continuous)
        {
            StartCoroutine(InflictDamage(collision.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && !continuous)
        {
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && !continuous)
        {
            StartCoroutine(InflictDamage(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && !continuous)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator InflictDamage(GameObject player)
    {
        while (true)
        {
            player.GetComponent<PlayerController>().health -= damage;
            yield return new WaitForSeconds(delay);
        }
    }
}
