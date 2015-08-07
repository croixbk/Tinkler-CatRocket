using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlGenerator : MonoBehaviour
{
	[HideInInspector]
	public bool canSpawn = false;

	[Space(10)]

	[Header("Current block to spawn")]
	public GameObject currentSpawn;

	[Space(10)]

	[Header("Spawnpoint reference")]
	public GameObject spawnPoint;

	[Space(10)]

	[Header("Blocks velocity")]
	public float velocityOfBlocks = 10F;

	int contBlock;

	void Awake ()
	{
		canSpawn = false;
		spawnPoint = GameObject.Find ("Spawn Point");
	}

	void Update ()
	{
		if (canSpawn) 
		{
			if (currentSpawn != null)
			{
				canSpawn = false;
				EnemyBlock enBlock = currentSpawn.GetComponent<EnemyBlock> ();

				GameObject go = Instantiate (currentSpawn, new Vector3 ((enBlock.blockType != EnemyBlock.BlockType.RightSlash) ? spawnPoint.transform.position.x + 0.65F : spawnPoint.transform.position.x - 0.65F, 2, spawnPoint.transform.position.z), Quaternion.identity) as GameObject;
				go.name = "Block " + contBlock;
				contBlock++;

				// Making the game harder, bitch
				// velocityOfBlocks += 0.1F;
			}
		}
	}
}
