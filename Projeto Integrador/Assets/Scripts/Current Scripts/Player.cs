using UnityEngine;

public class Player : MonoBehaviour
{
    public string hability = "None";

    [HideInInspector]
    public bool alive;

    [HideInInspector]
    public int score;

    [HideInInspector]
    public Rigidbody _rigidbody;

    [HideInInspector]
    public bool getCaught = false;

    bool diedOnce = false;

    //Animator _animator;
    Camera mainCam;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        mainCam = Camera.main;

        try
        {
       //     _animator.GetComponent<Animator>();
        }
        catch { }
    }

    void Start()
    {
        SaveAndLoadData timeControl = GameObject.Find("Time Control").GetComponent<SaveAndLoadData>();
        hability = timeControl.informationsToSave.activeSkill;

        if (hability == "Shield")
            gameObject.AddComponent<ShieldSkill>();

        else if (hability == "Invencibility")
            gameObject.AddComponent<InvencibilitySkill>();

        else if (hability == "Ray Sphere")
            gameObject.AddComponent<RaySphereSkill>();

        alive = true;
    }

    void Update()
    {
        if (!alive)
        {
            Debug.Log("Ai não");
            if (!diedOnce)
            {
                diedOnce = true;
                SaveAndLoadData timeControl = GameObject.Find("Time Control").GetComponent<SaveAndLoadData>();
                timeControl.UpdatePlayerXP(score);

                // Death animation

                try
                {
               //     _animator.SetBool("Dead", true);
                }
                catch { }

              //  Invoke("DestroyPlayer", 1.5F);
            }
        }

        // If player gets caught by the black hole
        if (getCaught)
        {

        }
    }

    void DestroyPlayer()
    {
        Debug.Log("Chamei o método pra matar o player!");
        mainCam.transform.parent = null;
        Destroy(gameObject);
    }
}