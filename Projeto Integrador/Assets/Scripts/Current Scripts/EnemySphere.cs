using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySphere : MonoBehaviour
{
    public float pullSpeed;
    public float pullDistance;
    public float velocity;
    public float killDistance;
    public string enemyType;
    public bool isActive;
    public GameObject enemyChild;
    public GameObject playerGO;
    GameObject playerRS;
    public ControlGenerator enemyGenerator;

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerRS = playerGO.transform.parent.gameObject;

        isActive = true;

        transform.rotation = Quaternion.FromToRotation(transform.up, enemyChild.transform.position - transform.position); // coloca seu up em relaçao ao inimigo que ira controlar

        enemyChild.transform.parent = transform;
        enemyGenerator = playerGO.GetComponentInChildren<ControlGenerator>();
    }

    void Update()
    {
        if (isActive && enemyChild != null)
        {
            switch (enemyType)
            {
                case "Black Hole":
                    BlackHoleBehaviour();
                    break;

                case "Follow Player":
                    FollowPlayer();
                    break;

                case "Teste3":
                    CheckEnemiesNear();
                    break;
            }
        }
    }

    void BlackHoleBehaviour()
    {
        //transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0), Space.Self);
        transform.Rotate(transform.up, 30 * Time.deltaTime);
        CheckEnemiesNear();
    }

    void FollowPlayer()
    {
        RotateToRotation(playerRS.transform.rotation);
    }

    void RotateToRotation(Quaternion rotation)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, velocity * Time.deltaTime);
    }

    void CheckEnemiesNear()
    {
		velocity = 10;
        float distance;
        EnemySphere sphere;
        //RotateToRotation (playerRS.transform.rotation);

        try
        {
            foreach (GameObject go in enemyGenerator.enemiesSpawnedList)
            {
                if (!go.transform.name.Equals(enemyChild.transform.name))
                {
                    distance = Vector3.Distance(enemyChild.transform.position, go.transform.position);

                    if (distance < pullDistance)
                    {
                        sphere = go.GetComponentInParent<EnemySphere>();

                        sphere.isActive = false;

                        sphere.transform.RotateAround(enemyChild.transform.position, transform.up, velocity);
                        sphere.transform.rotation = Quaternion.Slerp(sphere.transform.rotation, transform.rotation, pullSpeed * Time.deltaTime);
                        
                        if (distance < killDistance)
                        {
                            Destroy(sphere.gameObject, 0.2F);
                            Destroy(go.transform.parent.gameObject, 0.2F);
                            enemyGenerator.enemiesSpawnedList.Remove(go);
                            enemyGenerator.enemiesSpawnedList.Remove(go.transform.parent.gameObject);
                        }
                    }
                }
            }
        }
        catch { }
    }

    
    public void RotateToRotation(Quaternion rotation, float v)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, v * Time.deltaTime);
    }
    
}
