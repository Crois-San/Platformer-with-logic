using System;
using UnityEngine;

[Serializable]
public abstract class LogicalElement : MonoBehaviour
{
	public bool state;
	
	[SerializeField]
	protected Sprite on;

	[SerializeField]
	protected Sprite off;
	
	[SerializeField]
	protected LogicalElement le1;

	[SerializeField]
	protected LogicalElement le2;

	protected SpriteRenderer sr;

	public bool GetState()
	{
		return state;
	}

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
