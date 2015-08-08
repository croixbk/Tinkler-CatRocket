using UnityEngine;
using System.Collections;

public class BackgroundControl : MonoBehaviour {

	[Space(10)]
	public GameObject upperSpawn;
	public GameObject bottomSpawn;
	public GameObject currentBackground;
	GameObject lastBackground;

	bool canSpawnNextBackground = false;

	int contBackground;

	public float rangeToSpawnAndDestroy = 5F;

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
		if (canSpawnNextBackground)
		{
			if (currentBackground != null)
			{
				if (transform.position.x + rangeToSpawnAndDestroy <= currentBackground.transform.position.x)
				{
					SpawnConf();
				}
			}
		}
	}

	void SpawnConf()
	{
		canSpawnNextBackground = false;
		
		//upperSpawn.transform.parent = null;
		
		Vector3 positionToSpawn = new Vector3(upperSpawn.transform.position.x, upperSpawn.transform.position.y, upperSpawn.transform.position.z * 2);
		
		GameObject go = Instantiate(currentBackground, positionToSpawn, currentBackground.transform.rotation) as GameObject;
		go.name = "Background" + contBackground;
		contBackground ++;
		
		BackgroundInformations backInfo = go.GetComponent<BackgroundInformations>();
		
		lastBackground = currentBackground;
		currentBackground = go;
		
		upperSpawn = backInfo.upperSpawn;
		bottomSpawn = backInfo.bottomSpawn;
		
		RendererAnimation rendAnimLast = lastBackground.GetComponent<RendererAnimation>();
		RendererAnimation rendAnimCurrent = currentBackground.GetComponent<RendererAnimation>();
		
		rendAnimCurrent.uvOffset = rendAnimLast.uvOffset;
	}
	
	void DestroyNext()
	{
		if (lastBackground != null)
		{
			if (transform.position.x - rangeToSpawnAndDestroy >= lastBackground.transform.position.x)
			{
				canSpawnNextBackground = true;
				Destroy(lastBackground);
			}
		}
	}
}
