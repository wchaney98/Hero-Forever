using UnityEngine;
using System.Collections;

public class BulletOld : MonoBehaviour {

    public GameObject impactParticlesPrefab;

    //todo: particles on death

	void Awake () {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
	}
	
	void Update () {
	
	}

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "PlayerBullet")
        {
            foreach (ContactPoint2D pt in other.contacts)
            {
                GameObject impactParticles = Instantiate(impactParticlesPrefab, new Vector3(pt.point.x, pt.point.y, 0), Quaternion.identity) as GameObject;
                Destroy(impactParticles, 1f);
            }
   
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            GameObject impactParticles = Instantiate(impactParticlesPrefab, other.transform.position, Quaternion.identity) as GameObject;
            Destroy(impactParticles, 1f);
            Destroy(gameObject);
        }
    }
}
