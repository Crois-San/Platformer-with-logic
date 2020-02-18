using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Collider2D[] collidersNextToEntity, collidersUnderEntity, collidersUnderPlatform;
    private Collider2D input;
    private float timer;
    private float attentionTimerPlayer;
    private float lookingForInputTimer;
    private float attentionTimerInput;
    [SerializeField]
    private float speedIdle = 0.05f;
    [SerializeField]
    private float speedChasing = 0.1f;
    private float groundHeight;
    RaycastHit2D raycastResult;
    private Vector3 rayOriginPoint;
    [SerializeField]
    private LayerMask targetPlayerMask,targetInputMask;

    private GameObject targetPlayer, targetInput;




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
//        if (collidersUnderPlatform != null)
//        {
            if (collidersUnderEntity.Length == 0 && collidersUnderPlatform.Length == 0 &&!jumpRequest)
            {
                moving *= -1;
            }
//        }
        

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
        
        speed = speedIdle;
        if (timer >= 10)
        {
            moving *= -1;
            timer = 0.0f;
        }

        Moving(moving);
    }

    void StatePlayerChasing()
    {
        
        moving =Mathf.Sign((targetPlayer.transform.position - transform.position).x) ;
        attentionTimerPlayer += Time.deltaTime % 60;
        speed = speedChasing;
        Moving(moving);
        if (attentionTimerPlayer >= 3)
        {
            targetPlayer = null;
            attentionTimerPlayer = 0;
        }

    }

    void StateInputChanging()
    {
        int foo = (int) (lookingForInputTimer % 10);
        if (foo == 0 && lookingForInputTimer > 1)
        {
            attentionTimerInput += Time.deltaTime % 60;
            if (attentionTimerInput >= 2)
            {
                return;
            }
        }
        else
        {
            attentionTimerInput = 0;
            return;
        }
        input = Physics2D.OverlapCircle(transform.position, 5f, targetInputMask);
        if (!input) return;
        targetInput = input.gameObject;
        if (!targetInput) return;
        if (Mathf.Abs(targetInput.transform.position.x - transform.position.x) <= 0.1f)
        {
            jumpRequest = true;
            Jumping();
            targetInput = null;

        }
        else
        {
            moving = Mathf.Sign((targetInput.transform.position - transform.position).x);
            Moving(moving);
        }
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
        timer += Time.deltaTime % 60;
        lookingForInputTimer += Time.deltaTime % 60;
        rayOriginPoint = transform.position + moving * characterSize.x * Vector3.right;
        raycastResult = Physics2D.Raycast(rayOriginPoint, moving*Vector3.right,15f, targetPlayerMask);
        if (raycastResult)
        {
            targetPlayer = raycastResult.collider.gameObject;
        }
        if (raycastResult || targetPlayer)
        {
            StatePlayerChasing();
        }
        else
        {
            StateInputChanging();
            StateIdle();
        }
            
        
    }
}