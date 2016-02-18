using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;

public class ItemDatabase : MonoBehaviour
{

	void Start ()
    {
        BuildItemDatabase();
	}
	
	void Update ()
    {
	
	}

    void BuildItemDatabase()
    {
        string jsonData = string.Join("", File.ReadAllLines(Application.dataPath + "/Scripts/JSON/items.json"));
        Item jsonItem = JsonUtility.FromJson<Item>(jsonData);
        Debug.Log(jsonItem.ID + ", " + jsonItem.Title);
    }
}

[Serializable]
public class Item
{
    public int ID;
    public string Title;

    public Item()
    {

    }
}