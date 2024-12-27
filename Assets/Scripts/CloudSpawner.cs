using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject[] clouds;
    private float timer;

    private void Start()
    {
        for (int i = -5; i < 30; i++)
        {
            GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length)], new Vector3(i * Random.Range(5f, 10f), transform.position.y + Random.Range(-3f, 1f), 0), Quaternion.identity);
            float multiplier = Random.Range(0.1f, 1f);
            cloud.transform.localScale *= multiplier;
            cloud.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, multiplier - 0.3f);
            cloud.GetComponent<Cloud>().speed = multiplier * 3;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            timer = 0;
            GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length)], transform.position + new Vector3(0, Random.Range(-3f, 1f), 0), Quaternion.identity);
            float multiplier = Random.Range(0.1f, 1f);
            cloud.transform.localScale *= multiplier;
            cloud.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, multiplier - 0.3f);
            cloud.GetComponent<Cloud>().speed = multiplier * 3;
        }
    }
}
