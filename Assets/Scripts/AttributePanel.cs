﻿using UnityEngine;
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

    Button powerButton;
    Button dexterityButton;
    Button spiritButton;

	void Start ()
    {
        pointsRemainingText = GameObject.Find("PointsRemaining").GetComponent<Text>();
        powerText = GameObject.Find("PowerText").GetComponent<Text>();
        dexterityText = GameObject.Find("DexterityText").GetComponent<Text>();
        spiritText = GameObject.Find("SpiritText").GetComponent<Text>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        powerButton = GameObject.Find("AddPowerButton").GetComponent<Button>();
        dexterityButton = GameObject.Find("AddDexterityButton").GetComponent<Button>();
        spiritButton = GameObject.Find("AddSpiritButton").GetComponent<Button>();
    }
	
	void LateUpdate ()
    {
        pointsRemainingText.text = "Points Remaining: " + GM.playerState.statPoints;
        powerText.text = "Power: " + GM.playerState.power;
        dexterityText.text = "Dexterity: " + GM.playerState.dexterity;
        spiritText.text = "Spirit: " + GM.playerState.spirit;
        //todo: remove possibility for statpoints to go negative (actually to 255)
	}

    public void OnToggleDropDown()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OnAddPower()
    {
        if (GM.playerState.statPoints > 0)
        {
            powerButton.interactable = true;
            GM.playerState.power += 1;
            GM.playerState.statPoints -= 1;
        }
        else
        {
            powerButton.interactable = false;
        }
    }

    public void OnAddDexterity()
    {
        if (GM.playerState.statPoints > 0)
        {
            dexterityButton.interactable = true;
            GM.playerState.dexterity += 1;
            GM.playerState.statPoints -= 1;
        }
        else
        {
            dexterityButton.interactable = false;
        }
    }
    

    public void OnAddSpirit()
    {
        if (GM.playerState.statPoints > 0)
        {
            spiritButton.interactable = true;
            GM.playerState.spirit += 1;
            GM.playerState.statPoints -= 1;
        }
        else
        {
            spiritButton.interactable = false;
        }
    }
}
