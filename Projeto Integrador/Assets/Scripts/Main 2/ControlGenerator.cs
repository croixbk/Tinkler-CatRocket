using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Level { Easy, Medium, Hard, Hell }

public class ControlGenerator : MonoBehaviour
{
	[Space(10)]
	[Header("Current level")]
	public Level level;
	
	Player player;

	GameObject[] spawns;

	[Space(10)]
	[Header("Enemy types")]
	public GameObject[] easyEnemies;
	public GameObject[] mediumEnemies;
	public GameObject[] hardEnemies;
	public GameObject[] hellEnemies;

	[Space(10)]
	[Header("Time between enemy spawns")]
	public float timeBetweenSpawns = 3.5F;

	[Space(10)]
	[Header("If can spawn a new enemy")]
	public bool canSpawn = true;

	int contEnemies;

	void Awake()
	{
		canSpawn = true;
		level = Level.Easy;
	}

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		spawns = GameObject.FindGameObjectsWithTag ("Spawn");
	}

	void Update ()
	{
		if (spawns != null && canSpawn && player.alive) 
			StartCoroutine("Spawn");
	}

	IEnumerator Spawn()
	{
		canSpawn = false;

		switch (level) 
		{
			case Level.Easy:
			{
				SpawnNewEnemy(easyEnemies, spawns);
				break;
			}
			case Level.Medium:
			{
				SpawnNewEnemy(mediumEnemies, spawns);
				break;
			}
			case Level.Hard:
			{
				SpawnNewEnemy(hardEnemies, spawns);
				break;
			}
			case Level.Hell:
			{
				SpawnNewEnemy(hellEnemies, spawns);
				break;
			}
		}
		
		yield return new WaitForSeconds (timeBetweenSpawns);
		canSpawn = true;
	}

	void SpawnNewEnemy(GameObject[] enemiesV, GameObject[] spawnsG)
	{
		int indiceEnemy = Random.Range(0, enemiesV.Length);
		int indiceSpawn = Random.Range(0, spawnsG.Length);

		GameObject en = Instantiate(enemiesV[indiceEnemy], spawnsG[indiceSpawn].transform.position, spawnsG[indiceSpawn].transform.rotation) as GameObject;
		en.name = "Enemy " + contEnemies;
		contEnemies ++;
	}

	//
}
