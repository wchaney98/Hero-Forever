using UnityEngine;
using System.Collections;

public class Main_Cam : MonoBehaviour {

    //buggy lerp

    public float lerpTime;

    private GameObject player;
    private float zCoord;

	void Start () {
        player = GameObject.Find("Player");
        zCoord = transform.position.z;
	}
	
	void Update () {
        StartCoroutine(CenterOnPlayer(transform.position, player.transform.position, lerpTime));
	}

    IEnumerator CenterOnPlayer(Vector3 start, Vector3 end, float time)
    {
        // enumerator to lerp to player position, currently buggy
        float t = 0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / time;
            Vector3 temp = new Vector3();
            temp = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, t))));
            temp.z = zCoord;
            transform.position = temp;
            yield return null;
        }
    }
}
