using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	Vector3 targetPosition;

	public float timeToSmooth = 0.4F;
	public float maxDistance;
	public GameObject target;
	public GameObject sphere;

	//Vector3 reference = Vector3.zero;

	void Start () 
	{
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		/*

		Vector3 needPos = target.transform.forward * 10F;
		Vector3 nextPos = target.transform.position - needPos;

		transform.position = Vector3.SmoothDamp (transform.position, nextPos, ref reference, timeToSmooth);
		transform.rotation = target.transform.rotation;


		Ray ray = new Ray(transform.position, sphere.transform.position);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
		{
			transform.rotation = Quaternion.FromToRotation(transform.position, hit.normal);
		}

		Ray ray;
		RaycastHit hit;

		if(Physics.Raycast(transform.position, sphere.transform.position,out hit,Mathf.Infinity)){
			transform.rotation = Quaternion.FromToRotation(transform.position, hit.normal);
		}

		distance = Vector3.Distance (thisPosition, target.transform.position);
		if(distance > maxDistance){
			transform.Translate(target.transform.position);
		}

		TUDO LIXO
		*/ 
	}
}
