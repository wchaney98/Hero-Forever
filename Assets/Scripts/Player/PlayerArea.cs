using UnityEngine;
using System.Collections;

public class PlayerArea : MonoBehaviour
{
    GameManager GM;

	void Start ()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        // Get the script from the colliding enemy, subtract its remaining health from the player's health if it has the enemy tag
        Debug.Log("Triggered");
        if (other.gameObject.HasTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent(typeof(Enemy)) as Enemy;
            GM.playerState.health -= enemy.health;
            // Safely destroy the enemy object
            enemy.health = 0;
        }
    }
}
