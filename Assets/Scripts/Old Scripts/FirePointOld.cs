using UnityEngine;
using System.Collections;

public class FirePointOld : MonoBehaviour {

    public float horizontalSpeed;
    public float amplitude;
    public float lerpTime;

    private Vector3 tempPosition;
    private GameObject player;

	void Start ()
    {
        tempPosition = transform.position;
        player = GameObject.Find("FirePointOrigin");
	}

    void Update()
    {
        StartCoroutine(LerpToPlayer(transform.position, player.transform.position, lerpTime));
    }

    IEnumerator LerpToPlayer(Vector3 start, Vector3 end, float time)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, t))));
            tempPosition = transform.position;
            tempPosition.y = Mathf.Sin(Time.timeSinceLevelLoad * horizontalSpeed) * amplitude + transform.position.y;
            transform.position = tempPosition;
            yield return null;
        }
    }


}
