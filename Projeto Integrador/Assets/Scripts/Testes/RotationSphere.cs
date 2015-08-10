using UnityEngine;
using System.Collections;

public class RotationSphere : MonoBehaviour {

	Rigidbody rigidBody;
	public float velocity;

	void Start () {
	}
	
	void Update () {
		if (Input.GetMouseButton(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit) && !hit.transform.tag.Equals("Player")){
				transform.rotation = Quaternion.Lerp(
					transform.rotation, 
					Quaternion.FromToRotation(transform.up, hit.normal)* transform.rotation,
					velocity * Time.deltaTime);

			}
		} 
	}
}
