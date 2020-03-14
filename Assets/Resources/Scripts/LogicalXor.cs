using UnityEngine;

public class LogicalXor : LogicalElement
{
	/*
	 * Логическое И принимает на вход два сигнала и выдает положительное состояние,
	 * если одно состояние на входе положительно, а другое отрицательно.
	 */
	public void Logical_xor(LogicalElement A, LogicalElement B)
	{
		state = (A.state || B.state) && (!A.state || !B.state);
	}
	
	protected  override void Update()
	{
		Logical_xor(le1, le2);
		base.Update();
	}
}
