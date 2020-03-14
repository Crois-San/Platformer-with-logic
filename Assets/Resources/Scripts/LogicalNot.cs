using UnityEngine;

public class LogicalNot : LogicalElement
{
	/*
	 * Логическое И принимает на вход один сигнал и выдает положительное состояние,
	 * если состояние на входе отрицательное, инвертируя сигнал.
	 */
	public void Logical_not(LogicalElement A)
	{
		state = !A.state;
	}
	

	protected  override void Update()
	{
		Logical_not(le1);
		base.Update();
	}
}
