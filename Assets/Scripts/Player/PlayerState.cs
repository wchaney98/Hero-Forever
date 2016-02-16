using UnityEngine;
using UnityEngine.UI;
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
    public bool statPointsAreAvailable;

    public long xpRunningTotal;

    byte level;
    byte pointsPerLevel;
    float colorLerpDuration;
    float elapsedColorLerpTime;
    bool isBlue;
    Text attrPanelToggLabel;

    public PlayerState(float health, float xpMultiplier, int gold, int xp, int firstToSecondLevelXP)
    {
        // Init variables
        this.health = health;
        this.xpMultiplier = xpMultiplier;
        this.gold = gold;
        xpCurrent = xp;

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
        statPointsAreAvailable = false;
        pointsPerLevel = 3;

        // Attribute panel and GM script init
        attrPanelToggLabel = GameObject.Find("Label").GetComponent<Text>();
        colorLerpDuration = 1.5f;
        elapsedColorLerpTime = 0f;
        isBlue = false;
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
    }

    /// <summary>
    /// When the player has available stat points, lerps between the initialColor and finalColor
    /// </summary>
    /// <param name="deltaTime">delta time from Update</param>
    public void AlertStatPointsAvailable(float deltaTime, Color initialColor, Color finalColor)
    {
        // Lerps between 2 colors
        elapsedColorLerpTime += deltaTime;
        float t = elapsedColorLerpTime / colorLerpDuration;

        if (t >= 1)
        {
            isBlue = !isBlue;
            t = 0f;
            elapsedColorLerpTime = 0f;
        }

        if (t <= 1 && isBlue)
        {
            attrPanelToggLabel.color = Color.Lerp(initialColor, finalColor, t);
        } 
        else if (t <= 1 && !isBlue)
        {
            attrPanelToggLabel.color = Color.Lerp(finalColor, initialColor, t);
        }
    }
}
