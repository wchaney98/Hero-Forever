using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // Slot prefab
    public GameObject slotPrefab;

    List<Slot> slots = new List<Slot>();

    int currentRow;
    int currentColumn;

	void Start ()
    {
        currentRow = 1;
        currentColumn = 1;

	    for (int i = 0; i < 10; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab);
            
            newSlot.transform.SetParent(GameObject.Find("SlotPanel").transform);
            newSlot.transform.localScale = Vector3.one;
            //newSlot.GetComponent<Slot>().Initialize(GetNextFreeSlot());
            //inventory
            
        }
	}

    Vector2 GetFirstFreeSlot()
    {

        return Vector2.zero;
    }
	
	void LateUpdate ()
    {
	
	}
}
