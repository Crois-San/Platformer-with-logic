using UnityEngine;

public class LogicalAnd : LogicalElement
{
	protected void Logical_and(LogicalElement A, LogicalElement B)
	{
		state = (A.state && B.state);
	}
	

	private void Update()
	{
		Logical_and(le1, le2);
		SpriteChange();
	}
}
