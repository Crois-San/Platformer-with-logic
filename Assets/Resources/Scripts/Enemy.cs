using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Collider2D[] collidersNextToEntity, collidersUnderEntity, collidersUnderPlatform;
    private float timer;
    private float groundHeight;


    protected override void Moving(float move)
    {
        collidersNextToEntity =
            Physics2D.OverlapCircleAll(transform.position + move * 2.0f * Vector3.right, 0.2f, mask);
        collidersUnderEntity =
            Physics2D.OverlapCircleAll(
                transform.position + (characterSize.y / 2 + 0.5f) * Vector3.down + move * (characterSize.x/2 + 0.2f) * Vector3.right, 0.2f,
                mask);
        if (collidersUnderEntity.Length !=0)
        {
            groundHeight = collidersUnderEntity[0].bounds.size.y;
            collidersUnderPlatform = Physics2D.OverlapCircleAll(
                transform.position + (characterSize.y / 2 + 0.5f+groundHeight) * Vector3.down + move * (characterSize.x/2 + 0.2f) * Vector3.right, 0.2f,
                mask);
        }
        
        if (collidersNextToEntity.Length > 0)
        {
            jumpRequest = true; }

        Jumping();

        if (collidersUnderEntity.Length == 0 && collidersUnderPlatform.Length == 0 &&!jumpRequest)
        {
            moving *= -1;
        }

        base.Moving(move);
    }

    protected override void Jumping()
    {
        if (grounded && jumpRequest)
        {
            body.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            jumpRequest = false;
            grounded = false;
        }
        else
        {
            Vector2 boxCenter = (Vector2) body.transform.position + 0.5f * (characterSize.y + boxSize.y) * Vector2.down;
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);
        }

        if (body.velocity.y < 0f)
        {
            body.gravityScale = fallMultiplier;
        }
    }


    void StateIdle()
    {
        timer += Time.deltaTime % 60;
        if (timer >= 10)
        {
            moving *= -1;
            timer = 0.0f;
        }

        Moving(moving);
    }

    void StatePlayerChasing()
    {
    }

    void StateInputChanging()
    {
    }

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        moving = 1;
        speed = 0.05f;
        grounded = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
    }

    protected override void FixedUpdate()
    {
        StateIdle();
    }
}