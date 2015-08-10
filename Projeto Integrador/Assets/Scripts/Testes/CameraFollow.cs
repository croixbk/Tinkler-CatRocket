using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Vector3 thisPosition;
	public Vector3 targetPosition;
	public float distance;
	public float maxDistance;
	public GameObject target;
	public GameObject sphere;

	void Start () {
		thisPosition = transform.position;
		target = GameObject.Find ("Player");
		sphere = GameObject.Find("Sphere");
	}
	
	void Update () {
/*
		Ray ray;
		RaycastHit hit;

		if(Physics.Raycast(transform.position, sphere.transform.position,out hit,Mathf.Infinity)){
			transform.rotation = Quaternion.FromToRotation(transform.position, hit.normal);
		}

		distance = Vector3.Distance (thisPosition, target.transform.position);
		if(distance > maxDistance){
			transform.Translate(target.transform.position);
		}
*/ 
		//TUDO LIXO//
	}
}
