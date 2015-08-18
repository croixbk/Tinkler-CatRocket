using UnityEngine;

public enum EnemyType { BlackHole, FollowPlayer, Laser }

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    public float velocityEnemy = 15F;
    public ControlGenerator control;

    [HideInInspector]
    public Rigidbody _rigidbody;
    public Animator anim;

    Player player;

    public virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        control = GameObject.Find("Spawns").GetComponent<ControlGenerator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        try { anim = GetComponent<Animator>(); } catch { }
    }

    // All enemies behaviour
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (player == null)
                player = other.gameObject.GetComponent<Player>();

            if (player.hability == "Shield")
            {
                ShieldSkill shield = player.GetComponent<ShieldSkill>();
                shield.ShieldSetDamage();
            }
            else if (player.hability == "Invencibility")
            {
                // Destroy the enemy and start the animation
               // Destroy(gameObject);
            }
            else if (player.hability == "None" || player.hability == "Ray Sphere")
            {
                //player.alive = false;
                //control.DestroyAllEnemies();
            }

            //Colocar algum tipo de animação maneira logo abaixo, mas claro, não temos a animação maneira, mas pode ser uma "explosão" de partículas
        }
    }
}
