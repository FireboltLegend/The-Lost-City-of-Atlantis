using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthBar;
    public float health;

    private float startHealth;

    private void Start()
    {
        startHealth = health;
    }

    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
        healthBar.gameObject.SetActive(health < startHealth);
        healthBar.value = health / startHealth;
    }
}
