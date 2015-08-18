using UnityEngine;
using System.Collections;

public class EnemyCollisions : Enemy
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
