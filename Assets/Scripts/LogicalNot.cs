using UnityEngine;

public class LogicalNot : LogicalElement
{
	public void Logical_not(LogicalElement A)
	{
		state = !A.state;
	}
	

	private void Update()
	{
		Logical_not(le1);
		SpriteChange();
	}
}
