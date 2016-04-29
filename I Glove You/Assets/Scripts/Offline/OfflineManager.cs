using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void SpawnPlayers ();
public class OfflineManager : MonoBehaviour
{

	//Static Singleton Instance
	public static OfflineManager _Instance = null;

	//property to get instance
	public static OfflineManager Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{                                                                                   
				_Instance = (OfflineManager)FindObjectOfType (typeof(OfflineManager));
			}
			return _Instance;
		}	
	}

	public static event SpawnPlayers spawnPlayers;


	public bool Mute;
	
	//scripts link
	//public PlayerHolderController PlayerHolder1;
	//public PlayerHolderController PlayerHolder2;
	public OfflineRoundController RoundPanel;



	public GameObject Player1HUDPanel;
	public GameObject Player2HUDPanel;

	public Transform foreground;

	//public  GameState currentState;
    
	//	public bool glovePicked;
	//public bool PUPicked;

	public int roundNumber;
	public int MaxHealth;
	public float MaxSpeed;
    
	//public float MaxRoundTimer;

	//public Text roundText_HUD;
	//public Text timerText_HUD;
	
	private Vector3 P1StartPos;
	private Vector3 P2StartPos;
	//private float roundTimer;

	public bool Pause;
	public Text PauseText;

	public P1Trophy TrophyP1;
	public P2Trophy TrophyP2;

	public PUController myPUController;

	//for rematch
	private bool P1Ready;
	private bool P2Ready;
	public Text P1ReadtText;
	public Text P2ReadtText;

	public GameObject pauseBtn;


	//player ui stuff
	public Image[] healthBars;
	public Transform[] flyingTextSpawnPoint;
	public Text[] winText_HUD;
	public Text[] healthText_HUD;
	public Sprite[] punchSprites;


	//sets GameState to RoundStart and sets the sprite for both player
	void OnEnable ()
	{
		//tells GameManager to spawn players
		if (spawnPlayers != null)
		{
			spawnPlayers ();
		}

		GameManager.Instance.currentState = GameState.RoundStart;

		GameManager.Instance.players [0].GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.players [0].mySprites [OfflineMenuController.Player1CharacterID];
		GameManager.Instance.players [1].GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.players [1].mySprites [OfflineMenuController.Player2CharacterID];


	}



	//sets the player intital position and calls ShowRoundPanel()
	void Start ()
	{
		SoundsController.Instance.PlayCrowdSound ();

		//foreground.transform.localScale = new Vector3 (.8f, 0.8f, 1);
        
		//RoundPanel.gameObject.SetActive(true);
		RoundPanel.ShowRoundPanel ();
        
	}
    
	//check for escape button click, spawn gloves and power ups, controls timer, checks round status
	void Update ()
	{
		

		if (GameManager.Instance.currentState == GameState.Fight)
		{
			ZoomIn ();
		}
		else if (GameManager.Instance.currentState == GameState.RoundOver || GameManager.Instance.currentState == GameState.MatchOver)
		{
			
			ZoomOut ();
			myPUController.glove.SetActive (false);
			myPUController.PU.SetActive (false);
			GameManager.Instance.PUPicked = true;
			if (P1Ready && P2Ready)
			{
				P1Ready = false;
				P2Ready = false;
				SceneManager.LoadScene ("offline game");
			}
		}


	}




	//camera zoom code
	void ZoomIn ()
	{
		Player1HUDPanel.SetActive (true);
		Player2HUDPanel.SetActive (true);

		//if (foreground.transform.localScale.x < 1)
		//{
		//	foreground.transform.localScale += new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		//}
	}

	public void ZoomOut ()
	{
		
		

		//Player1HUDPanel.SetActive (false);
		//Player2HUDPanel.SetActive (false);

		//if (foreground.transform.localScale.x > 0.8f)
		//{
		//	foreground.transform.localScale -= new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		//}
	}

	//checks for the winner and sets the GameState to MatchOver or RoundOver
	//any call for stoping the game should be sent here
	public void CheckRoundStatus ()
	{
		if (GameManager.Instance.players [0].myHealth > GameManager.Instance.players [1].myHealth)
		{
			GameManager.Instance.players [0].roundWins++;
			TrophyP1.gameObject.SetActive (true);
		}
		else if (GameManager.Instance.players [1].myHealth > GameManager.Instance.players [0].myHealth)
		{
			GameManager.Instance.players [1].roundWins++;
			TrophyP2.gameObject.SetActive (true);

		}
		else if (GameManager.Instance.players [1].myHealth == GameManager.Instance.players [0].myHealth)
		{
			if (GameManager.Instance.players [0].hasGlove)
			{
				GameManager.Instance.players [0].roundWins++;
				TrophyP1.gameObject.SetActive (true);

			}
			else if (GameManager.Instance.players [1].hasGlove)
			{
				GameManager.Instance.players [1].roundWins++;
				TrophyP2.gameObject.SetActive (true);

			}
		}
		
		if (GameManager.Instance.players [0].roundWins == 2 || GameManager.Instance.players [1].roundWins == 2)
		{
			GameManager.Instance.currentState = GameState.MatchOver;
		}
		else
		{
			if (GameManager.Instance.currentState != GameState.MatchOver)
			{
				GameManager.Instance.currentState = GameState.RoundOver;
			}
		}
		
		RoundPanel.ShowRoundPanel ();
	}

	//sets the players intital positions, timer and calls for SpawnGlove()
	public void StartNewRound ()
	{
		//roundTimer = MaxRoundTimer;
		//code for timer
		//GetComponentInChildren<ProgressBar> ().SetUpdateBar ((int)roundTimer);
		roundNumber++;
		GameManager.Instance.players [0].transform.localPosition = new Vector3 (0, -3, 0);
		GameManager.Instance.players [0].transform.rotation = Quaternion.identity;
		GameManager.Instance.players [0].ResetPlayer ();

		GameManager.Instance.players [1].transform.localPosition = new Vector3 (0, 3, 0);
		GameManager.Instance.players [1].transform.rotation = Quaternion.Euler (0, 0, 180);
		GameManager.Instance.players [1].ResetPlayer ();

		//roundText_HUD.text = "Round: " + OfflineManager.Instance.roundNumber;

		//if (GameManager.SpwanFirstGlove != null)
		//{
		//          GameManager.SpwanFirstGlove();
		//}
		GameManager.Instance.NewRound ();
	}
    
	//sets the roundWins to 0 for both player
	public void NewMatchStart ()
	{
		GameManager.Instance.players [0].roundWins = 0;
		GameManager.Instance.players [1].roundWins = 0;
	}

	public void OnMenuClick ()
	{
		SceneManager.LoadScene ("offline menu");
	}

	public void OnReMatchClickP1 ()
	{
		P1Ready = true;
		P1ReadtText.text = "Ready!";


	}

	public void OnReMatchClickP2 ()
	{
		P2Ready = true;
		P2ReadtText.text = "Ready!";

	}

	//plays the sound that is passed in as an argument //Deprecated
	//public void PlaySound (AudioSource a)
	//{
	//	if (!Mute)
	//	{
	//		a.Play ();
	//	}
	//}


	public void OnPauseBtn ()
	{
		if (!Pause)
		{
			Pause = true;

			Time.timeScale = 0;

			PauseText.text = "Paused";
		}
		else
		{
			Pause = false;
			Time.timeScale = 1;
			PauseText.text = "II";
		}
	}
}
