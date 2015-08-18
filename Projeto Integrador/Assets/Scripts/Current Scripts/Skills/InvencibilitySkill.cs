using UnityEngine;
using UnityEngine.UI;

public class InvencibilitySkill : MonoBehaviour
{
    public float invenTotalDuration = 3F;
    float invenActualTime;

    public float refillSpeed = 1.2F;
    public float depleteSpeed = 1.5F;

    [Space(10)]

    [Header("Base duration of the skill")]
    public float baseMultiplierLevel = 100F;

    string state;

    public Image lifeBar;
    public Image backgroundInvenciblity;

    Rect zone = new Rect(0.1F, 0.1F, 0.8F, 0.8F);
    Rect clickArea;

    // -----------------------

    [Space(10)]

    [Header("Time between double tap - To use skill")]
    public float timeBetweenTouches = 0.4F;
    float currentTimeBetweenTouches;
    bool enableDoubleTouchPossibility = false;

    void Awake()
    {
        try
        {
            lifeBar.enabled = true;
            lifeBar.fillMethod = Image.FillMethod.Horizontal;

            backgroundInvenciblity.enabled = true;
        }
        catch { }
    }

    void Start()
    {
        SaveAndLoadData timeControl = GameObject.Find("Time Control").GetComponent<SaveAndLoadData>();

        for (int i = 0; i < timeControl.listAllSkills.Count; i++)
        {
            // Just to make persistance
            if (timeControl.listAllSkills[i].skillName == timeControl.informationsToSave.activeSkill)
            {
                // Need some tests
                invenTotalDuration = (timeControl.listAllSkills[i].level == 0) ? baseMultiplierLevel : baseMultiplierLevel * timeControl.listAllSkills[i].level;
                break;
            }
        }

        invenActualTime = invenTotalDuration;
        state = "Disabled";

        #region Click area

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        clickArea = new Rect(zone.x * screenWidth, zone.y * screenHeight, zone.width * screenWidth, zone.height * screenHeight);

        #endregion
    }

    void Update()
    {
        #region Double tap verification

        if (Input.touchCount > 0)
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

        #region Skill state

        if (state == "Active")
        {
            invenActualTime -= Time.deltaTime * depleteSpeed;

            if (invenActualTime < 0)
                invenActualTime = 0;
        }
        else if (state == "Disabled")
        {
            invenActualTime += Time.deltaTime * refillSpeed;

            if (invenActualTime >= invenTotalDuration)
                invenActualTime = invenTotalDuration;
        }

        #endregion

        RefreshInterface();
    }

    // Call in player script
    public bool OnPress(Vector3 pos)
    {
        bool press = false;

        if (clickArea.Contains(pos))
        {
            if (state == "Active")
                state = "Disabled";
            else if (state == "Disabled")
                state = "Active";

            press = true;
        }
        return press;
    }

    void RefreshInterface()
    {
        lifeBar.fillAmount = invenActualTime / invenTotalDuration;
    }

    #region DrawTouchArea

    /*
       void OnGUI()
       {
           DrawTouchArea(clickArea, Color.red);
       }
    */

    /*
    void DrawTouchArea(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
    */

    #endregion
}