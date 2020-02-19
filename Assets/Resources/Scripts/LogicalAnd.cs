using UnityEngine;

public class LogicalAnd : LogicalElement
{
	/*
	 * Логическое И принимает на вход два сигнала и выдает положительное состояние,
	 * если оба состояния на входе положительны.
	 */
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
