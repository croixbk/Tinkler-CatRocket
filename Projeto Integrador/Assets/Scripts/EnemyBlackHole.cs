using UnityEngine;
using System.Collections;

public class EnemyBlackHole : Enemy 
{
	[HideInInspector]
	public float mass; 
	public float nextScale;
	public float velocityToGrow = 1.0F;

	public override void Start () 
	{
		base.Start ();
		mass = _rigidbody.mass;
	}

	public override void Update () 
	{
		base.Update ();
		BehaviourBlackHole ();
	}

	// Put 1000 in Black Hole mass in RigidBody component

	void BehaviourBlackHole() 
	{
		transform.Rotate(new Vector3(0, Random.Range(1.0F, 2.0F) * Time.deltaTime, 0));
		
		nextScale = mass / 1000;
		mass = _rigidbody.mass;
		
		if(transform.localScale.x < nextScale)
		{
			float velocityTempToGrow = Time.deltaTime * velocityToGrow;
			transform.localScale += new Vector3(velocityTempToGrow, velocityTempToGrow, velocityTempToGrow);
		}
	}

	public override void OnCollisionEnter(Collision coll)
	{
		//base.OnCollisionEnter ();
	}

	void OnTriggerEnter(Collider other)
	{ 

	}
}
