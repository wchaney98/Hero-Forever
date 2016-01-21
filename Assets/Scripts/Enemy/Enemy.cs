using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (BoxCollider2D))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    // Abstract class to serve as an outline for all enemies

    public float damage;
    public float health;
    public float speed;

    Rigidbody2D RB2D;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        RB2D.velocity = new Vector2(-speed, 0f);
    }

    public virtual void Update()
    {
        if (health <= 0f)
            Destroy(gameObject);
    }
}
