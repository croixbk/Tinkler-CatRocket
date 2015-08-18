using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class PlayerInformations
{
    public string activeSkill;
    public int xp;
}

public class SaveAndLoadData : MonoBehaviour
{
    // ------------------------------------------------------

    public GameObject sampleButton;

    public List<Skills> listAllSkills;

    private List<Skills> listSkillsToSave = new List<Skills>();

    public Transform skillsContentPanel;

    static int indice;
    static bool alreadyEquipped;

    // ------------------------------------------------------

    List<PlayerInformations> listPlayerInformations;

    // To verify what skill the player is using and to save
    [HideInInspector]
    public PlayerInformations informationsToSave;

    // ------------------------------------------------------

    [Space(10)]

    [Header("Controle de quanto será necessário de XP pra cada level")]
    public int baseXPPerLevel = 150;
    public int baseXPPerLevelWhenNotBuied = 200;

    // ------------------------------------------------------

    [Space(10)]

    [Header("Informações do player - Interface")]
    public Image imagePlayerCurrentEquippedSkill;
    public Text textPlayerCurrentXP;

    private int currentXP;

    // ------------------------------------------------------

    private void Awake()
    {
        if (informationsToSave == null)
            informationsToSave = new PlayerInformations();

        // Carregar as informações do player
        LoadFileWithPlayerInformations();

        // Carregar as informações de todas as skills
        LoadAllSkillsFileInformations();

        // Preencher HUD com as skills do arquivo
        PopulateListWithAllSkills();

        // Colocar a xp no script de Game Controller
        LoadPlayerInformations(listPlayerInformations);

        textPlayerCurrentXP.text = informationsToSave.xp + " XP";

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //Debug.Log("Skill ativa: " + informationsToSave.activeSkill);
        player.hability = informationsToSave.activeSkill;
    }

    void Update()
    {
        //DeleteFiles();
    }

    // Quando comprar a skill, não esquecer de incrementar o level em 1
    private void PopulateListWithAllSkills()
    {
        foreach (var skill in listAllSkills)
        {
            try
            {
                if (skill.skillName == informationsToSave.activeSkill)
                    skill.equiped = true;
                

                GameObject newButton = Instantiate(sampleButton) as GameObject;
                SampleButton button = newButton.GetComponent<SampleButton>();

                if (!skill.alreadyBuy)
                {
                    skill.equiped = false;
                    skill.level = 0;
                }

                button.name = skill.skillName;
                button.skillName.text = skill.skillName;

                //Debug.Log("Skills icons/" + skill.skillName + " - " + skill.level);

                button.icon.sprite = Resources.Load("Skills icons/" + skill.skillName + " - " + skill.level, typeof(Sprite)) as Sprite;

                button.index = indice++;

                // Colocar verificação com a skill atual do player
                if (skill.equiped)
                {
                    button.backgroundActionEquipOrUnequip.sprite = Resources.Load("Background button images/backgroundEquip", typeof(Sprite)) as Sprite;
                    button.textActionEquipOrUnequip.text = "Unequip";
                    informationsToSave.activeSkill = skill.skillName;
                }
                else
                {
                    button.backgroundActionEquipOrUnequip.sprite = Resources.Load("Background button images/backgroundBuy", typeof(Sprite)) as Sprite;
                    button.textActionEquipOrUnequip.text = "Buy";
                }

                if (skill.level >= skill.maxLevel)
                {
                    button.backgroundUpgrade.sprite = Resources.Load("Background button images/backgroundMaxLevel", typeof(Sprite)) as Sprite;
                    button.textUpgrade.text = "Max." +
                        "\nLevel";

                    button.textPrice.text = "Full upgraded!";
                }
                else
                {
                    button.backgroundUpgrade.sprite = Resources.Load("Background button images/backgroundLevel", typeof(Sprite)) as Sprite;
                    button.textUpgrade.text = "Level" +
                        "\n" + skill.level;

                    if (skill.level != 0)
                    {
                        skill.priceToBuy = skill.level * baseXPPerLevel;
                        button.textPrice.text = "Price: " + skill.priceToBuy + " XP";
                    }
                    else
                    {
                        skill.priceToBuy = baseXPPerLevelWhenNotBuied;
                        button.textPrice.text = "Price: " + skill.priceToBuy + " XP";
                    }

                }

                if (skill.alreadyBuy)
                {
                    // Verificar se já está equipada - VER DEPOIS
                    button.backgroundActionEquipOrUnequip.sprite = Resources.Load("Background button images/backgroundEquip", typeof(Sprite)) as Sprite;
                    button.textActionEquipOrUnequip.text = "Equip";
                }
                else
                {
                    button.backgroundActionEquipOrUnequip.sprite = Resources.Load("Background button images/backgroundBuy", typeof(Sprite)) as Sprite;
                    button.textActionEquipOrUnequip.text = "Buy";
                }

                if (skill.equiped)
                    button.textActionEquipOrUnequip.text = "Unequip";

                newButton.transform.SetParent(skillsContentPanel, false);
            }
            catch (System.NullReferenceException)
            {
                //Debug.Log("Deu ruim ao tentar colocar os botões na HUD");
            }
        }

        indice = 0;
    }

