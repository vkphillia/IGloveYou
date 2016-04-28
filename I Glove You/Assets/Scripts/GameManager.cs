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

	public GameState currentState;
	public GameMode currentMode;

	[HideInInspector]
	public bool PUPicked;

	[HideInInspector]
	public bool glovePicked;

	[HideInInspector]
	public int ChallengeLevel;

	public ChallengeDetail myChallenge;

	void Awake ()
	{
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
}
