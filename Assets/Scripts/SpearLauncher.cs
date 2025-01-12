using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLauncher : MonoBehaviour
{
    [SerializeField] private GameObject spear;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float shootInterval;
    [SerializeField] private float shootDelay;
    private float timer;
    private float startTimer;

    void Start()
    {
        
    }

    void Update()
    {
        startTimer += Time.deltaTime;
        if (startTimer >= shootDelay)
            timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            timer = 0;
            GameObject newSpear = Instantiate(spear, transform);
            newSpear.GetComponent<Rigidbody2D>().velocity = transform.right * shootSpeed;
        }
    }
}
