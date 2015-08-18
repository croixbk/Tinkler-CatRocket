using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RaySphereSkill : MonoBehaviour
{
    Rect zone = new Rect(0.1F, 0.1F, 0.8F, 0.8F);
    Rect clickArea;

    List<GameObject> enemiesToKill;

    // -----------------------

    [Space(10)]

    [Header("Time between double tap - To use skill")]
    public float timeBetweenTouches = 0.4F;
    float currentTimeBetweenTouches;
    bool enableDoubleTouchPossibility = false;

    public ParticleSystem particle;

    #region Ray sphere skill power and radius variables

    [Space(10)]

    [Header("Ray sphere skill power and radius")]
    public float radius = 5F;
    public float power = 10F;

    #endregion

    void Start()
    {


        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        clickArea = new Rect(zone.x * screenWidth, zone.y * screenHeight, zone.width * screenWidth, zone.height * screenHeight);
    }

    void Update()
    {
        InputSkillCheck();
    }

    void InputSkillCheck()
    {
        #region Double tap verification

        if (Input.touches.Length != 0)
        {
            Touch touch = Input.GetTouch(0);

            if (OnPress(touch.position))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (enableDoubleTouchPossibility)
                    {
                        if (currentTimeBetweenTouches <= timeBetweenTouches)
                        {
                            currentTimeBetweenTouches = 0F;
                            enableDoubleTouchPossibility = false;
                            UseSkill();
                            goto OutIf;
                        }
                        else
                            enableDoubleTouchPossibility = true;
                    }
                }
            }

        OutIf:
            if (enableDoubleTouchPossibility)
            {
                currentTimeBetweenTouches += Time.deltaTime;

                if (currentTimeBetweenTouches >= timeBetweenTouches)
                {
                    currentTimeBetweenTouches = 0F;
                    enableDoubleTouchPossibility = false;
                }
            }
        }

        #endregion
    }

    void UseSkill()
    {
        # region Skill in distance

        /*
        enemiesToKill = new List<GameObject>();

        GameObject[] enemiesAround = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemiesAround.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, enemiesAround[i].transform.position);

            if (distance <= 5F)
            {
                // Lauch animation and kill the enemies
                enemiesToKill.Add(enemiesAround[i]);
            }
        }

        DestroyEnemiesAround(enemiesToKill);
        */

        #endregion

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider collider in colliders)
        {
            try
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Remove the scripts that makes the enemy follow the normal and also their behaviour
                    collider.gameObject.GetComponent<EnemySphere>().enabled = false;
                    collider.gameObject.GetComponent<EnemyCollisions>().enabled = false;

                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                }
            }
            catch (System.NullReferenceException) { }
        }
    }

    IEnumerator DestroyEnemiesAround(List<GameObject> en)
    {
        // Just to take some time do kill the enemies
        // I need to make this value comes from the active skill - 
        yield return new WaitForSeconds(0.5F);

        for (int i = 0; i < en.Count; i++)
        {
            Destroy(en[i]);
        }
    }

    public bool OnPress(Vector3 pos)
    {
        bool press = false;

        if (clickArea.Contains(pos))
            press = true;

        return press;
    }
}