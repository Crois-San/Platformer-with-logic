using System;
using UnityEngine;

[Serializable]
public class LogicalInput : LogicalElement
{
	/*
	 * Состояние этого элемента изменяемо игроком посредством коллизии,
	 * передает свое состояние в другие элементы
	 */
	//свечение логического элемента
	private GameObject light;
	private void Start()
	{
		//находим свечение логического элемента
		light = transform.Find("LightSource").gameObject;
	}

	private void Update()
	{
		SpriteChange();
		//включаем свечение по сигналу
		light.SetActive(state);
	}

	private void OnCollisionEnter2D()
	{
		state = !state;
	}
}
