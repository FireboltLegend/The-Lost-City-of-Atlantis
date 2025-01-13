using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;

    private Transform player;
    private Camera cam;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.position = player.position + Vector3.forward * -10;

        Vector3 newPosition = Vector3.zero;
        newPosition.x = Mathf.Clamp(transform.position.x, min.x, max.x);
        newPosition.y = Mathf.Clamp(transform.position.y, min.y, max.y);
        newPosition.z = -10;
        transform.position = newPosition;
    }
}
