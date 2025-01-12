using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<SpearLauncher>() && !collision.GetComponent<PlayerController>() && collision.gameObject.layer != 4 && collision.gameObject.layer != 13) //4 is water layer and 13 is player layer
            Destroy(gameObject);
    }
}
