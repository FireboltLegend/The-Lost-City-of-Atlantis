using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTank : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().oxygen = 100;
            Destroy(gameObject);
        }
    }
}
