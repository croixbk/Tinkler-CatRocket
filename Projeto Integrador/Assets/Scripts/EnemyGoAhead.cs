using UnityEngine;
using System.Collections;

public class EnemyGoAhead : Enemy 
{
	[HideInInspector]
	public bool getCaught = false;
	public GameObject target = null;

	bool removeParentOnce = false;

	float speed = 5F;

	public override void Start()
	{
		base.Start ();
	}

	public override void Update () 
	{
		base.Update ();
		BehaviourGoAhead ();
	}

	void BehaviourGoAhead() 
	{
		if (getCaught)
		{
			if (target != null)
			{
				if (!removeParentOnce)
				{
					transform.parent = null;
					removeParentOnce = true;
				}
				
				//transform.LookAt(target.transform.position);
				// Not tested
				var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

				transform.right = new Vector3(transform.position.x + 0.2F, 2, transform.position.z) * velocityEnemy * Time.deltaTime;
				transform.forward = new Vector3(transform.position.x, 2, transform.position.z + 0.5F) * velocityEnemy * Time.deltaTime;
				
				float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
				
				if (distanceToTarget >= 0.3F) 
				{
					EnemyBlackHole eBH = target.GetComponent<EnemyBlackHole>();
					eBH._rigidbody.mass += _rigidbody.mass;
					Destroy(gameObject);
				}
			}
		}
	}

	public override void OnCollisionEnter(Collision other)
	{
		//base.OnCollisionEnter ();
	}
}