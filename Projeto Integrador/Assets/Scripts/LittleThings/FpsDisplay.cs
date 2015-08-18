using UnityEngine;
using UnityEngine.UI;

public class FpsDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;
	string text;
	Text textArea;

	void Start()
    {
		textArea = GetComponent<Text> ();
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		textArea.text = text;
	}
}