using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    private float shootTimer = 0f;                   // Timer to track shooting interval

    void Awake()
    {
        // Initialize the bullet pool
        bulletPool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, false, 30, 100);
    }

    void Update()
    {
        // Increment the shoot timer by delta time
        shootTimer += Time.deltaTime;

        // Check if the timer has exceeded the shooting interval
        if (shootTimer >= shootIntervalInSeconds)
        {
            Shoot();
            shootTimer = 0f; // Reset the timer after shooting
        }
    }

    private void Shoot()
    {
        // Get a bullet from the pool
        Bullet bullet = bulletPool.Get();

        // Set bullet position and rotation
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;

        // Activate the bullet
        bullet.gameObject.SetActive(true);
    }

    // Method to create a new bullet if none are available in the pool
    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.SetPool(bulletPool); // Assign the pool reference to the bullet
        return newBullet;
    }

    // Method called when taking a bullet from the pool
    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true); // Make sure bullet is active
    }

    // Method called when returning a bullet to the pool
    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Deactivate bullet to make it invisible
    }

    // Method called when a bullet is destroyed (for memory management)
    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public Transform parentTransform;
}