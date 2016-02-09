using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Script for the stats panel, to add stats granted after levels
/// </summary>
public class AttributePanel : MonoBehaviour
{
    Text pointsRemainingText;
    Text powerText;
    Text dexterityText;
    Text spiritText;

    GameManager GM;

	void Start ()
    {
        pointsRemainingText = GameObject.Find("PointsRemaining").GetComponent<Text>();
        powerText = GameObject.Find("PowerText").GetComponent<Text>();
        dexterityText = GameObject.Find("DexterityText").GetComponent<Text>();
        spiritText = GameObject.Find("SpiritText").GetComponent<Text>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	void LateUpdate ()
    {
        pointsRemainingText.text = "Points Remaining: " + GM.playerState.statPoints;
        powerText.text = "Power: " + GM.playerState.power;
        dexterityText.text = "Dexterity: " + GM.playerState.dexterity;
        spiritText.text = "Spirit: " + GM.playerState.spirit;
        //todo: remove possibility for statpoints to go negative (actually to 255)
	}

    public void OnAddPower()
    {
        GM.playerState.power += 1;
        GM.playerState.statPoints -= 1;
    }

    //todo: add OnAddDexterity and Spirit functions
}
