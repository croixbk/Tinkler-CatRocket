using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour 
{
	ControlGenerator control;

	float timeSurvived;
	float timeEasy = 30F,timeMedium, timeHard;
	//float timeHell;

	Player player;

	int xpPerDecimals = 2;

	[HideInInspector]
	public int bonusXPTotal;

	float xpToGive;

	bool diedOnce = false;
	bool reachHellLevel = false;

	// -- Quantidade de itens bonus coletados para somar ao bonus final
	int countItemBonusXPCollected;

	void Start ()
	{
		timeMedium = timeEasy * 2;
		timeHard = timeEasy * 3;
		//timeHell = timeEasy * 4;

		control = GameObject.Find ("Spawns").GetComponent<ControlGenerator> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}

	void Update ()
	{
		try
		{
			if (player.alive) 
				CheckLevel();
			
			else 
				PlayerDied();
		}
		catch (System.NullReferenceException)
		{
			Debug.Log("Null reference");
		}
	}

	// Sendmessage function
	void ItemBonusXPCollected()
	{
		countItemBonusXPCollected++;
	}

	void BonusXP(bool levelChanged)
	{
		if (levelChanged) 
		{
			if (control.level == Level.Medium)
				xpToGive += 1000;

			else if (control.level == Level.Hard)
				xpToGive += 2000;

			else if (control.level == Level.Hell) 
				xpToGive += 3000;
		} 
		else 
			xpToGive += countItemBonusXPCollected * XPBonusToGiveDependingLevel();
	}

	int XPBonusToGiveDependingLevel()
	{
		int xp = 0;
		int baseXP =  100;

		if (control.level == Level.Easy) 
			xp = baseXP;

		 else if (control.level == Level.Medium) 
			xp = baseXP * 2;

		 else if (control.level == Level.Hard) 
			xp = baseXP * 3;

		else if (control.level == Level.Hell)
			xp = baseXP * 4;

		return xp;
	}

	void CheckLevel()
	{
		timeSurvived += Time.deltaTime;
		
		if (control.level == Level.Easy) 
		{
			if (timeSurvived >= timeEasy) 
			{
				control.level = Level.Medium;
				BonusXP (true);
			}
		} 
		else if (control.level == Level.Medium) 
		{
			if (timeSurvived >= timeMedium) 
			{
				control.level = Level.Hard;
				BonusXP (true);
			}
		} 
		else if (control.level == Level.Hard) 
		{
			if (timeSurvived >= timeHard) 
			{
				reachHellLevel = true;
				control.level = Level.Hell;
				BonusXP (true);
			}
		} 
		else if (control.level == Level.Hell) 
		{
			if (reachHellLevel) 
			{
				// Idk what to do here.. ZzzzZzzzZZz				
			}
		}
	}

	void PlayerDied()
	{
		if (diedOnce)
		{
			diedOnce = false;

			int numberLoops = 0;

			float numberWith2Decimals = Mathf.Round(timeSurvived * 10F) / 10F;

			for (float i = 0.1F; i <= numberWith2Decimals; i += 0.1F)
			{
				numberLoops ++;

				if (i == numberWith2Decimals)
				{
					BonusXP(true);

					xpToGive += (xpPerDecimals * numberLoops);
					bonusXPTotal += (int) xpToGive;
					break;
				}
			}
		}
	}
}
