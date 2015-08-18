using UnityEngine;
using System.Collections;

public enum TypeTransform { Up, Forward}

public class WorldMovement : MonoBehaviour 
{
	public TypeTransform typeTransform;

	public float velocity = 2F;
	Vector3 ahead; 
 
	void Update () 
	{
		ahead = ((typeTransform == TypeTransform.Up)? Vector3.up : Vector3.forward) * velocity * Time.deltaTime;
		transform.Translate (ahead); 
	}
}