using UnityEngine;

public class LogicalNot : LogicalElement
{
	 //LogicalInput li1;

	 [SerializeField] 
	 private LogicalElement le;
	 
	private SpriteRenderer sr;

	[SerializeField]
	private Sprite on;

	[SerializeField]
	private Sprite off;

	public void Logical_not(LogicalElement A)
	{
		state = !A.state;
	}
	

	private void Update()
	{
		sr = GetComponent<SpriteRenderer>();
		Logical_not(le);
		SpriteChange(off, on, sr);
	}
}
