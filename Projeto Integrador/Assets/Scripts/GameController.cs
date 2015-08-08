using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyWave{
	[Header("This wave")]
	public GameObject[] enemies;

	[Header("Next wave")]
	public GameObject[] nextEnemies;
}

public class GameController : MonoBehaviour {

	public Player player;
	public GameObject[] enemies;
	public EnemyWave enemyWave;
	RendererAnimation backgroundAnim;
	ControlGenerator enemyGenerator;

	void Start () {
		enemyGenerator = GameObject.Find ("Control Generator").GetComponent<ControlGenerator> ();
		backgroundAnim = GameObject.Find ("Ground").GetComponent<RendererAnimation> ();
		player = GameObject.Find("Player").GetComponent<Player>();
		backgroundAnim.enabled = false;
	}
	
	void Update () {
	
	}

	public void StartGame(){
		player.canMove = true;
		backgroundAnim.enabled = true;
		enemyGenerator.canSpawn = true;
		SortEnemyWave ();
		SpawnEnemyWave ();
	}

	public void SortEnemyWave(){
		GameObject[] temp = new GameObject[3];
		for(int i=0; i< temp.Length;i++){
			temp[i] = enemies[Random.Range(0,enemies.Length)];
		}
		enemyWave.nextEnemies = temp;
	}

	public void SpawnEnemyWave(){
		enemyWave.enemies = enemyWave.nextEnemies;
		enemyGenerator.SpawnEnemy (enemyWave.enemies);
		SortEnemyWave ();
	}
}