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
	
	[field: SerializeField]
	public GameObject wire { get; set; }
	public Transform wireOutp { get; set; }

	//Рендерер спрайтов, нужен для переключения спрайтов состояний элемента
	protected SpriteRenderer sr;

	protected ISoundSystem ss;

	public LogicalElement GetLE1 => le1;
	public LogicalElement GetLE2 => le2;

	public LogicalElement SetLE1
	{
		set => le1 = value;
	}
	public LogicalElement SetLE2
	{
		set => le1 = value;
	}

	public bool GetState()
	{
		return state;
	}

	public void SetWire(LogicalElement input)
	{
		if (input != null && !(gameObject.GetComponent<LogicalElement>() is LogicalWire))
		{
			wire = Resources.Load("Prefabs/Logical Wire") as GameObject;
			wire = Instantiate(wire);
			wire.name = input.name + " - " + gameObject.name;
			wire.GetComponent<LogicalWire>().SetLe1 = input;
			wire.GetComponent<LogicalWire>().SetLe2 = gameObject.GetComponent<LogicalElement>();
			wire.SetActive(true);

		}
	}

	protected virtual void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		ss = new SoundSystemDefaultLooping(gameObject,Sounds.LogicalElementBuzz, 0.048f);
		SetWire(le1);
		SetWire(le2);
		
	}
	protected virtual void Update()
	{
		SpriteChange();
		ElementSound();
	}

	//Эта функция изменяет спрайт в зависимости от состояния элемента
	protected void SpriteChange()
	{
		if (state)
		{
			sr.sprite = on;
		}
		else
		{
			sr.sprite = off;
		}
	}
	
	protected void ElementSound()
	{
		if (state)
		{
			ss.MakeSound();
		}
		else
		{
			ss.StopSound();
		}
	}
	
}
