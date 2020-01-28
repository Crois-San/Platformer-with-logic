using UnityEngine;

public class LogicalXor : LogicalElement
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
	public void Logical_xor(LogicalElement A, LogicalElement B)
	{
		state = (A.state || B.state) && (!A.state || !B.state);
	}
	
	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_xor(le1, le2);
		SpriteChange(off, on, sr);
	}
}
