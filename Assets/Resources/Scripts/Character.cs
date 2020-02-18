using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Range(0f, 10f)] public float jumpspeed;

    [SerializeField] protected LayerMask mask;

    protected float moving;

    protected float speed = 0.2f;

    protected float fallMultiplier = 2.5f;

    protected float lowFallMultiplier = 2f;

    protected float groundedSkin = 0.5f;

    protected bool jumpRequest;

    protected bool grounded;

    protected Vector2 jump;

    protected Vector2 left;

    protected Vector2 right;

    protected Vector2 boxSize;

    protected Vector2 characterSize;

    protected Rigidbody2D body;

    protected SpriteRenderer sr;

    public bool getJumpRequest => jumpRequest;
    public bool getGrounded => grounded;

    protected virtual void Moving(float move)
    {
        body.transform.Translate(move * speed, 0f, 0f, Space.Self);
        sr.flipX = move < 0.0f;
    }

    protected virtual void Jumping()
    {
        if (jumpRequest)
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
        else if (body.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            body.gravityScale = lowFallMultiplier;
        }
        else
        {
            body.gravityScale = 1f;
        }
    }

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        characterSize = GetComponent<BoxCollider2D>().size;
        sr = GetComponent<SpriteRenderer>();
        boxSize = new Vector2(characterSize.x, groundedSkin);
    }

    protected virtual void Update()
    {
        moving = Input.GetAxis("Moving");
        Moving(moving);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpRequest = true;
        }
    }

    protected virtual void FixedUpdate()
    {
        Jumping();
    }
}