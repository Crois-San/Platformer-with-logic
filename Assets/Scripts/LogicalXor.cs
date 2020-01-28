public class LogicalXor : LogicalElement
{
	public void Logical_xor(LogicalElement A, LogicalElement B)
	{
		state = (A.state != B.state);
	}
}
