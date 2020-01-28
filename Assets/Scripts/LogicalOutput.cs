using UnityEngine;

public class LogicalOutput : LogicalElement
{
	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	[SerializeField]
	LogicalElement le;

	public void Logical_output(LogicalElement A)
	{
		state = A.state;
	}

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_output(le);
		SpriteChange(off, on, sr);
	}
}
