using UnityEngine;
using System.Collections;
using System;

public abstract class Enemies : MonoBehaviour, IDamageable
{
    //remove abstract class?

    public int startingHealth;
    public int startingDamage;
    public float startingMoveSpeed ;

    public int health { get; private set; }
    public int damage { get; private set; }
    public float moveSpeed { get; private set; }

    public GameObject player_obj;
    public PlayerOld player_s;
    public Transform player_t;
    public Rigidbody2D RB2D;


    public virtual void Start()
    {
        health = startingHealth;
        damage = startingDamage;
        moveSpeed = startingMoveSpeed;
        player_obj = GameObject.Find("Player");
        player_s = player_obj.GetComponent<PlayerOld>();
        player_t = player_obj.transform;
    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            // todo make/set dying anim here
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health -= player_s.bulletDamage;
            Debug.Log("Hit, " + gameObject.name + " now has " + health + " health");
        }

        if (other.gameObject.tag.Contains("Enemy"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position - other.gameObject.transform.position);
            RB2D.velocity = startingMoveSpeed * -hit.normal;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= (int)damage;
    }
}