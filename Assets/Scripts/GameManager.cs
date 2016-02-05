using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Handles game events. Currently only handles enemy spawning.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Spawners
    public TopAirSpawner topAirSpawner;
    public MidAirSpawner midAirSpawner;
    public BotAirSpawner botAirSpawner;
    public GroundSpawner groundSpawner;

    // Spawners use spawnEnemies function every waveTime seconds
    public float waveTime;

    // GameObject of Player health HUD and its text
    public GameObject HudPlayerHealthObj;
    Text hudPlayerHealth;

    // GameObject of Player gold HUD and its text
    public GameObject HudPlayerGoldObj;
    Text hudPlayerGold;

    // GameObject of Player gold HUD and its text
    public GameObject HudPlayerXPObj;
    Text hudPlayerXP;

    // Prefab for gold drop object and its text
    public GameObject HudGoldDropObj;

    // Array for Spawners
    Spawner[] spawners = new Spawner[4];

    // Float to store elapsed time
    float timeSinceWave;

    // Public initial player health value
    public float startingPlayerHealth;
    public int startingPlayerGold;
    public int startingPlayerXP;
    public int firstToSecondLevelXP;

    // Declare PlayerState struct
    public PlayerState playerState;

	void Start ()
    {
        // Assign spawners to spawners array
        spawners[0] = topAirSpawner;
        spawners[1] = midAirSpawner;
        spawners[2] = botAirSpawner;
        spawners[3] = groundSpawner;

        // Initialize texts to their respective GO's
        hudPlayerHealth = HudPlayerHealthObj.GetComponent<Text>();
        hudPlayerGold = HudPlayerGoldObj.GetComponent<Text>();
        hudPlayerXP = HudPlayerXPObj.GetComponent<Text>();

        // Initialize wave-time to 0
        timeSinceWave = 0;

        // Initialize PlayerState struct
        playerState = new PlayerState(startingPlayerHealth, startingPlayerGold, startingPlayerXP, firstToSecondLevelXP, 0);
    }
	
	void Update ()
    {
        // Increments time elapsed every frame
        timeSinceWave += Time.deltaTime;

        // Takes enemy spawn chance from child Spawner uses rand to see if that Spawner will spawn this wave
        if (timeSinceWave >= waveTime)
        {
            timeSinceWave = 0f;
            foreach (Spawner spawner in spawners)
            {
                if (Random.Range(0f, 1f) <= spawner.spawnChancePerWave)
                {
                    spawner.spawnEnemies();
                }
            }
        }
        // End of spawning code

        // If at or above XP for next level, begin progressing to next level
        if (playerState.xp >= playerState.xpToNextLevel)
        {

        }

        UpdateHUD();
	}

    /// <summary>
    /// Updates the Heads-Up-Display
    /// </summary>
    void UpdateHUD()
    {
        hudPlayerHealth.text = "HEALTH: " + playerState.health;
        hudPlayerGold.text = "G O L D : " + playerState.gold;
        //todo:
        hudPlayerXP.text = "XP: " + (playerState.xp - playerState.xpForLastLevel) + " / " + playerState.xpToNextLevel;
    }

    /// <summary>
    /// When an enemy dies, call this function to add the respective amount of gold to the player's total
    /// </summary>
    /// <param name="enemy">The enemy that died</param>
    public void LootDeadEnemy(Enemy enemy)
    {
        playerState.gold += enemy.goldDrop;

        GameObject canvasObj = GameObject.Find("Canvas");
        Canvas canvas = canvasObj.GetComponent<Canvas>();

        GameObject dropTextObj = Instantiate(HudGoldDropObj);
        Text dropText = dropTextObj.GetComponent<Text>();
        RectTransform dropTextRect = dropTextObj.GetComponent<RectTransform>();

        dropText.transform.SetParent(canvas.transform, false);
        dropTextRect.position = enemy.transform.position;
        dropText.text = "+" + enemy.goldDrop + "g";
    }
}
