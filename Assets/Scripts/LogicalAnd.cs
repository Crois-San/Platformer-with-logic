using UnityEngine;

public class LogicalAnd : LogicalElement
{
	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	 LogicalOr lo1;

	 LogicalNot ln1;

	public void Logical_and(LogicalElement A, LogicalElement B)
	{
		state = (A.state && B.state);
	}

	private void Start()
	{
		lo1 = GameObject.Find("Logical Or").GetComponent<LogicalOr>();
		ln1 = GameObject.Find("Logical Not").GetComponent<LogicalNot>();
	}

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_and(lo1, ln1);
		SpriteChange(off, on, sr);
	}
}
