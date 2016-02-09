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
    public float xpMultiplier;

    public int gold;
    public int xpForLastLevel;
    public int xpCurrent;
    public int xpToNextLevel;

    public int power;
    public int dexterity;
    public int spirit;
    public byte statPoints;

    public long xpRunningTotal;

    byte level;
    byte pointsPerLevel;
    int firstToSecondLevelXP;
    GameObject attributePanel;
    GameManager GM;

    public PlayerState(float health, float xpMultiplier, int gold, int xp, int firstToSecondLevelXP)
    {
        // Init variables
        this.health = health;
        this.xpMultiplier = xpMultiplier;
        this.gold = gold;
        this.xpCurrent = xp;
        this.firstToSecondLevelXP = firstToSecondLevelXP;

        // Level 1 to Level 2 xp init
        xpToNextLevel = firstToSecondLevelXP;

        xpRunningTotal = xp; // Sum of all xp starts at starting xp
        xpForLastLevel = 0; // How much xp it took to get to level 1
        level = 1; // Level init at 1

        // Stats init
        power = 1;
        dexterity = 1;
        spirit = 1;
        statPoints = 0;
        pointsPerLevel = 3;

        // Attribute panel and GM script init
        attributePanel = GameObject.Find("AttributePanel");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	// More attributes to be added

    public void LevelUp()
    {
        // Store how much xp it took to get from last level to this level and store running total
        xpForLastLevel = xpToNextLevel;

        // Increment level and reset (visual) xpCurrent
        level += 1;
        xpCurrent = xpCurrent - xpToNextLevel;

        // Add stat points
        statPoints += pointsPerLevel;

        // Set how much xp it will take to get to the next level
        xpToNextLevel = (int)(xpToNextLevel *  xpMultiplier);

        // todo: implement ui to increase any stats any combo of 3 pts
        AlertStatIncrease();

        //Debug.Log("New Level: " + level + " at " + xpCurrent + " xp and " + xpToNextLevel + " to next lvl");
    }

    void AlertStatIncrease()
    {
        Debug.Log("bring up point distr");
        attributePanel.SetActive(true);
    }
}
