using UnityEngine;

public class LogicalXor : LogicalElement
{
	public void Logical_xor(LogicalElement A, LogicalElement B)
	{
		state = (A.state || B.state) && (!A.state || !B.state);
	}
	
	private void Update()
	{
		Logical_xor(le1, le2);
		SpriteChange();
	}
}
