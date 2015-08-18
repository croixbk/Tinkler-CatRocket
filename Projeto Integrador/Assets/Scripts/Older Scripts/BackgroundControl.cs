using UnityEngine;
using System.Collections;

public class BackgroundControl : MonoBehaviour 
{
	/*
	[Space(10)]

	public GameObject upperSpawn;
	public GameObject currentBackground;

	float rangeToSpawnAndDestroy = 9F;

	GameObject lastBackground, lastUpperSpawn;

	bool canSpawnNextBackground = false;
	int contBackground;

	void Awake () 
	{
		canSpawnNextBackground = false;
	}

	void Start()
	{
		SpawnConf();
	}

	void Update () 
	{
		SpawnNext();
		DestroyNext();
	}

	void SpawnNext()
	{
		if (canSpawnNextBackground && currentBackground != null)
		{
			if (transform.position.z - rangeToSpawnAndDestroy <= currentBackground.transform.position.z)
			{
				SpawnConf();
			}
		}
	}

	void SpawnConf()
	{
		canSpawnNextBackground = false;

		float distanceToAdd = Vector3.Distance(currentBackground.transform.position, upperSpawn.transform.position);

		Vector3 positionToSpawn = new Vector3(upperSpawn.transform.position.x, upperSpawn.transform.position.y, upperSpawn.transform.position.z + distanceToAdd);
		
		GameObject go = Instantiate(currentBackground, positionToSpawn, currentBackground.transform.rotation) as GameObject;
		go.name = "Background " + contBackground;
		contBackground ++;

		lastBackground = currentBackground;
		currentBackground = go;

		BackgroundInformations backInfo = currentBackground.GetComponent<BackgroundInformations>();

		lastUpperSpawn = upperSpawn;
		upperSpawn = backInfo.upperSpawn;
		
		RendererAnimation rendAnimLast = lastBackground.GetComponent<RendererAnimation>();
		RendererAnimation rendAnimCurrent = currentBackground.GetComponent<RendererAnimation>();
		
		rendAnimCurrent.uvOffset = rendAnimLast.uvOffset;
	}
	
	void DestroyNext()
	{
		if (lastBackground != null && !canSpawnNextBackground)
		{
			if (transform.position.z >= lastUpperSpawn.transform.position.z + rangeToSpawnAndDestroy)
			{
				canSpawnNextBackground = true;
				GameObject go = lastBackground;
				Destroy(go);
			}
		}
	}*/
}
