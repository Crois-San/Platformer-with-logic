using UnityEngine;

public class LogicalOutput : LogicalElement
{
	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	LogicalAnd la1;

	public void Logical_output(LogicalElement A)
	{
		state = A.state;
	}

	private void Start()
	{
		la1 = GameObject.Find("Logical And").GetComponent<LogicalAnd>();
	}

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_output(la1);
		SpriteChange(off, on, sr);
	}
}
