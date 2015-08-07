using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	public float velocity = 12F;
	Vector3 ahead;

	void Update () 
	{
		ahead = Vector3.up * velocity * Time.deltaTime;
		transform.Translate (ahead);
	}
}