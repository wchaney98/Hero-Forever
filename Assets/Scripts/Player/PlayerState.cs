using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Struct to store all player data
/// </summary>
public struct PlayerState
{
    // todo: add methods to simplify use of struct and XP algorithm
    public float health;
    public int gold;
    public int xpForLastLevel;
    public int xp;
    public int xpToNextLevel;
    public long xpRunningTotal;

    int firstToSecondLevelXP;

    public PlayerState(float health, int gold, int xp, int firstToSecondLevelXP, int xpForLastLevel)
    {
        this.health = health;
        this.gold = gold;
        this.xp = xp;
        this.firstToSecondLevelXP = firstToSecondLevelXP;
        xpToNextLevel = firstToSecondLevelXP;
        xpRunningTotal = 0;
        this.xpForLastLevel = xpForLastLevel;
    }
	// More attributes to be added
}
