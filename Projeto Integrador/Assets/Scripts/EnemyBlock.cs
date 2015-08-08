using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBlock : MonoBehaviour 
{
	public enum BlockType { Car, T, Tunel, L, C, I, V, LeftSlash, RightSlash }

	[Header("Block type")]
	public BlockType blockType;

	[Space(10)]
	[Header("List of next possibles blocks to spawn")]
	public List<GameObject> nextPossibles;

	[Space(10)]
	[Header("GameObjects references")]
	public GameObject upperPointSpawn;
	public GameObject bottomPointSpawn;
	public GameObject spawnPointReference;

	ControlGenerator control;
	Rigidbody _rigidbody;

	bool canSpawn = true;

	void Awake ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		control = GameObject.Find("Control Generator").GetComponent<ControlGenerator> ();
		control.currentSpawn = gameObject;
		spawnPointReference = GameObject.Find("Spawn Point");
	}

	void Update()
	{
		_rigidbody.velocity = new Vector3(transform.position.x, 2, -10 * control.velocityOfBlocks * Time.deltaTime);

		if (canSpawn)
		{
			float distance = Vector3.Distance(upperPointSpawn.transform.position, spawnPointReference.transform.position);

			//Debug.Log (Vector3.Distance(upperPointSpawn.transform.position, bottomPointSpawn.transform.position));

			if (distance >= 7.52F)
			{
				SortNextToSpawn();
				Destroy(gameObject, 10F);
				canSpawn = false;
			}
		}
	}

	void SortNextToSpawn()
	{
		int chance = Random.Range(0, nextPossibles.Count);

		control.currentSpawn = nextPossibles[chance];
		control.canSpawn = true;
	}
}
