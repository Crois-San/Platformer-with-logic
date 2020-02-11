using System;
using UnityEngine;

[Serializable]
public class LogicalInput : LogicalElement
{
	private void Update()
	{
		SpriteChange();
	}

	private void OnCollisionEnter2D()
	{
		state = !state;
	}
}