    private void DeleteFiles()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerInformations.dat"))
            {
                File.Delete(Application.persistentDataPath + "/PlayerInformations.dat");
                //Debug.Log("PlayerInformations excluído com sucesso!");
            }


            if (File.Exists(Application.persistentDataPath + "/SkillsInformations.dat"))
            {
                File.Delete(Application.persistentDataPath + "/SkillsInformations.dat");
                //Debug.Log("Skills excluído com sucesso!");
            }
        }
    }

    private void LoadFileWithPlayerInformations()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInformations.dat"))
        {
            var bFormatter = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/PlayerInformations.dat", FileMode.Open);

            if (listPlayerInformations == null || listPlayerInformations.Count == 0)
                listPlayerInformations = new List<PlayerInformations>();

            listPlayerInformations.Clear();

            listPlayerInformations = (List<PlayerInformations>)bFormatter.Deserialize(file);
            file.Close();

            //Debug.Log("Arquivo de informações do player carregado com sucesso!");

            LoadPlayerInformations(listPlayerInformations);
        }
        else
        {
            informationsToSave.xp = 0;
            informationsToSave.activeSkill = "None";

            if (listPlayerInformations == null || listPlayerInformations.Count == 0)
                listPlayerInformations = new List<PlayerInformations>();

            // Maybe just throw to somewhere
            listPlayerInformations.Add(informationsToSave);

            //Debug.Log("Deu como se o arquivo não existisse, então criei um com valores default!");

            SavePlayerInformations();
        }
    }

    private void LoadAllSkillsFileInformations()
    {
        if (File.Exists(Application.persistentDataPath + "/SkillsInformations.dat"))
        {
            var bFormatter = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/SkillsInformations.dat", FileMode.Open);

            listAllSkills.Clear();

            listAllSkills = (List<Skills>)bFormatter.Deserialize(file);
            file.Close();

            //Debug.Log("Arquivo de skills carregado com sucesso!");
        }
        else
            SaveSkillsInformations();
    }

    // Método criado para ser utilizado no momento que o player morrer
    public void UpdatePlayerXP(int xpToIncrease)
    {
        informationsToSave.xp += xpToIncrease;
        SavePlayerInformations();
    }

    public void LoadPlayerInformations(List<PlayerInformations> listPlayer)
    {
        GameController gameController = GameObject.Find("Time Control").GetComponent<GameController>();

        informationsToSave.xp = listPlayer[0].xp;
        informationsToSave.activeSkill = listPlayer[0].activeSkill;

        gameController.currentXPPlayer = informationsToSave.xp;

        //Debug.Log("Arquivo de informações do player carregado com sucesso no script de Game Controller!");
    }

    public void SavePlayerInformations()
    {
        var bFormatter = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/PlayerInformations.dat");

        if (listPlayerInformations == null || listPlayerInformations.Count == 0)
            listPlayerInformations = new List<PlayerInformations>();

        listPlayerInformations.Clear();

        listPlayerInformations.Add(informationsToSave);

        bFormatter.Serialize(file, listPlayerInformations);
        file.Close();

        //Debug.Log("Save player informations into file working!");
    }

    private void SaveSkillsInformations()
    {
        try
        {
            var bFormatter = new BinaryFormatter();
            var file = File.Create(Application.persistentDataPath + "/SkillsInformations.dat");

            listSkillsToSave.Clear();

            foreach (Skills skill in listAllSkills)
            {
                listSkillsToSave.Add(skill);
            }

            bFormatter.Serialize(file, listSkillsToSave);
            file.Close();

            //Debug.Log("Save skills informations into file working!");

        }
        catch (System.NullReferenceException) { Debug.Log("Deu ruim no save das skills!"); }
    }

    public void UpgradeSkill(SampleButton btn)
    {
        foreach (Skills sk in listAllSkills)
        {
            if (sk.priceToBuy <= currentXP)
            {
                if (btn.skillName.text == sk.skillName)
                {
                    if (sk.level < sk.maxLevel && sk.alreadyBuy)
                    {
                        sk.level += 1;

                        RefreshButtonUpgraded();
                        RefreshPlayerInterfaceInformations(sk);
                    }
                }
            }
        }
    }

    private void RefreshPlayerInterfaceInformations(Skills skill)
    {
        imagePlayerCurrentEquippedSkill.sprite = Resources.Load("Skills icons/" + skill.skillName + " - " + skill.level, typeof(Sprite)) as Sprite;

        currentXP -= skill.priceToBuy;
        informationsToSave.xp = currentXP;

        textPlayerCurrentXP.text = "" + currentXP;

        SavePlayerInformations();
        SaveSkillsInformations();
        LoadPlayerInformations(listPlayerInformations);
    }

    private void RefreshButtonUpgraded()
    {
        for (int i = 0; i < skillsContentPanel.childCount; i++)
        {
            Destroy(skillsContentPanel.GetChild(i).gameObject);
        }

        PopulateListWithAllSkills();
    }
}