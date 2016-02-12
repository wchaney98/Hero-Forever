using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public GameObject HudDamageDealtObj;
    public float bulletDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the bullet hits an enemy, deal damage and display damage dealt
        if (other.gameObject.HasTag("Enemy"))
        {
            GameObject canvasObj = GameObject.Find("Canvas");
            Canvas canvas = canvasObj.GetComponent<Canvas>();

            GameObject dropTextObj = Instantiate(HudDamageDealtObj);
            Text dropText = dropTextObj.GetComponent<Text>();
            RectTransform dropTextRect = dropTextObj.GetComponent<RectTransform>();

            dropText.transform.SetParent(canvas.transform, false);
            dropTextRect.position = gameObject.transform.position;
            dropText.text = bulletDamage.ToString();

            other.gameObject.SendMessage("TakeDamage", bulletDamage);
            Destroy(gameObject);
        }
    }
}
