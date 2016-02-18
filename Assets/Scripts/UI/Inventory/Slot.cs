using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour
{
    public Vector2 slotPosition
    {
        get; set;
    }

    public void Initialize(Vector2 startPosition)
    {
        slotPosition = startPosition;
    }

    void Start ()
    {
	    
	}
	
	void Update ()
    {
	    
	}
}
