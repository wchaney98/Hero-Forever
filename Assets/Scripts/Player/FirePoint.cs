using UnityEngine;
using System.Collections;

public class FirePoint : MonoBehaviour
{
    public float horizontalSpeed;
    public float amplitude;

    float initialY;

    void Start ()
    {
        initialY = transform.position.y;
    }
	
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.timeSinceLevelLoad * horizontalSpeed)
                                         * amplitude + initialY), transform.position.z);
    }
}
