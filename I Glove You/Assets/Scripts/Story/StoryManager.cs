using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum StoryGameState
{
	ChallengeSelect,
	RoundStart,
	Fight,
	Playing,
	Paused,
	RoundOver,
	MatchOver}
;

public class StoryManager : MonoBehaviour
{
	

	//Static Singleton Instance
	public static StoryManager _Instance = null;

	//property to get instance
	public static StoryManager Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{                                                                                   
				_Instance = (StoryManager)FindObjectOfType (typeof(StoryManager));
			}
			return _Instance;
		}	
	}

	[SerializeField]
	private bool
		cleanData = false;

	public PlayerControlsUniversal playerControl;
	public Enemy myEnemy;
	public PlayerControlsUniversal myPlayer;
	public StoryGameState currentState = StoryGameState.ChallengeSelect;
	public GameObject ChallengeTowerPanel;
	public StoryRoundController RoundPanel;
	[HideInInspector]
	public Challenge currentChallenge;



	public Text story;

	// Use this for initialization
	void Start ()
	{
		PlayerData.Instance.ReadData ();
		if (cleanData)
		{
			Debug.Log ("is cleaning Data");
			PlayerData.Instance.CleanData (cleanData);
			PlayerData.Instance.SaveKey ("tutorialDone", 0);
			cleanData = false;
			PlayerData.Instance.SaveKey ("CleanData", (cleanData ? 0 : 0));
		}
		if (PlayerData.Instance.TutorialDone == 0)
		{
			StartCoroutine (GameIntro ());
		}
		else
		{
			//Show challange tower since tutorial is already done
			ChallengeTowerPanel.SetActive (true);
		}
	}

	IEnumerator GameIntro ()
	{
		story.text = "";
		yield return new WaitForSeconds (2f);
		story.text = "Hi";
		yield return new WaitForSeconds (2f);
		/*story.text = "How are you?";
		yield return new WaitForSeconds (2f);
		story.text = "This is your player";
		yield return new WaitForSeconds (3f);
		story.text = "Enough watching\n Let's learn player movements";
		yield return new WaitForSeconds (4f);
		story.text = "Press key Z or tap left to rotate player in anticlockwise direction";
		yield return new WaitForSeconds (5.5f);
		story.text = "Great";
		yield return new WaitForSeconds (1f);
		story.text = "Press key X or tap right to rotate player in clockwise direction";
		yield return new WaitForSeconds (5.5f);
		story.text = "Great";
		yield return new WaitForSeconds (1.5f);
		story.text = "Now dont worry about the movements, we got it covered for you";
		yield return new WaitForSeconds (4.5f);
		story.text = "See";
		playerControl.move = true;
		yield return new WaitForSeconds (1f);
		story.text = "";
		yield return new WaitForSeconds (7f);
		story.text = "Congats, you learned about your player movements";
		playerControl.move = false;
		yield return new WaitForSeconds (4f);
		story.text = "Come back later for more :)";*/
		PlayerData.Instance.TutorialDone = 1; //set to true

		//Show challange tower
		ChallengeTowerPanel.SetActive (true);

	}


	void Update ()
	{
		if (StoryManager.Instance.currentState == StoryGameState.Playing)
		{
			//Debug.Log ("playing");
			currentChallenge.CheckForObjectiveComplete ();
		}
	}
}
