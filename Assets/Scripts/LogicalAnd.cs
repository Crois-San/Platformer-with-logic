using UnityEngine;

public class LogicalAnd : LogicalElement
{
	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	[SerializeField]
	LogicalElement le1;

	[SerializeField]
	LogicalElement le2;

	public void Logical_and(LogicalElement A, LogicalElement B)
	{
		state = (A.state && B.state);
	}
	

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_and(le1, le2);
		SpriteChange(off, on, sr);
	}
}
