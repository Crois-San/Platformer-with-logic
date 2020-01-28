using UnityEngine;

public class LogicalOr : LogicalElement
{
	 [SerializeField]
	 LogicalElement le1;

	 [SerializeField]
	 LogicalElement le2;

	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	public void Logical_or(LogicalElement A, LogicalElement B)
	{
		state = (A.state || B.state);
	}
	

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_or(le1, le2);
		SpriteChange(off, on, sr);
	}
}
