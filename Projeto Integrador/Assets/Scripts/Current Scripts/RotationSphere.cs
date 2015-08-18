using UnityEngine;
using System.Collections;

public class RotationSphere : MonoBehaviour
{
    public float velocity;
    public float outVelocity;
    public float dragVelocity;
    float outDragVelocity;
    public float dragFactor;
    float initialDragV;
    Quaternion rotationPosition;
    bool hasTouched;
    public GameObject edgeCube;
    public LayerMask layerMask;
    public LayerMask edgeCubeLM;
    GameObject player;
    Vector3 lastInputPosition;

    void Awake()
    {
        player = GameObject.Find("Player");
        transform.rotation = Quaternion.FromToRotation(transform.up, player.transform.position - transform.position) * transform.rotation;
        player.transform.parent = transform;
        initialDragV = dragVelocity;
        hasTouched = false;
        outDragVelocity = dragVelocity - 1.5f;
    }

    void Update()
    {
#if UNITY_EDITOR
        MouseMovement();
#else
            TouchMovement();
#endif
    }

    void MovementEdgeCube(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, edgeCubeLM))
            edgeCube.transform.position = hit.point;
    }

    void MouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            hasTouched = true;
            dragVelocity = initialDragV;
            outDragVelocity = initialDragV - 1.5f;
            SphereRaycasts(Input.mousePosition, velocity, outVelocity);
        }
        else if (hasTouched)
        {
            SphereRaycasts(lastInputPosition, dragVelocity, outDragVelocity);
            DragVelocityDecrase();
        }
    }

    //-------- O MouseMovement funciona normalmente no celular (Não precisa do touch), mas vai que... --------//

    void TouchMovement()
    {
        if (Input.touches.Length != 0)
        {
            foreach (Touch touch in Input.touches)
            {
                hasTouched = true;
                outDragVelocity = initialDragV - 1.5f;
                dragVelocity = initialDragV;
                SphereRaycasts(touch.position, velocity, outVelocity);
            }
        }
        else if (hasTouched)
        {
            SphereRaycasts(lastInputPosition, dragVelocity, outDragVelocity);
            DragVelocityDecrase();
        }
    }

    void SphereRaycasts(Vector3 input, float v, float ov)
    {
        if (player != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(input);
            RaycastHit hit;
            lastInputPosition = input;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name.Equals("Plane"))
                {
                    MovementEdgeCube(ray);
                    //Debug.DrawLine(edgeCube.transform.position, transform.position, Color.red, Mathf.Infinity, false);

                    if (Physics.Linecast(edgeCube.transform.position, transform.position, out hit, layerMask))
                        rotationPosition = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, ov * Time.deltaTime);
                }
                else
                    rotationPosition = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, v * Time.deltaTime);
            }
            transform.rotation = rotationPosition;
        }
    }

    void DragVelocityDecrase()
    {
        dragVelocity -= dragFactor * Time.deltaTime;
        outDragVelocity -= dragFactor * Time.deltaTime;
    }
}
