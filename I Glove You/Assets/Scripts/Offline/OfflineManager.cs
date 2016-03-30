using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public delegate void GloveEvent ();
public enum GameState
{
	RoundStart,
	Fight,
	Playing,
	Paused,
	RoundOver,
	MatchOver}
;

public class OfflineManager : MonoBehaviour
{
	public static event GloveEvent SpwanFirstGlove;
	
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

	public bool Mute;
	//public AudioSource source_Punch;
	//public AudioSource source_RoundStart;
	//public AudioSource source_Fight;
	//public AudioSource source_Round;
	//public AudioSource[] source_RoundNumber;
	
	//scripts link
	public PlayerHolderController PlayerHolder1;
	public PlayerHolderController PlayerHolder2;
	public OfflineRoundController RoundPanel;



	public GameObject Player1HUDPanel;
	public GameObject Player2HUDPanel;

	public Transform foreground;

	public GameState currentState;
    
	public bool glovePicked;
	public bool PUPicked;

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



	//sets GameState to RoundStart and sets the sprite for both player
	void OnEnable ()
	{
		currentState = GameState.RoundStart;
		//why we need this when we know that there are 2 players and we have 2 sprites
		PlayerHolder1.GetComponent<SpriteRenderer> ().sprite = PlayerHolder1.mySprites [OfflineMenuController.Player1CharacterID];
		//Debug.Log (OfflineMenuController.Player1CharacterID);
		PlayerHolder2.GetComponent<SpriteRenderer> ().sprite = PlayerHolder2.mySprites [OfflineMenuController.Player2CharacterID];
		//Debug.Log (OfflineMenuController.Player2CharacterID);

	}



	//sets the player intital position and calls ShowRoundPanel()
	void Start ()
	{
		P1ReadtText.text = "Re-Match";
		P2ReadtText.text = "Re-Match";


		//really need it? never used anywhere else
		P1StartPos = new Vector3 (0, -3, 0);
		P2StartPos = new Vector3 (0, 3, 0);
		PlayerHolder1.transform.position = P1StartPos;
		PlayerHolder2.transform.position = P2StartPos;

		foreground.transform.localScale = new Vector3 (.8f, 0.8f, 1);
        
		//RoundPanel.gameObject.SetActive(true);
		RoundPanel.ShowRoundPanel ();
        
	}
    
	//check for escape button click, spawn gloves and power ups, controls timer, checks round status
	void Update ()
	{

		//commenting this coz it gets too frustrating while playing
		/*if (Input.GetKeyDown (KeyCode.Escape))
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
				PauseText.text = "";
			}
		}*/
		
		if (currentState == GameState.Playing)
		{
			

			//Timer controller
			//roundTimer -= Time.deltaTime;
			//code for timer
			//GetComponentInChildren<ProgressBar> ().UpdateBar ((int)roundTimer);

			//timerText_HUD.text = roundTimer.ToString ("N0");

			/*if (roundTimer <= 0)
			{
				//Times up and round is over
				CheckRoundStatus ();
			}*/
		}
		else if (currentState == GameState.Fight)
		{
			ZoomIn ();
		}
		else if (currentState == GameState.RoundOver || currentState == GameState.MatchOver)
		{
			
			ZoomOut ();
			myPUController.glove.SetActive (false);
			myPUController.PU.SetActive (false);
			PUPicked = true;
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

		if (foreground.transform.localScale.x < 1)
		{
			foreground.transform.localScale += new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}
	}

	public void ZoomOut ()
	{
		
		

		//Player1HUDPanel.SetActive (false);
		//Player2HUDPanel.SetActive (false);

		if (foreground.transform.localScale.x > 0.8f)
		{
			foreground.transform.localScale -= new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}
	}

	//checks for the winner and sets the GameState to MatchOver or RoundOver
	//any call for stoping the game should be sent here
	public void CheckRoundStatus ()
	{
		if (PlayerHolder1.myHealth > PlayerHolder2.myHealth)
		{
			PlayerHolder1.roundWins++;
			TrophyP1.gameObject.SetActive (true);
		}
		else if (PlayerHolder2.myHealth > PlayerHolder1.myHealth)
		{
			PlayerHolder2.roundWins++;
			TrophyP2.gameObject.SetActive (true);

		}
		else if (PlayerHolder2.myHealth == PlayerHolder1.myHealth)
		{
			if (PlayerHolder1.hasGlove)
			{
				PlayerHolder1.roundWins++;
				TrophyP1.gameObject.SetActive (true);

			}
			else if (PlayerHolder2.hasGlove)
			{
				PlayerHolder2.roundWins++;
				TrophyP2.gameObject.SetActive (true);

			}
		}
		
		if (PlayerHolder1.roundWins == 2 || PlayerHolder2.roundWins == 2)
		{
			currentState = GameState.MatchOver;
		}
		else
		{
			if (currentState != GameState.MatchOver)
			{
				currentState = GameState.RoundOver;
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
		PlayerHolder1.transform.localPosition = new Vector3 (0, -3, 0);
		PlayerHolder1.transform.rotation = Quaternion.identity;
		PlayerHolder1.ResetPlayer ();

		PlayerHolder2.transform.localPosition = new Vector3 (0, 3, 0);
		PlayerHolder2.transform.rotation = Quaternion.Euler (0, 0, 180);
		PlayerHolder2.ResetPlayer ();

		//roundText_HUD.text = "Round: " + OfflineManager.Instance.roundNumber;

		//some new codes here for BGColor, do we need this change?? it will be difficult to match all sprites with the bg color
		//okay lets leave the code till we get the assets
		if (this.roundNumber == 2)
		{
			//Camera.main.backgroundColor = Color.cyan;
			//cameraBGcolor = Camera.main.backgroundColor;
		}
		if (roundNumber == 3)
		{
			//Camera.main.backgroundColor = Color.grey;
			//cameraBGcolor = Camera.main.backgroundColor;
		}
		//till here

		if (SpwanFirstGlove != null)
		{
			SpwanFirstGlove ();
		}


	}
    
	//sets the roundWins to 0 for both player
	public void NewMatchStart ()
	{
		PlayerHolder1.roundWins = 0;
		PlayerHolder2.roundWins = 0;
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
