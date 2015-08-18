using UnityEngine;
using System.Collections;

public class DistanceToAtom : MonoBehaviour
{
    public float distance;
    public static float atomDistance;
    GameObject core;

    void Awake()
    {
        core = GameObject.Find("Core");
    
        if (distance > 0)
            atomDistance = distance;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, core.transform.position, out hit, 14))
            transform.up = hit.normal;
    }
}
