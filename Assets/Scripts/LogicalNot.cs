using UnityEngine;

public class LogicalNot : LogicalElement
{
	 LogicalInput li1;

	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	public void Logical_not(LogicalElement A)
	{
		state = !A.state;
	}

	private void Start()
	{
		li1 = GameObject.Find("Logical Input 1").GetComponent<LogicalInput>();
	}

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_not(li1);
		SpriteChange(off, on, sr);
	}
}
