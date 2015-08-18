using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 targetPosition;

    public float timeToSmooth = 0.4F;
    public float maxDistance;
    public GameObject target;
    public GameObject sphere;

    //Vector3 reference = Vector3.zero;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
}
