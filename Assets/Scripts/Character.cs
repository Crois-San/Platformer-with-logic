using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Range(0f, 10f)]
	public float jumpspeed;

	public LayerMask mask;

	private float moving;

	private float speed = 0.2f;

	private float fallMultiplier = 2.5f;

	private float lowFallMultiplier = 2f;

	private float groundedSkin = 0.5f;

	private bool jumpRequest;

	private bool grounded;

	private Vector2 jump;

	private Vector2 left;

	private Vector2 right;

	private Vector2 boxSize;

	private Vector2 characterSize;

	private Rigidbody2D body;

	private GameObject character;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		character = GetComponent<GameObject>();
		characterSize = GetComponent<BoxCollider2D>().size;
		boxSize = new Vector2(characterSize.x, groundedSkin);
	}

	private void Update()
	{
		moving = Input.GetAxis("Moving");
		body.transform.Translate(moving * speed, 0f, 0f, Space.Self);
		if (Input.GetButtonDown("Jump") && grounded)
		{
			jumpRequest = true;
		}
	}

	private void FixedUpdate()
	{
		if (jumpRequest)
		{
			body.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
			jumpRequest = false;
			grounded = false;
		}
		else
		{
			Vector2 boxCenter = (Vector2)body.transform.position + 0.5f * (characterSize.y + boxSize.y) * Vector2.down;
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
}
