using UnityEngine;
using System;
using System.Collections;

public class PlayerSlice : MonoBehaviour
{
    public Color c0 = Color.red;
    public Color c1 = Color.yellow;
    public float maxSlashTime;
    public float slashDelay;
    public float slashDamage;

    Lean.LeanFinger currFinger;

    GameObject lineGameObject;
    LineRenderer lineRenderer;
    int i;
    float timeSinceLastSlash;
    bool slashing;



    void Start()
    {
        Lean.LeanTouch.OnFingerDown += OnFingerDown;
        Lean.LeanTouch.OnFingerUp += OnFingerUp;
        currFinger = null;

        lineGameObject = new GameObject("Line");
        lineGameObject.AddComponent<LineRenderer>();

        lineRenderer = lineGameObject.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.SetColors(c0, c1);
        lineRenderer.SetWidth(0.1f, 0f);
        lineRenderer.SetVertexCount(0);

        i = 0;
        timeSinceLastSlash = 0;
        slashing = false;
    }

    public void OnFingerDown(Lean.LeanFinger finger)
    {
        currFinger = finger;
    }

    public void OnFingerUp(Lean.LeanFinger finger)
    {
        if (finger == currFinger)
        {
            currFinger = null;
        }
    }

    void Update()
    {
        if (slashing)
        {
            timeSinceLastSlash += Time.deltaTime;
        }

        if (currFinger != null && timeSinceLastSlash <= maxSlashTime)
        {


            slashing = true;
            lineRenderer.SetVertexCount(i + 1);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mousePos));
            i++;

            BoxCollider2D BC2D = lineGameObject.AddComponent<BoxCollider2D>();
            BC2D.transform.position = lineRenderer.transform.position;
            BC2D.size = new Vector2(0.1f, 0.1f);
        }

        if (currFinger == null)
        {
            BoxCollider2D[] colliders = lineGameObject.GetComponents<BoxCollider2D>();
            if (slashing)
                Slash(colliders);

            lineRenderer.SetVertexCount(0);
            i = 0;

            foreach (BoxCollider2D b in colliders)
            {
                Destroy(b);
            }
            timeSinceLastSlash = 0f;
        }
    }


    void Slash(BoxCollider2D[] colliders)
    {
        Debug.Log("slash");
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (BoxCollider2D b in colliders)
        {
            foreach (Enemy enemy in enemies)
            {
                if (b.IsTouching(enemy.GetComponent<BoxCollider2D>()))
                {
                    enemy.SendMessage("TakeDamage", slashDamage);
                }
            }
        }
        slashing = false;
    }
}
