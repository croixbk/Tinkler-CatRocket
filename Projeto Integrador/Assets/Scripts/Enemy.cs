using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public enum EnemyType { GoAhead, BlackHole, WallKicker }

	public EnemyType enemyType;
	public float velocity = 3F;
	public ControlGenerator control;
	public Rigidbody _rigidbody;

	public virtual void Start () 
	{
		_rigidbody = GetComponent<Rigidbody>();
		control = GameObject.Find ("Control Generator").GetComponent<ControlGenerator>();
	}
}
