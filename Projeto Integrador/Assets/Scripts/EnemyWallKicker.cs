using UnityEngine;
using System.Collections;

public class EnemyWallKicker : Enemy 
{
	public float velocityX = 0.5F;
	public float velocityZ = 0.5F;

	char dir;

	bool changeDir = false;

	Vector3 positionToGo;

	public override void Start () 
	{
		base.Start ();

		dir = (Random.Range(0, 2) == 1)? 'r' : 'l';
		positionToGo = Vector3.zero;
	}

	public override void Update ()
	{
		base.Update ();

		if (changeDir) 
		{
			dir = (dir == 'r')? 'l' : 'r';
			changeDir = false;
		}
		
		positionToGo = new Vector3 (transform.position.x + ((dir == 'r')? velocityX : -velocityX), 2, transform.position.z - velocityZ);
	}

	void FixedUpdate () 
	{
		BehaviourWallKicker ();
	}

	void BehaviourWallKicker()
	{
		_rigidbody.velocity = positionToGo * velocityEnemy * Time.fixedDeltaTime;
	}

	public override void OnCollisionEnter(Collision other)
	{
		//base.OnCollisionEnter ();

		if (other.gameObject.tag == "Environment" || other.gameObject.tag == "Default") 
			changeDir = true;
	}
}
