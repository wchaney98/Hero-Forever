using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public float bulletDamage;

	void Start ()
    {

	}
	
	void Update ()
    {
	
	}

    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.HasTag("Enemy"))
        {
            other.gameObject.SendMessage("TakeDamage", bulletDamage);
            Destroy(gameObject);
        }
    }
}
