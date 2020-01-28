using System;
using UnityEngine;

[Serializable]
public abstract class LogicalElement : MonoBehaviour
{
	public bool state;

	public bool GetState()
	{
		return state;
	}

	public void SpriteChange(Sprite off, Sprite on, SpriteRenderer sr)
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
}
