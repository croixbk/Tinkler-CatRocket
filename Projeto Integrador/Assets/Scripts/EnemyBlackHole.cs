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
	
	void Update () 
	{
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

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Enemy type = other.gameObject.GetComponent<Enemy>();
			EnemyType typeEnemy = type.enemyType;
			
			if (typeEnemy != EnemyType.BlackHole)
			{
				if (typeEnemy == EnemyType.GoAhead)
				{
					EnemyGoAhead eGo = other.GetComponent<EnemyGoAhead>();
					eGo.getCaught = true;
					eGo.target = gameObject;
				}
				else if (typeEnemy == EnemyType.WallKicker)
				{

				}
			}
		}
	}
}
