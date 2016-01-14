using UnityEngine;
using System.Collections;

public class Enemy_BasicFloating : Enemies {
    // enemy specific overrides

    public override void Update()
    {
        base.Update();

        if (transform.position != player_t.position && Random.Range(0f, 1f) > 0.99f && gameObject != null)
        {
            Vector2 direction = new Vector2(player_t.position.x - transform.position.x, player_t.position.y - transform.position.y).normalized;
            RB2D.velocity = moveSpeed * direction;
        }
    }

}
