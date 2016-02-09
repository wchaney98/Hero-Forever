using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDGoldDrop : MonoBehaviour
{
    public float lifeTime;
    public float riseHeight;

    RectTransform rect;
    Text text;
    Vector2 start;
    Vector2 end;
    Color startColor;
    Color endColor;

	void Start ()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<Text>();
        start = rect.position;
        end = new Vector2(start.x, start.y + riseHeight);
        startColor = text.color;
        endColor = new Color(text.color.r, text.color.g, text.color.b, 0f);
	}
	
	void Update ()
    {
        StartCoroutine("FadeOut");
	}

    /// <summary>
    /// Lerps the gold drop text up and makes it fade
    /// </summary>
    /// <returns>null</returns>
    IEnumerator FadeOut()
    {
        // Initialize time elapsed
        float t = 0;
        
        // For the size of lifeTime, lerp the text up and the color to transparent
        while (t <= lifeTime)
        {
            t += Time.deltaTime;
            rect.position = Vector2.Lerp(start, end, t / lifeTime);
            text.color = Color.Lerp(startColor, endColor, Mathf.SmoothStep(0f, 1f, t / lifeTime));
            yield return null;
        }

        // Destroy the object when the text is fully transparent
        Destroy(gameObject);
    }
}
