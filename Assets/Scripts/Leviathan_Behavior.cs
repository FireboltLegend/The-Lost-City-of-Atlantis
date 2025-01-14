using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leviathan_Behavior : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float followDistance = 3f; // Distance to maintain behind the player
    public float moveSpeed = 2f; // Speed of the enemy's movement
    public float followDelay = 0.5f; // Delay in following the player

    public GameObject projectilePrefab; // Projectile prefab
    public Transform firePoint; // Where the projectiles spawn
    public float shootInterval = 2f; // Time between shots

    [SerializeField, ReadOnly(true)] private float shootTimer; // Timer for shooting
    private Vector3 targetPosition; // The position the enemy moves to
    [SerializeField] private EnemyHealth enemyHealth; // Reference to the enemy's health

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // FollowPlayerWithDelay();
            HandleShooting();
        }
    }

    void OnDestroy()
    {
        SceneManager.LoadScene("Victory");
    }

    void FollowPlayerWithDelay()
    {
        Vector3 offset = (transform.position - player.position).normalized * followDistance;
        targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector3 direction = (player.position - firePoint.position).normalized;
            projectile.transform.up = direction;

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.damage = Random.Range(0.5f, 2f);
            }
        }
    }

}
