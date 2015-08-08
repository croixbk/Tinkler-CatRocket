using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
	ControlGenerator control;

	[Space(10)]

	[Header("Paineis do canvas")]
	public GameObject canvas;
	public GameObject mainPanel;
	public GameObject creditsPanel;
	public GameObject gameOverPanel;

	float timeToDisappearMenu = 0.5F;

	void Start()
	{
		control = GameObject.Find("Control Generator").GetComponent<ControlGenerator>();
	}

	public void StartGame()
	{
		StartCoroutine("SecondaryPlay");
	}

	public void GameOverSadFace()
	{
		StartCoroutine("GameOver");
	}

	public void Credits() 
	{ 
		StartCoroutine("CreditsTeam");
	}

	IEnumerator CreditsTeam()
	{
		yield return new WaitForSeconds(timeToDisappearMenu);
		
		//gameOverPanel.SetActive (false);
		control.canSpawn = false;
		mainPanel.SetActive (false);
		creditsPanel.SetActive (true);
		canvas.SetActive (true);
	}

	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(timeToDisappearMenu);

		control.canSpawn = false;
		creditsPanel.SetActive (false);
		mainPanel.SetActive (false);
		canvas.SetActive (true);
		//gameOverPanel.SetActive (true);
	}

	IEnumerator Play()
	{
		yield return new WaitForSeconds(timeToDisappearMenu);
		creditsPanel.SetActive (false);
		mainPanel.SetActive (false);
		canvas.SetActive (false);
		//gameOverPanel.SetActive (false);
		control.canSpawn = true;
	}

	IEnumerator SecondaryPlay(){

		yield return new WaitForSeconds (timeToDisappearMenu);

		GameObject.Find("Controller").SendMessage("StartGame");
		mainPanel.SetActive (false);
	}
}
