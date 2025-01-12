using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    void Start()
    {
        explosion.SetActive(false);
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        explosion.SetActive(true);
        yield return new WaitForSeconds(1f);
        explosion.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4) //4 is water layer
            GetComponent<Rigidbody2D>().drag = 6;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4) //4 is water layer
            GetComponent<Rigidbody2D>().drag = 0.1f;
    }
}
