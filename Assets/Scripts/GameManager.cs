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

    // Prefabs for hovering text indicators
    public GameObject HudGoldDropPrefab;
    public GameObject HudDamageDealtPrefab;

    // Array for Spawners
    Spawner[] spawners = new Spawner[4];

    // Float to store elapsed time
    float timeSinceWave;

    // Public initial player health value
    public float startingPlayerHealth;
    public float xpMultiplier;
    public int startingPlayerGold;
    public int startingPlayerXP;
    public int firstToSecondLevelXP;

    // Declare PlayerState struct
    public PlayerState playerState;

    // Attribute panel toggle label text ref
    Text attrPanelToggLabel;

    // Reference to UI Canvas
    Canvas canvas;

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
        playerState = new PlayerState(startingPlayerHealth, xpMultiplier, startingPlayerGold, startingPlayerXP, firstToSecondLevelXP);

        // Initially disable attribute panel
        GameObject.Find("AttributePanel").SetActive(false);

        // Load attribute panel toggle label text ref
        attrPanelToggLabel = GameObject.Find("Label").GetComponent<Text>();

        // Init ref to Canvas object
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
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

        UpdateInternalsStats();
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
        hudPlayerXP.text = "XP: " + playerState.xpCurrent + " / " + playerState.xpToNextLevel + "\nTOTAL: " + playerState.xpRunningTotal;
    }

    /// <summary>
    /// Currently updates playerState variables and does necessary xp calculations
    /// </summary>
    void UpdateInternalsStats()
    {        
        // If at or above XP for next level, begin progressing to next level
        if (playerState.xpCurrent >= playerState.xpToNextLevel)
        {
            playerState.LevelUp();
        }

        // Flash player stats UI if stat points are available, otherwise keep it blue
        if (playerState.statPoints > 0)
        {
            playerState.AlertStatPointsAvailable(Time.deltaTime, Color.blue, Color.yellow);
        } else
        {
            attrPanelToggLabel.color = Color.blue;
        }
    }

    /// <summary>
    /// When an enemy dies, call this function to add the respective amount of gold to the player's total (and to instantiate a gold drop text)
    /// </summary>
    /// <param name="enemy">The enemy that died</param>
    public void LootDeadEnemy(Enemy enemy)
    {
        // "Loot" the enemy's predefined gold and xp and give it to the playerState
        playerState.gold += enemy.goldDrop;
        playerState.xpCurrent += enemy.xp;

        // Update grand total of player xp
        playerState.xpRunningTotal += enemy.xp;

        // Gold dropped text indicator
        GameObject dropTextObj = Instantiate(HudGoldDropPrefab);
        Text dropText = dropTextObj.GetComponent<Text>();
        RectTransform dropTextRect = dropTextObj.GetComponent<RectTransform>();

        dropText.transform.SetParent(canvas.transform, false);
        dropTextRect.position = enemy.transform.position;
        dropText.text = "+" + enemy.goldDrop + "g";
    }

    /// <summary>
    /// Instantiate damage dealt text where damage was dealt
    /// </summary>
    /// <param name="obj">Obj to display damage text, i.e. obj.transform</param>
    /// <param name="damager">What is dealing damage, implements IDoesDamage</param>
    public void DisplayDamageDealt(GameObject obj, IDoesDamage damager)
    {
        GameObject dropTextObj = Instantiate(HudDamageDealtPrefab);
        Text dropText = dropTextObj.GetComponent<Text>();
        RectTransform dropTextRect = dropTextObj.GetComponent<RectTransform>();

        dropText.transform.SetParent(canvas.transform, false);
        dropTextRect.position = new Vector3(obj.transform.position.x + Random.Range(-0.5f, 0.5f), obj.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
        dropText.text = "-" + damager.Damage.ToString();
    }
}
