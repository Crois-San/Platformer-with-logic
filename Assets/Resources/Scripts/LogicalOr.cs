using UnityEngine;

public class LogicalOr : LogicalElement
{
	/*
	 * Логическое И принимает на вход два сигнала и выдает положительное состояние,
	 * если хотя бы одно состояние на входе положительно.
	 */
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
