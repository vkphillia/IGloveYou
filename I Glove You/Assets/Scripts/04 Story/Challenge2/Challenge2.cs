using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Challenge2 : Challenge
{
  
	public GameObject UI;
	public Text filler;
	public Text enemyKilled;

	private int enemyCount;

	void Start ()
	{
		enemyKilled.text = "0";

		//all these things are not temporary now
		StartCoroutine (StartRound ());
	}



	IEnumerator StartRound ()
	{
		filler.text = "Challenge 2";
		yield return new WaitForSeconds (1f);
		filler.text = myLevelDesciption;
		yield return new WaitForSeconds (3f);
		filler.text = "3";
		yield return new WaitForSeconds (0.5f);
		filler.text = "2";
		yield return new WaitForSeconds (0.5f);
		filler.text = "1";
		yield return new WaitForSeconds (0.5f);
		filler.text = "";

		player.move = true;//enables player movement

		GameTimer.Instance.timerStarted = true;//starts timer
		enemyHolder.Spawn (1, true, true);
		Challenge.noOfEnemyAlive++;//increaing no of enemy available in scene

	}

	void Update ()
	{
		// 
		//timerStarted is set to false by GameTimer when time reaches 0
		if (player.health == 0 && GameTimer.Instance.timerStarted)
		{
			GameTimer.Instance.timerStarted = false;
			Debug.Log ("I stopped it");
		}
		else if (player.move == true && !GameTimer.Instance.timerStarted)
		{
			Debug.Log ("I stopped it too");
			StartCoroutine (StopRound ());
		}
	}
    
	//for stoping the player movement and showing challenge complete UI
	IEnumerator StopRound ()
	{
		player.move = false;
		Challenge.noOfEnemyAlive = 0;//reseting

		if (player.health == 0)
		{
			filler.text = "Success comes with great practice";
		}
		else
		{
			filler.text = "Congrats, You survived";
		}
        
		UI.SetActive (true);//setting challenge complete buttons to active
		yield return new WaitForSeconds (1f);

	}
    

	//   public override void SpawnEnemy ()
	//{
	//	base.SpawnEnemy ();
	//	StoryManager.Instance.myEnemy.GetComponent<SpriteRenderer> ().sprite = myEnemySprite;

	//}



	//public override void CheckForObjectiveComplete ()
	//{
	//	//Timer controller
	//	roundTimer -= Time.deltaTime;
	//	StoryManager.Instance.story.text = "Time: " + roundTimer.ToString ("N0");

	//	if (roundTimer <= 0)
	//	{
	//		//Times up and round is over
	//		CheckRoundStatus ();
	//	}
	//}

}
