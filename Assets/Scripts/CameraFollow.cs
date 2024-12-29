using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        transform.position = player.position + Vector3.forward * -10;
    }
}
