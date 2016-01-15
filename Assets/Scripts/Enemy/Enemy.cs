using UnityEngine;
using System.Collections;
using System;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    // Abstract class to serve as an outline for all enemies

    public float damage;
    public float health;
    public float speed;

    public abstract void TakeDamage(float damage);
}
