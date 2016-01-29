using UnityEngine;
using System.Collections;
using System;
using Lean;

public class PlayerShoot : MonoBehaviour, IDoesShoot
{

    public GameObject bulletPrefab;
    public float bulletVelocity;

    public float fireRate { get; set; }

    Transform firePoint;
    Transform rotationIndicator;

    float timeSinceLastShot;
    Vector2 currentDirection;

    void Start ()
    {
        // Link to Multi-Finger drag event
        LeanTouch.OnMultiDrag += OnMultiDrag;

        firePoint = GameObject.Find("FirePoint").transform;
        rotationIndicator = GameObject.Find("RotationIndicator").transform;

        fireRate = 1f;
        timeSinceLastShot = 0;
        currentDirection = Vector2.right;

        SetRotationIndicator(currentDirection);
	}

    void OnMultiDrag(Vector2 MultiDragDelta)
    {
        // Get the direction vector from the FirePoint to the center of the multi-touch gesture
        Vector2 center = Camera.main.ScreenToWorldPoint(LeanTouch.GetCenterOfFingers());
        currentDirection = (center - (Vector2)firePoint.position).normalized;

        // Use the direction to rotate the indicator
        SetRotationIndicator(currentDirection);
    }

    void Update ()
    {
        // Firerate for the FirePoint - shoot every x seconds
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }

    /// <summary>
    /// Void method which takes the current multitouch position in world-space and shoots a player bullet in that direction with
    /// a player defined "bulletVelocity"
    /// </summary>
    public void Shoot()
    {
        GameObject bullet = (GameObject) Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = currentDirection * bulletVelocity;
        Destroy(bullet, 5f);
    }

    /// <summary>
    /// Rotates the RotationIndicator the face the center of the multi-touch gesture
    /// </summary>
    /// <param name="currentDirection">The vector the point the RotationIndicator in</param>
    void SetRotationIndicator(Vector2 currentDirection)
    {
        float rotationZ = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        rotationIndicator.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationZ - 45));
    }
}
