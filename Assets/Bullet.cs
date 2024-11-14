using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;

    private Rigidbody2D rb;
    private IObjectPool<Bullet> bulletPool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Set the bullet's velocity when it becomes active
        rb.velocity = transform.right * bulletSpeed;
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // When bullet collides, return it to the pool
        if (bulletPool != null)
        {
            bulletPool.Release(this);
        }
        else
        {
            Destroy(gameObject); // Fallback if no pool is assigned
        }
    }

    void Update()
    {
        // Check if the bullet is off-screen, and if so, return it to the pool
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1)
        {
            if (bulletPool != null)
            {
                bulletPool.Release(this);
            }
            else
            {
                Destroy(gameObject); // Fallback if no pool is assigned
            }
        }
    }

    void OnDisable()
    {
        // Reset velocity when the bullet is deactivated
        rb.velocity = Vector2.zero;
    }
}
