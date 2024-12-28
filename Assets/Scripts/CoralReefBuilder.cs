using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralReefBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] coralPieces;
    [SerializeField] private float density;
    [SerializeField] private float range;

    [SerializeField] private Color[] coralColors;

    void Start()
    {
        for (float i = 0; i < range; i += 1f / density)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * i, Vector2.down, 200);
            GameObject coralPiece = Instantiate(coralPieces[Random.Range(0, 5)], hit.point, Quaternion.identity);
            coralPiece.transform.localScale *= Random.Range(1, 1.75f);
            coralPiece.GetComponent<SpriteRenderer>().color = coralColors[Random.Range(0, coralColors.Length)];
            coralPiece.transform.parent = transform;
        }
    }
}
