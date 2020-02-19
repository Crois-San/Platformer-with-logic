using System;
using UnityEngine;

[Serializable]
public abstract class LogicalElement : MonoBehaviour
{
	//Состояние логического элемента. Меняется от воздействия других элементов в схеме
	 public bool state;
	
	//спрайт включенного элемента
	[SerializeField]
	protected Sprite on;

	//спрайт выключенного элемента
	[SerializeField]
	protected Sprite off;
	
	/*
	 * Каждый элемент принимает на вход другие объекты класса логических элементов.
	 * Из этих элементов берется их состояние и применятся в соотвествующую функцию логического элемента
	 */
	[SerializeField]
	protected LogicalElement le1;

	[SerializeField]
	protected LogicalElement le2;

	//Рендерер спрайтов, нужен для переключения спрайтов состояний элемента
	protected SpriteRenderer sr;

	public bool GetState()
	{
		return state;
	}

	//Эта функция изменяет спрайт в зависимости от состояния элемента
	protected void SpriteChange()
	{
		sr = GetComponent<SpriteRenderer>();
		if (state)
		{
			sr.sprite = on;
		}
		else
		{
			sr.sprite = off;
		}
	}
}
