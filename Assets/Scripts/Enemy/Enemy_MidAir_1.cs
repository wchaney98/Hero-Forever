using UnityEngine;
using System.Collections;
using System;

public class Enemy_MidAir_1 : Enemy
{
    // MidAir1 enemy behavior, inherits from abstract Enemy class

    Rigidbody2D RB2D;

    public override void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Start ()
    {
        RB2D = GetComponent<Rigidbody2D>();
        RB2D.velocity = new Vector2(-speed, 0f);
    }
	
	void Update ()
    {
        if (health <= 0f)
            Destroy(gameObject);
	}

    void FixedUpdate()
    {
        
    }
}
