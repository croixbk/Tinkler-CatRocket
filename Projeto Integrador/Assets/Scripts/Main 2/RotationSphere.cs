using UnityEngine;
using System.Collections;

public class RotationSphere : MonoBehaviour 
{
	public float velocityEnemy;
	public float factor = 3.1F;

	float velocityWhenBall;

	void Start()
	{
		velocityWhenBall = velocityEnemy / factor;
	}

	void Update () 
	{
		Rotate ();
	}

	void Rotate()
	{
		if (Input.GetMouseButton (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
		
			if (Physics.Raycast (ray, out hit))
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.FromToRotation (transform.up, (hit.transform.tag.Equals ("Ball")) ? -hit.normal : hit.normal) * transform.rotation, (hit.transform.tag.Equals ("Ball") ? velocityWhenBall : velocityEnemy) * Time.deltaTime);
		}
	}
}
