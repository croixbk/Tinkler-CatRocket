using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float velocity = 10F;

	[HideInInspector]
	public bool alive = true;
	public int score;
	Rigidbody _rigidbody;

	void Start ()
	{
		alive = true;
		_rigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		if (alive) 
		{
			#if UNITY_EDITOR
				MouseMovement ();
			#else
				TouchMovement();
			#endif
		}
	}

	void MouseMovement ()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			pos = Camera.main.ScreenToWorldPoint (pos);

			pos = new Vector3 (pos.x - transform.position.x, 2, pos.z - transform.position.z).normalized;
			_rigidbody.velocity += pos * Time.fixedDeltaTime * velocity;
		} 
		else if (Input.GetMouseButton (0)) 
		{
			Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			pos = Camera.main.ScreenToWorldPoint (pos);

			pos = new Vector3 (pos.x - transform.position.x, 2, pos.z - transform.position.z).normalized;
			_rigidbody.velocity = pos * Time.fixedDeltaTime * velocity;
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

				if (touch.phase == TouchPhase.Began)
				{
					Vector3 movementDir = new Vector3 (hit.point.x - transform.position.x, 2, hit.point.z - transform.position.z).normalized;
					_rigidbody.velocity += movementDir * velocity * Time.fixedDeltaTime;
				}
				else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					Vector3 movementDirection = new Vector3 (hit.point.x - transform.position.x, 2, hit.point.z - transform.position.z).normalized;
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