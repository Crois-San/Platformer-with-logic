using UnityEngine;

public class LogicalOutput : LogicalElement
{
	/*
	 * Отображает сигнал на входе
	 */
	public void Logical_output(LogicalElement A)
	{
		state = A.state;
	}

	private void Update()
	{
		Logical_output(le1);
		SpriteChange();
	}
}
