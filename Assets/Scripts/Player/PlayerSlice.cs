using UnityEngine;
using System.Collections;

public class PlayerSlice : MonoBehaviour
{
    public Color c0 = Color.red;
    public Color c1 = Color.yellow;
    public float maxSlashTime;

    GameObject lineGameObject;
    LineRenderer lineRenderer;
    int i;
    float timeSinceLastSlash;

	void Start ()
    {
        lineGameObject = new GameObject("Line");
        lineGameObject.AddComponent<LineRenderer>();

        lineRenderer = lineGameObject.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.SetColors(c0, c1);
        lineRenderer.SetWidth(0.1f, 0f);
        lineRenderer.SetVertexCount(0);

        i = 0;
        timeSinceLastSlash = 0;
	}
	
	void Update ()
    {
	    if (Input.touchCount > 0)
        {
            timeSinceLastSlash += Time.deltaTime;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && timeSinceLastSlash <= maxSlashTime)
            {
                lineRenderer.SetVertexCount(i + 1);
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
                lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mousePos));
                i++;

                BoxCollider2D BC2D = lineGameObject.AddComponent<BoxCollider2D>();
                BC2D.transform.position = lineRenderer.transform.position;
                BC2D.size = new Vector2(0.1f, 0.1f);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Slash();

                lineRenderer.SetVertexCount(0);
                i = 0;

                BoxCollider2D[] colliders = lineGameObject.GetComponents<BoxCollider2D>();
                foreach (BoxCollider2D b in colliders)
                {
                    Destroy(b);
                }
                timeSinceLastSlash = 0f;
            }
        }
	}

    void Slash()
    {
        //implement slash method
    }
}
