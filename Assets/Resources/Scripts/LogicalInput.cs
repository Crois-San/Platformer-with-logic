using System;
using UnityEngine;

[Serializable]
public class LogicalInput : LogicalElement
{
	/*
	 * Состояние этого элемента изменяемо игроком посредством коллизии,
	 * передает свое состояние в другие элементы
	 */
	private void Update()
	{
		SpriteChange();
	}

	private void OnCollisionEnter2D()
	{
		state = !state;
	}
}
