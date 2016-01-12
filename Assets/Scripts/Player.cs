using UnityEngine;
using System.Collections;
using System;

/*
    finished:
        movement w double jump
        some lights
        fireobject and lerp
        base player sprite
        one enemy sprite
        add player shooting system
        added enemies
        destroy bullets on impact
        fixed bullets making weird collisions
        added some enemies that follow player
        made enemies not collide with each other
*/

/*
    todo:
        backgrounds
        test & add more enemies
        make & add actual sprites (get working code first)
        main menu
        menus
        hud
        items
        inventory
        rpg elements
*/

public class Player : MonoBehaviour
{
    public float maxHMoveSpeed;
    public float jumpForce;
    public int maxJumps;
    public LayerMask GroundLayerMask;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    public int startingHealth;
    public int startingBulletDamage;
    public int health { get; private set; }
    public int bulletDamage { get; private set; }

    private float groundCheckRadius = 0.2f;

    private GameObject firePoint;
    private Rigidbody2D RB2D;
    private Transform groundCheck;
    private bool jump;
    private bool shoot;
    private bool facingRight;
    private bool needFlip;
    private float localX;
    private int jumpCount;
    private int bulletLifeTime;

    /*
    START AWAKE & UPDATE CALLS
    */

    void Awake()
    {
        firePoint = GameObject.Find("FirePoint");
        RB2D = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        jump = false;
        shoot = false;
        facingRight = true;
        needFlip = false;
        localX = transform.localScale.x;
        jumpCount = 0;
        bulletLifeTime = 3;

        health = startingHealth;
        bulletDamage = startingBulletDamage;

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), bulletPrefab.GetComponent<Collider2D>());
    }

    void Update()
    {
        GetJumpInput();
        GetShootInput();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        
        ComputeLeftAndRightVelocities(h);

        HandleJumpInput();
        HandleShootInput();

        if (needFlip)
        {
            ReorientateSprite();
        }


    }

    /*
    END AWAKE & UPDATE CALLS
    */

    void GetJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void HandleJumpInput()
    {
        if (IsGrounded())
        {
            jumpCount = 0;
        }
        if (!IsGrounded() && jumpCount == 0)
        {
            jumpCount = 1;
        }
        else if (jump && IsGrounded() && jumpCount == 0)
        {
            RB2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            jumpCount += 1;
        }
        else if (jump && jumpCount < maxJumps)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x, 0f);
            RB2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            jumpCount += 1;
        }
    }

    void GetShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
        }
    }

    void HandleShootInput()
    {
        if (shoot)
        {
            Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (cursorInWorldPos - (Vector2)firePoint.transform.position).normalized;
            Debug.DrawRay(firePoint.transform.position, direction, Color.cyan, 3, false);

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            GameObject.Destroy(bullet, (float)bulletLifeTime);

            shoot = false;
        }
            
    }

    bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, GroundLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void ReorientateSprite()
    {
        if (facingRight)
        {
            transform.localScale = new Vector3(localX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-localX, transform.localScale.y, transform.localScale.z);
        }
        needFlip = false;
    }

    void ComputeLeftAndRightVelocities(float h)
    {
        if (h < -0.0001f)
        {
            RB2D.velocity = (new Vector2(-maxHMoveSpeed, RB2D.velocity.y));
            facingRight = false;
            needFlip = true;
        }
        else if (h > 0.0001f)
        {
            RB2D.velocity = (new Vector2(maxHMoveSpeed, RB2D.velocity.y));
            facingRight = true;
            needFlip = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {                                                   
            
        }
    }
}