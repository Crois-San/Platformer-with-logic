using System;
using UnityEngine;

[Serializable]
public class LogicalInput : LogicalElement
{
	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	private void Start()
	{
	}

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		SpriteChange(off, on, sr);
	}

	private void OnCollisionEnter2D()
	{
		state = !state;
	}
}
