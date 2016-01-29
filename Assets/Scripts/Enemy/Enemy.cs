using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Abstract enemy class to group together all enemies. Uses the IDamageable interface.
/// </summary>
[RequireComponent (typeof (BoxCollider2D))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    // Abstract class to serve as an outline for all enemies

    public float damage;
    public float health;
    public float speed;

    Rigidbody2D RB2D;

    /// <summary>
    /// Takes a float to subtract from the enemy's health, "taking damage."
    /// </summary>
    /// <param name="damage">Damage inflicted upon enemy</param>
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
        // Check if the enemy is "dead" each frame
        if (health <= 0f)
            Destroy(gameObject);
    }
}
