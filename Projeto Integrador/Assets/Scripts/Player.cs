using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	float velocity = 10F;

	[HideInInspector]
	public bool alive;

	[HideInInspector]
	public int score;

	Rigidbody _rigidbody;

	void Awake()
	{
		alive = true;
	}

	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		/*
		if (alive) 
		{
			#if UNITY_EDITOR
				MouseMovement ();
			#else
				TouchMovement();
			#endif
		} 
		*/
	} 

	void MouseMovement ()
	{
		if (Input.GetMouseButton(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				Vector3 movementDir = hit.point - transform.position;
				movementDir.y = 2;
				_rigidbody.velocity = movementDir * velocity * Time.fixedDeltaTime;
			}
		}  
		else
			_rigidbody.velocity = Vector3.zero;			
	}

	void TouchMovement ()
	{
		if (Input.touches.Length != 0) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) 
			{
				Touch touch = Input.GetTouch(0);

				Vector3 movementDirection;

				if (touch.phase == TouchPhase.Began)
				{
					movementDirection = new Vector3 (hit.point.x - transform.position.x, 2, hit.point.z - transform.position.z).normalized;
					_rigidbody.velocity += movementDirection * velocity * Time.fixedDeltaTime;
				}
				else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					movementDirection = new Vector3 (hit.point.x - transform.position.x, 2, hit.point.z - transform.position.z).normalized;
					_rigidbody.velocity = movementDirection * velocity * Time.fixedDeltaTime;
				}
				else if (touch.phase == TouchPhase.Ended)
					_rigidbody.velocity = Vector3.zero;
			}
		} 
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.collider.gameObject.tag == "Enemy") 
			alive = false;
	}
}