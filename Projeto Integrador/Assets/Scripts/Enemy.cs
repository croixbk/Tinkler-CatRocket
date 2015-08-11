using UnityEngine;
using System.Collections;

public enum EnemyType { GoAhead, BlackHole, WallKicker,BlockController }

public class Enemy : MonoBehaviour 
{
	public EnemyType enemyType;
	public float velocityEnemy = 3F;
	public GameController control;
	public Rigidbody _rigidbody;

	Player player; 

	public virtual void Start () 
	{
		_rigidbody = GetComponent<Rigidbody>();
		control = GameObject.Find ("Spawns").GetComponent<GameController>();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		Destroy (gameObject, 60F);
	} 

	public virtual void Update()
	{
		RaycastHit hit;
		Vector3 pos = new Vector3 (transform.position.x, transform.position.y - 0.25F, transform.position.z);
		
		if (Physics.Raycast (pos, -transform.up, out hit)) 
			transform.rotation = Quaternion.FromToRotation (Vector3.up, hit.normal);
	}

	// So pra nao encher o saco no inspector com referencia nao usada
	IEnumerator asdasdasd()
	{
		yield return new WaitForSeconds (34232490);
		Debug.Log (player.name);
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			Player player = other.gameObject.GetComponent<Player>();
			player.alive = false;

			/*
				Colocar algum tipo de animaçao maneira ao inves de apenas destruir, 
				mas claro, nao temos a animaçao maneira, mas pode ser uma "explosao" de particulas
			*/

			Destroy (gameObject, 0.3F);
		}
	}
}
