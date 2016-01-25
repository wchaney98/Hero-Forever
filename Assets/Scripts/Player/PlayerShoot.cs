using UnityEngine;
using System.Collections;
using System;
using Lean;

public class PlayerShoot : MonoBehaviour, IDoesShoot
{

    public GameObject bulletPrefab;
    public float bulletVelocity;

    public float fireRate { get; set; }

    LeanFinger currFinger;
    Transform firePoint;

    float timeSinceLastShot;
    Vector2 currentDirection;

    void Start ()
    {

        LeanTouch.OnMultiDrag += OnMultiDrag;

        currFinger = null;

        firePoint = GameObject.Find("FirePoint").transform;

        fireRate = 1f;
        timeSinceLastShot = 0;
        currentDirection = Vector2.right;
	}

    void OnMultiDrag(Vector2 MultiDragDelta)
    {
        Vector2 center = Camera.main.ScreenToWorldPoint(LeanTouch.GetCenterOfFingers());
        currentDirection = (center - (Vector2)firePoint.position).normalized;
        
    }

    void Update ()
    {
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
}
