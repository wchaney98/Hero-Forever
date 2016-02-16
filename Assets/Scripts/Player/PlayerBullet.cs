using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerBullet : MonoBehaviour, IDoesDamage
{
    public GameObject HudDamageDealtObj;
    public int bulletDamage;

    GameManager GM;

    public int Damage
    {
        get
        { return bulletDamage; }
    }

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the bullet hits an enemy, deal damage and display damage dealt
        if (other.gameObject.HasTag("Enemy"))
        {
            GM.DisplayDamageDealt(gameObject, this);
            other.gameObject.SendMessage("TakeDamage", bulletDamage);
            Destroy(gameObject);
        }
    }
}
