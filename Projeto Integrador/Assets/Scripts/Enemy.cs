using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public enum EnemyType { GoAhead, BlackHole, WallKicker,BlockController }

	public EnemyType enemyType;
	public float distanceToDie;
	public float velocity = 3F;
	public GameController control;
	public Rigidbody _rigidbody;
	GameObject spawnPoint;

	public virtual void Start () 
	{
		velocity = 10;
		distanceToDie = 20;
		_rigidbody = GetComponent<Rigidbody>();
		spawnPoint = GameObject.Find("Spawn Point");
		control = GameObject.Find ("Controller").GetComponent<GameController>();
	}

	void Update(){
		if(enemyType == EnemyType.BlockController){
			if (Vector3.Distance (transform.position, spawnPoint.transform.position) > distanceToDie) {
				control.SpawnEnemyWave();
				Destroy (this.gameObject);
			}
		}

	}

	void FixedUpdate(){
		_rigidbody.velocity = new Vector3(0,0,-velocity*Time.deltaTime);
	}

	void OnCollisionEnter(){
		Destroy (this.gameObject,0.3f);
	}
}
