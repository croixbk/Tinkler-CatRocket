using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldSkill : MonoBehaviour
{
    Player player;

    int shieldTotalLife = 1;
    int shieldActualLife;

    bool loadingShield = false;

    float time;
    public float timeTillRefillShield = 10F;

    public Image shieldRecover;
    public Image backgroundShield;

    void Awake()
    {
        player = GetComponent<Player>();

        shieldRecover.enabled = true;
        shieldRecover.fillMethod = Image.FillMethod.Horizontal;

        backgroundShield.enabled = true;
    }

    void Start()
    {
        SaveAndLoadData timeControl = GameObject.Find("Time Control").GetComponent<SaveAndLoadData>();

        for (int i = 0; i < timeControl.listAllSkills.Count; i++)
        {
            // Just to make persistance
            if (timeControl.listAllSkills[i].skillName == timeControl.informationsToSave.activeSkill)
            {
                shieldTotalLife = (timeControl.listAllSkills[i].level == 0) ? timeControl.listAllSkills[i].level + 1 : timeControl.listAllSkills[i].level;
                break;
            }
        }

        shieldActualLife = shieldTotalLife;
    }

    void Update()
    {
        if (loadingShield)
        {
            time += Time.deltaTime;

            if (time >= timeTillRefillShield)
            {
                time = 0;
                loadingShield = false;
                shieldActualLife = shieldTotalLife;
            }
        }

        shieldRecover.fillAmount = shieldActualLife / shieldTotalLife;
    }

    // Method that i will call on the enemy default script method OnCollisionEnter
    public void ShieldSetDamage()
    {
        shieldActualLife--;

        if (shieldActualLife == 0)
            loadingShield = true;

        else if (shieldActualLife < 0)
            player.alive = false;
    }
}