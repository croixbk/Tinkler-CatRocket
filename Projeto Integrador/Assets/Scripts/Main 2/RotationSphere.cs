using UnityEngine;
using System.Collections;

public class RotationSphere : MonoBehaviour 
{
	public float velocity;
	public float outVelocity;
	Quaternion rotationPosition;
	public GameObject edgeCube;
	public LayerMask layerMask;
	public LayerMask edgeCubeLM;
	void Start()
	{

	}

	void Update () 
	{
		#if UNITY_EDITOR
			MouseMovement();	
		#endif
			TouchMovement ();
	}

	void MovementEdgeCube(Ray ray){
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,edgeCubeLM)){
				edgeCube.transform.position = hit.point;
			}

	}

	void MouseMovement()
	{
		if (Input.GetMouseButton (0)) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				if(hit.transform.name.Equals("Plane")){
					MovementEdgeCube(ray);
					//Debug.DrawLine(edgeCube.transform.position,transform.position,Color.red,Mathf.Infinity,false);
					if(Physics.Linecast(edgeCube.transform.position,transform.position,out hit,layerMask)){
						rotationPosition =  Quaternion.Lerp (transform.rotation,
					                                     Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation,
					                                     outVelocity * Time.deltaTime);
					}
				}
				else{
					rotationPosition =  Quaternion.Lerp (transform.rotation,
														Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation,
					                                    velocity * Time.deltaTime);
				}
			}
		}
		transform.rotation = rotationPosition;


	}
//--------O MouseMovement Funciona normalmente no celular(nao precisa do touch), mas vai que neh-----------------------//
	void TouchMovement(){
		if(Input.touches.Length != 0){
			Ray ray;
			RaycastHit hit;
			foreach(Touch touch in Input.touches){
				ray = Camera.main.ScreenPointToRay (touch.position);

				if (Physics.Raycast (ray, out hit)) {
					print(hit.transform.name);
					if(hit.transform.name.Equals("Plane")){
						MovementEdgeCube(ray);
						if(Physics.Linecast(edgeCube.transform.position,transform.position,out hit,layerMask)){
							rotationPosition =  Quaternion.Lerp (transform.rotation,
							                                     Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation,
							                                     outVelocity * Time.deltaTime);
						}
					}
					else{
						rotationPosition =  Quaternion.Lerp (transform.rotation,
						                                     Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation,
						                                     velocity * Time.deltaTime);
					}
				}
			}
			transform.rotation = rotationPosition;
		}

	}
}
