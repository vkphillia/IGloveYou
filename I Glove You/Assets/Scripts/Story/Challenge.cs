using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public delegate void StoryGloveEvent ();
public class Challenge : MonoBehaviour
{

	
	//Static Singleton Instance
	public static Challenge _Instance = null;

	//property to get instance
	public static Challenge Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{                                                                                   
				_Instance = (Challenge)FindObjectOfType (typeof(Challenge));
			}
			return _Instance;
		}	
	}

	public static event StoryGloveEvent SpwanFirstGlove;

	public static int noOfEnemyAlive;
	//temporary, can be removed later

	//challenge stuff
	[HideInInspector]
	public Button myButton;


	[HideInInspector]
	public int roundNumber;


	[HideInInspector]
	public float roundTimer;
	//public Text roundText_HUD;
	//public Text timerText_HUD;
	[HideInInspector]
	public bool glovePicked;
	[HideInInspector]
	public bool PUPicked;

	//Set in inspector for each challenge
	public int myLevelNum;
	public string myLevelDesciption;
	public int NoOfEnemies;
	//enemy stuff
	public EnemyHolder enemyHolder;
	public Sprite myEnemySprite;
	public float myEnemyMaxSpeed;
	public int myEnemyMaxHealth;
	public bool enemyHasGlove;
	public bool AIOn;
	//public GameObject myPUController;
	public bool PUOn;
	public bool GloveOn;
	//player stuff
	public PlayerControlsUniversal player;
	public float myPlayerMaxSpeed;
	public int myPlayerMaxHealth;
	public bool playerHasGlove;
	//round stuff
	public int NoOfRounds;
	public float MaxRoundTimer;



	public virtual void Awake ()
	{
		myButton = GetComponent<Button> ();
		roundTimer = MaxRoundTimer;
		Initialize ();

	}



	public virtual void StartNewRound ()
	{
		roundTimer = MaxRoundTimer;
		roundNumber++;
		if (SpwanFirstGlove != null)
		{
			SpwanFirstGlove ();
		}
	}

	public virtual void CheckForObjectiveComplete ()
	{
		//Debug.Log(Check);
	}


	public virtual void CheckRoundStatus ()
	{
		
		if (roundNumber == NoOfRounds)
		{
			StoryManager.Instance.currentState = StoryGameState.MatchOver;
		}
		else
		{
			StoryManager.Instance.currentState = StoryGameState.RoundOver;
		}
		
		StoryManager.Instance.RoundPanel.gameObject.SetActive (true);
	}


	//public virtual void SpawnEnemy ()
	//{
	//	if (NoOfEnemies > 0)
	//	{
	//		StoryManager.Instance.myEnemy.Initialize ();
	//	}

	//}

	//public virtual void SpawnPlayer ()
	//{
	//	StoryManager.Instance.myPlayer.Initialize ();
	//}

	public void Initialize ()
	{
		//Set properties for enemy and player
		enemyHolder.enemy.hasGlove = enemyHasGlove;
		player.hasGlove = playerHasGlove;
		player.maxHealth = myPlayerMaxHealth;
		GameTimer.Instance.totalTime = MaxRoundTimer;
	}


}
