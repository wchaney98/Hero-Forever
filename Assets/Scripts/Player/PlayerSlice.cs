using UnityEngine;
using Lean;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerSlice : MonoBehaviour
{
    public Color c0 = Color.red;
    public Color c1 = Color.yellow;
    public float maxVertexCount;
    public float slashDelay;
    public float slashDamage;
    public float slashSize;
    public float pointDelta;

    public Sprite visualHitBoxSprite;
    List<GameObject> visualHitBoxList = new List<GameObject>();
    List<BoxCollider2D> collidersList = new List<BoxCollider2D>();

    LeanFinger currFinger;

    GameObject lineGameObject;
    LineRenderer lineRenderer;
    int i;
    bool slashing;
    bool soloDragging;
    Vector2 currSoloDragDelta;

    void Start()
    {
        // Link to LeanTouch events
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnSoloDrag += OnSoloDrag;
        LeanTouch.OnMultiDrag += OnMultiDrag;
        currFinger = null;

        // Initialize LineRenderer
        lineGameObject = new GameObject("SlashLine");
        lineGameObject.AddComponent<LineRenderer>();

        lineRenderer = lineGameObject.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.SetColors(c0, c1);
        lineRenderer.SetWidth(0.1f, 0f);
        lineRenderer.SetVertexCount(0);

        // Initialize other variables
        i = 0;
        slashing = false;
        soloDragging = false;
        currSoloDragDelta = Vector2.zero;
    }

    // Lock the current finger down into currFinger
    void OnFingerDown(LeanFinger finger)
    {
        currFinger = finger;
    }

    // Un-lock the finger from currFinger if the finger is released
    void OnFingerUp(LeanFinger finger)
    {
        if (finger == currFinger)
        {
            currFinger = null;
        }
    }

    // If only one finger is dragging
    void OnSoloDrag(Vector2 SoloDragDelta)
    {
        soloDragging = true;
        currSoloDragDelta += SoloDragDelta;
    }
    
    // If more than one finger is dragging
    void OnMultiDrag(Vector2 MultiDragDelta)
    {
        soloDragging = false;
    }

    void Update()
    {

        if (currFinger != null && i <= maxVertexCount && soloDragging && (currSoloDragDelta.magnitude >= pointDelta))
        {
            // For each vertex

            // Set the lineRenderer position to mouse position
            slashing = true;
            lineRenderer.SetVertexCount(i + 1);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);

            // Create the visual hitbox for each "point" in the slash
            GameObject visualHitBoxGameObject = new GameObject("Hitbox Sprite");
            SpriteRenderer visualHitBox = visualHitBoxGameObject.AddComponent<SpriteRenderer>();
            visualHitBox.sprite = visualHitBoxSprite;
            visualHitBoxGameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            
            visualHitBoxList.Add(visualHitBoxGameObject);

            // Create a collider at this position, with size slashsize sqrd
            BoxCollider2D BC2D = visualHitBoxGameObject.AddComponent<BoxCollider2D>();
            BC2D.transform.position = visualHitBoxGameObject.transform.position;
            BC2D.size = new Vector2(slashSize, slashSize);

            collidersList.Add(BC2D);

            // Increment the vertex count and reset the drag delta
            lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mousePos));
            i++;
            currSoloDragDelta = Vector2.zero;
        }

        if (currFinger == null && slashing)
        {
            // Cleanup

            // Get colliders
            Slash(collidersList);

            // Reset vertex count
            lineRenderer.SetVertexCount(0);
            i = 0;

            // Destroy colliders and hitboxes for the current slash
            foreach (BoxCollider2D b in collidersList)
            {
                Destroy(b);
            }

            foreach (GameObject go in visualHitBoxList)
            {
                Destroy(go);
            }
        }
    }

    /// <summary>
    /// Checks each collider in the slash to see if it collides with an enemy; if so, the enemy takes slash damage
    /// </summary>
    /// <param name="colliders">Array for colliders from lineRenderer</param>
    void Slash(List<BoxCollider2D> colliders)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (colliders != null)
        {
            foreach (Enemy enemy in enemies)
            {
                foreach (BoxCollider2D b in colliders)
                {
                    if (b != null)
                    {
                        if (b.IsTouching(enemy.GetComponent<BoxCollider2D>()))
                        {
                            enemy.SendMessage("TakeDamage", slashDamage);
                            Debug.Log(enemy + " was just slashed");
                        }
                    }
                }
            }
        }
        slashing = false;
    }
}
