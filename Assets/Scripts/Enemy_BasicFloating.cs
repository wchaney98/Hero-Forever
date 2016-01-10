using UnityEngine;
using System.Collections;

public class Enemy_BasicFloating : MonoBehaviour {

    [SerializeField] private int startingHealth; //debug
    [SerializeField] private float moveSpeed;

    public int health { get; set; }

    private GameObject player_obj;
    private Player player_s;
    private Transform player_t;
    private Rigidbody2D RB2D;


    void Start ()
    {
        health = startingHealth;
        player_obj = GameObject.Find("Player");
        player_s = player_obj.GetComponent<Player>();
        player_t = player_obj.transform;
        RB2D = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
	    if (health <= 0)
        {
            // todo make/set dying anim here
            Destroy(gameObject);
        }

        if (transform.position != player_t.position && Random.Range(0f, 1f) > 0.99f)
        {
            Vector2 direction = new Vector2(player_t.position.x - transform.position.x, player_t.position.y - transform.position.y).normalized;
            RB2D.velocity = moveSpeed * direction;
        }
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health -= player_s.damage;
            Debug.Log("Hit, now has " + health + " health");
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position - other.gameObject.transform.position);
            RB2D.velocity = moveSpeed * -hit.normal;
        }
    }
}
