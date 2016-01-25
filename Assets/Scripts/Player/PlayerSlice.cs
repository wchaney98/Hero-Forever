using UnityEngine;
using Lean;
using System;
using System.Collections;

public class PlayerSlice : MonoBehaviour
{
    public Color c0 = Color.red;
    public Color c1 = Color.yellow;
    public float maxSlashTime;
    public float slashDelay;
    public float slashDamage;
    public float slashSize;

    LeanFinger currFinger;

    GameObject lineGameObject;
    LineRenderer lineRenderer;
    int i;
    float timeSinceLastSlash;
    bool slashing;
    bool soloDragging;

    void Start()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnSoloDrag += OnSoloDrag;
        LeanTouch.OnMultiDrag += OnMultiDrag;
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
        soloDragging = false;
    }

    void OnFingerDown(LeanFinger finger)
    {
        currFinger = finger;
    }

    void OnFingerUp(LeanFinger finger)
    {
        if (finger == currFinger)
        {
            currFinger = null;
        }
    }

    void OnSoloDrag(Vector2 SoloDragDelta)
    {
        soloDragging = true;
    }

    void OnMultiDrag(Vector2 MultiDragDelta)
    {
        soloDragging = false;
    }

    void Update()
    {
        if (slashing)
        {
            timeSinceLastSlash += Time.deltaTime;
        }

        if (currFinger != null && timeSinceLastSlash <= maxSlashTime && soloDragging)
        {
            slashing = true;
            lineRenderer.SetVertexCount(i + 1);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mousePos));
            i++;

            BoxCollider2D BC2D = lineGameObject.AddComponent<BoxCollider2D>();
            BC2D.transform.position = lineRenderer.transform.position;
            BC2D.size = new Vector2(slashSize, slashSize);
        }

        if (currFinger == null && slashing)
        {
            BoxCollider2D[] colliders = lineGameObject.GetComponents<BoxCollider2D>();
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
