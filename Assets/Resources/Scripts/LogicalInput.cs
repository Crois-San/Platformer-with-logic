using System;
using UnityEngine;

[Serializable]
public class LogicalInput : LogicalElement
{
	private ISoundSystem ssInput;
	/*
	 * Состояние этого элемента изменяемо игроком посредством коллизии,
	 * передает свое состояние в другие элементы
	 */
	protected override void Start()
	{
		base.Start();
		ssInput = new SoundSystemDefault(gameObject,Sounds.InputChange, 0.6f);
	}

//	private void Update()
//	{
//		SpriteChange();
//	}

	private void OnCollisionEnter2D()
	{
		state = !state;
		ssInput.MakeSound();
	}
}
