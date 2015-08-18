using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Level { Easy, Medium, Hard, Hell }

public class ControlGenerator : MonoBehaviour
{
    #region Variables

    [Space(10)]

    [Header("List of enemies spawned")]
    [HideInInspector]
    public List<GameObject> enemiesSpawnedList;

    List<GameObject> enemySphereList;

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

    [Space(10)]

    [Header("Maximum distance allowed to spawn a new enemy, if some enemy is near to some spawn")]
    public float distanceAllowed = 0.3F;

    [Space(10)]

    [Header("If can spawn a new enemy")]
    public float baseEnemyVelocity = 35F;
    public float baseEnemyVelocityAdd = 5F;

    [Space(10)]

    [Header("Time to destroy all enemies when player dies")]
    public float timeToDestroyEnemies = 3F;

    [Space(10)]

    [Header("Time to destroy all enemies when player dies")]
    public GameObject enemySphereGO;

    #endregion

    void Awake()
    {
        canSpawn = true;
        //level = Level.Easy;
        enemiesSpawnedList = new List<GameObject>();
        enemySphereList = new List<GameObject>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    void Update()
    {
        if (spawns != null && canSpawn /*&& player.alive*/)
            StartCoroutine(Spawn());
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

        yield return new WaitForSeconds(timeBetweenSpawns);
        canSpawn = true;
    }

    bool CheckEnemiesNear(GameObject spawnToCheck)
    {
        bool isNear = false;

        foreach (GameObject go in enemiesSpawnedList)
        {
            float distance = Vector3.Distance(go.transform.position, spawnToCheck.transform.position);

            if (distance <= distanceAllowed)
                isNear = true;
        }
		return isNear;
    }

    void ChangeEnemyVelocity(EnemyCollisions enemy)
    {
        if (level == Level.Easy)
            enemy.velocityEnemy = baseEnemyVelocity;

        else if (level == Level.Medium)
            enemy.velocityEnemy = baseEnemyVelocity + baseEnemyVelocityAdd;

        else if (level == Level.Hard)
            enemy.velocityEnemy = baseEnemyVelocity + 2 * baseEnemyVelocityAdd;

        else if (level == Level.Hell)
            enemy.velocityEnemy = baseEnemyVelocity + 3 * baseEnemyVelocityAdd;
    }

    void SpawnNewEnemy(GameObject[] enemiesV, GameObject[] spawnsG)
    {
        int indiceEnemy = Random.Range(0, enemiesV.Length);
        int indiceSpawn = Random.Range(0, spawnsG.Length);

        if (enemiesV.Length > 0)
        {
            EnemyCollisions enemyVel;

            try
            {
                enemyVel = enemiesV[indiceEnemy].GetComponent<EnemyCollisions>();
            }
            catch
            {
                enemyVel = enemiesV[indiceEnemy].GetComponentInParent<EnemyCollisions>();
            }

            ChangeEnemyVelocity(enemyVel);

            bool isEnemiesNearBy = CheckEnemiesNear(spawnsG[indiceSpawn]);

            if (isEnemiesNearBy)
            {
                List<GameObject> enemyAndSpawn = new List<GameObject>();

                enemyAndSpawn.Add(enemiesV[indiceEnemy]);
                enemyAndSpawn.Add(spawnsG[indiceSpawn]);

                StartCoroutine(CanSpawnNewEnemy(enemyAndSpawn));
            }
            else
                SpawnEnemy(enemiesV[indiceEnemy], spawnsG[indiceSpawn]);
        }
    }

    IEnumerator CanSpawnNewEnemy(List<GameObject> list)
    {
        yield return new WaitForSeconds(0.2F);

        bool isEnemiesNearBy = CheckEnemiesNear(list[1]);

        if (isEnemiesNearBy)
            StartCoroutine(CanSpawnNewEnemy(list));
        else
            SpawnEnemy(list[0], list[1]);
    }

    void SpawnEnemy(GameObject toEnemy, GameObject toSpawn)
    {
        GameObject newEnemySphere = Instantiate(enemySphereGO, enemySphereGO.transform.position, enemySphereGO.transform.rotation) as GameObject;
        newEnemySphere.name = "Enemy Sphere " + contEnemies;

        GameObject en = Instantiate(toEnemy, toSpawn.transform.position, toSpawn.transform.rotation) as GameObject;

        EnemySphere enSphere = newEnemySphere.GetComponent<EnemySphere>();

        if (en.name.Contains("Black"))
        {
            enSphere.velocity = 0F;
            en.name = "Enemy Black Hole " + contEnemies;
        }
        else
            en.name = "Enemy " + contEnemies;

        contEnemies++;

        enSphere.enemyChild = en;

        EnemyCollisions enCollisions = en.GetComponent<EnemyCollisions>();

        if (enCollisions.enemyType == EnemyType.BlackHole)
            enSphere.enemyType = "Black Hole";

        else if (enCollisions.enemyType == EnemyType.FollowPlayer)
            enSphere.enemyType = "Follow Player";

        else if (enCollisions.enemyType == EnemyType.Laser)
            enSphere.enemyType = "Laser";

        enemySphereList.Add(newEnemySphere);
        enemiesSpawnedList.Add(en);
    }

    public void DestroyAllEnemies()
    {
        foreach (GameObject go in enemiesSpawnedList)
        {
            if (go != null)
            {
                EnemyCollisions enColl = go.GetComponent<EnemyCollisions>();

                try
                {
                    enColl.anim.SetBool("Dead", true);
                }
                catch { }

                Destroy(enColl.gameObject, timeToDestroyEnemies);
            }
        }

        foreach (GameObject gameObj in enemySphereList)
        {
            Destroy(gameObj, timeToDestroyEnemies);
        }
    }
}