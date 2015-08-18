using UnityEngine;
using System.Collections;

public class DistanceToAtom : MonoBehaviour
{
    public float distance;
    public static float atomDistance;
    public GameObject atom;
	Vector3 movePosition;

    void Start()
    {
		movePosition = transform.position;
		movePosition.y += distance; 
        atom = GameObject.Find("Atom");
    
        if (distance > 0)
            atomDistance = distance;

        RaycastHit hit;

		if (Physics.Raycast (transform.position, atom.transform.position - transform.position, out hit, atom.gameObject.layer)) {
			transform.up = hit.normal;
			print("aaa");
			transform.position = hit.point;
			print(transform.name +" "+hit.transform.name);
			//transform.position = transform.up*distance; 
		}
    }
}
