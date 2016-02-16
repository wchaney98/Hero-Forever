using UnityEngine;
using System.Collections;

public class PlayerArea : MonoBehaviour
{
    GameManager GM;
    GameObject player;

	void Start ()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        // Get the script from the colliding enemy, subtract its remaining health from the player's health if it has the enemy tag
        Debug.Log("Triggered");
        if (other.gameObject.HasTag("Enemy"))
        {
            // Take damage
            Enemy enemy = other.gameObject.GetComponent(typeof(Enemy)) as Enemy;
            GM.playerState.health -= enemy.health;

            // Display damage dealt
            GM.DisplayDamageDealt(player, enemy);

            // Safely destroy the enemy object
            enemy.health = 0;
        }
    }
}
