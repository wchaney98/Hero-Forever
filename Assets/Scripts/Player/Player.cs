using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour, IDoesShoot
{

    public GameObject bulletPrefab;
    public float bulletVelocity;

    Transform firePoint;

    bool didShoot;

    void Start ()
    {
        firePoint = GameObject.Find("FirePoint").transform;

        didShoot = false;
	}

    void Update ()
    {
        HandleShootInput();
    }

    //overcomplicated
    bool ShootPressed()
    {
        if (Input.GetMouseButtonDown(1) && !didShoot)
            return true;
        return false;
    }

    void HandleShootInput()
    {
        if (ShootPressed())
        {
            Shoot();
            didShoot = true;
        }
    }

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
