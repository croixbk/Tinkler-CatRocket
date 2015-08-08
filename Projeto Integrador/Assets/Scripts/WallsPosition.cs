using UnityEngine;
using System.Collections;

public enum Side { Up, Bottom, Right, Left }

public class WallsPosition : MonoBehaviour 
{
	public Side side;

	void Start () 
	{
		Vector3 position = Vector3.zero;

		switch (side)
		{	
			case Side.Bottom:
			{
				position = Camera.main.ScreenToWorldPoint(new Vector3(0F, 0.5F, 13F));
				position.y = 2F;
				position.x = 0F;
				transform.position = position;
				break;
			}
			case Side.Up:
			{
				position = Camera.main.ScreenToWorldPoint(new Vector3(0F, 0F, 0 - 13F));
				position.y = 2F;
				position.x = 0F;
				transform.position = position;
				break;
			}

			case Side.Right:
			{
				position = Camera.main.ScreenToWorldPoint(new Vector3(0F, 0F, -13));
				position.y = 2F;
				position.z = transform.position.z;
				transform.position = position;
				break;
			}
			case Side.Left:
			{
				position = Camera.main.ScreenToWorldPoint(new Vector3(0F, 0F, 13F));
				position.y = 2F;
				position.z = transform.position.z;
				transform.position = position;
				break;
			}
			
		}
	}
}
