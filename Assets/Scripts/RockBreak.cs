using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBreak : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 2)
            Destroy(gameObject);
    }
}
