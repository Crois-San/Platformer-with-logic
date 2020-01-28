using System;
using UnityEngine;

[Serializable]
public class LogicalInput2 : LogicalElement
{
	private LogicalInput3 li3;

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