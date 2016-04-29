using UnityEngine;
using System.Collections;

public enum GameState
{
	RoundStart,
	Fight,
	Playing,
	Paused,
	RoundOver,
	MatchOver}
;

public enum GameMode
{
	TwoPlayer,
	SinglePlayer}
;

public delegate void GloveEvent ();
public delegate void PlayerSpawn ();

public class GameManager : MonoBehaviour
{
	//Static Singleton Instance
	public static GameManager _Instance = null;

	//property to get instance
	public static GameManager Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{
				_Instance = (GameManager)FindObjectOfType (typeof(GameManager));
			}
			return _Instance;
		}
	}

	public static event GloveEvent SpwanFirstGlove;
	public static event PlayerSpawn GetPlayerNo;

	public GameState currentState;
	public GameMode currentMode;

	[HideInInspector]
	public bool PUPicked;

	[HideInInspector]
	public bool glovePicked;

	[HideInInspector]
	public int ChallengeLevel;

	public PlayerHolderController playerPrefab;

	public PlayerHolderController[] players;

	public ChallengeDetail myChallenge;

	void Awake ()
	{
		OfflineManager.spawnPlayers += spawnFighters;
		PUPicked = true;
		if (_Instance != null && _Instance != this)
		{
			Destroy (this.gameObject);
		}
		else
		{
			_Instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void NewRound ()
	{
		if (SpwanFirstGlove != null)
		{
			SpwanFirstGlove ();
		}
	}

	void spawnFighters ()
	{
        //a for loop could have been added for this stuff
		players = new PlayerHolderController[2];
		players [0] = Instantiate (playerPrefab, new Vector3 (0, -3, 0), Quaternion.identity)as PlayerHolderController;
		players [0].playerNo = 0;
		players [0].gameObject.layer = 8 + players [0].playerNo;//makes it 8, this will help when we have multie enemies in story
		players [0].myGloveTrigger.layer = 8 + players [0].playerNo;//makes it 8, this will help when we have multie enemies in story

		Console.Log (players [0].playerNo);

		players [1] = Instantiate (playerPrefab, new Vector3 (0, 3, 0), Quaternion.identity)as PlayerHolderController;
		players [1].playerNo = 1;
		players [1].gameObject.layer = 8 + players [1].playerNo; //makes it 9
		players [1].myGloveTrigger.layer = 8 + players [1].playerNo;//makes it 9, this will help when we have multie enemies in story

		Console.Log (players [1].playerNo);

		//this will tell playermovementController to cache player number
		if (GetPlayerNo != null)
		{
			GetPlayerNo ();
		}


	}

	void OnDestroy ()
	{
		OfflineManager.spawnPlayers -= spawnFighters;
	
	}
}
