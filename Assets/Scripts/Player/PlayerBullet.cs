using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public float bulletDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the bullet hits an enemy, deal damage
        if (other.gameObject.HasTag("Enemy"))
        {
            other.gameObject.SendMessage("TakeDamage", bulletDamage);
            Destroy(gameObject);
        }
    }
}
