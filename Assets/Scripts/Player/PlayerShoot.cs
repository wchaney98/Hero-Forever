using UnityEngine;
using System.Collections;
using System;

public class PlayerShoot : MonoBehaviour, IDoesShoot
{

    public GameObject bulletPrefab;
    public float bulletVelocity;

    Transform firePoint;

    void Start ()
    {
        firePoint = GameObject.Find("FirePoint").transform;
	}

    void Update ()
    {
        HandleShootInput();
    }

    /// <summary>
    /// Void method which gets and processes mouse input
    /// </summary>
    void HandleShootInput()
    {
        if (ShootPressed())
        {
            Shoot();
        }
    }

    /// <summary>
    /// Boolean method which returns whether or not the right mouse button was pressed
    /// </summary>
    /// <returns>Returns whether or not the right mouse button was pressed</returns>
    bool ShootPressed()
    {
        if (Input.GetMouseButtonDown(1))
            return true;
        return false;
    }

    /// <summary>
    /// Void method which takes the current mouse position in world-space and shoots a player bullet in that direction with
    /// a player defined "bulletVelocity"
    /// </summary>
    public void Shoot()
    {
        Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (cursorInWorldPos - (Vector2)firePoint.position).normalized;
        Debug.DrawRay(firePoint.position, direction, Color.cyan, 3, false);
        Debug.DrawLine(firePoint.position, cursorInWorldPos, Color.red, 3);

        GameObject bullet = (GameObject) Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;
        Destroy(bullet, 5f);
    }
}
