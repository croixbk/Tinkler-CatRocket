using UnityEngine;

public enum ComponentType {	Background,	Trail, Personagem }

public class RendererAnimation: MonoBehaviour 
{
	public ComponentType component;

	Vector2 uvAnimationRate = new Vector2(0.1F, 0);

	[HideInInspector]
	public Vector2 uvOffset = Vector2.zero;

	Renderer rend;
	TrailRenderer trailRend;
	
	void Awake() 
	{
		if (component == ComponentType.Background) 
			rend = GetComponent < Renderer > ();

		else if (component == ComponentType.Trail) 
			trailRend = GetComponent < TrailRenderer > ();

		else if (component == ComponentType.Personagem) 
		{
			uvAnimationRate = new Vector2 (12F, 12F);
			rend = GetComponent < Renderer > ();
		}
	}
	
	void LateUpdate() 
	{
		ChangeOffset();
	}

	void ChangeOffset() 
	{
		uvOffset += (uvAnimationRate * Time.deltaTime);

		if (component == ComponentType.Background) 
			rend.material.SetTextureOffset("_MainTex", uvOffset);

		else if(component == ComponentType.Trail) 
			trailRend.material.SetTextureOffset("_MainText", uvOffset);
	}
}