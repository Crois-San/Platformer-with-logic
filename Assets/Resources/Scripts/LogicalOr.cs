using UnityEngine;

public class LogicalOr : LogicalElement
{
	public void Logical_or(LogicalElement A, LogicalElement B)
	{
		state = (A.state || B.state);
	}
	

	private void Update()
	{
		Logical_or(le1, le2);
		SpriteChange();
	}
}
