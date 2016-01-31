using UnityEngine;
using System.Collections;

/// <summary>
/// Struct to store all player data
/// </summary>
public struct PlayerState
{
    public float health;
    public int gold;

    public PlayerState(float health, int gold)
    {
        this.health = health;
        this.gold = gold;
    }
	// More attributes to be added
}
