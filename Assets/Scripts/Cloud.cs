using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        if (transform.position.x < -30)
            Destroy(gameObject);
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
