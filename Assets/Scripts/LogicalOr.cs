using UnityEngine;

public class LogicalOr : LogicalElement
{
	 LogicalInput2 li2;

	 LogicalInput3 li3;

	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	public void Logical_or(LogicalElement A, LogicalElement B)
	{
		state = (A.state || B.state);
	}

	private void Start()
	{
		li2 = GameObject.Find("Logical Input 2").GetComponent<LogicalInput2>();
		li3 = GameObject.Find("Logical Input 3").GetComponent<LogicalInput3>();
	}

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_or(li2, li3);
		SpriteChange(off, on, sr);
	}
}
