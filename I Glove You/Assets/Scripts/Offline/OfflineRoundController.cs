using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfflineRoundController : MonoBehaviour
{
	public Text myRoundText;
	public Text P1Text;
	public Text P2Text;
	public GameObject UI;
	private Animator myRoundTextAnim;
	public P1HUD HUDP1;
	public P2HUD HUDP2;



	void Awake ()
	{
		myRoundTextAnim = GetComponentInChildren<Animator> ();
	}

	//sets round start and over texts
	public void ShowRoundPanel ()
	{
		gameObject.SetActive (true);

		if (OfflineManager.Instance.currentState == GameState.RoundStart)
		{
			StartCoroutine (HideRoundStartText ());
		}
		else if (OfflineManager.Instance.currentState == GameState.RoundOver)
		{
			StartCoroutine (HideRoundOverText ());
		}
		else if (OfflineManager.Instance.currentState == GameState.MatchOver)
		{
			StartCoroutine (HideMatchOverText ());
		}
	}

	public IEnumerator HideRoundStartText ()
	{
		OfflineManager.Instance.StartNewRound ();
		if (OfflineManager.Instance.roundNumber == 1)
		{
			myRoundText.text = "Round I";
		}
		else if (OfflineManager.Instance.roundNumber == 2)
		{
			myRoundText.text = "Round II";
		}
		else if (OfflineManager.Instance.roundNumber == 3)
		{
			myRoundText.text = "Round III";
		}

		myRoundTextAnim.Play ("Round_Show");
		StartCoroutine (RoundNumberSFX ());
		yield return new WaitForSeconds (3f);
		//myRoundTextAnim.Play ("Round_Idle");
		myRoundText.text = "Fight!";
		OfflineManager.Instance.currentState = GameState.Fight;

		SoundsController.Instance.PlaySoundFX ("Fight", 1.0f);
        
		yield return new WaitForSeconds (1f);
		myRoundTextAnim.Play ("Round_Hide");
		OfflineManager.Instance.currentState = GameState.Playing;
		yield return new WaitForSeconds (1f);
		gameObject.SetActive (false);
	}

	public IEnumerator HideRoundOverText ()
	{	
		myRoundText.text = "";
		SoundsController.Instance.PlaySoundFX ("RoundEnd", 1.0f);
		yield return new WaitForSeconds (.2f);
		myRoundText.text = "Round Over";
		myRoundTextAnim.Play ("Round_Show");

		yield return new WaitForSeconds (3f);
		myRoundTextAnim.Play ("Round_Hide");
		yield return new WaitForSeconds (1f);

		StartCoroutine (HideRoundStartText ());
	}

	//loads offline menu after showing the winner
	public IEnumerator HideMatchOverText ()
	{
		myRoundText.text = "Game Over";
		myRoundTextAnim.Play ("Round_Show");

		yield return new WaitForSeconds (1f);
		SoundsController.Instance.PlaySoundFX ("Win", 1.0f);
		P1Text.gameObject.SetActive (true);
		P2Text.gameObject.SetActive (true);
		if (OfflineManager.Instance.PlayerHolder1.roundWins == 2)
		{
			


			if (OfflineManager.Instance.PlayerHolder1.myHealth == OfflineManager.Instance.MaxHealth)
			{
				P1Text.text = "You Win \n Flawless Victory!";
				P2Text.text = "You Lose";
			}
			else
			{
				P1Text.text = "You Win";
				P2Text.text = "You Lose";
			}

		}
		else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2)
		{

			if (OfflineManager.Instance.PlayerHolder2.myHealth == OfflineManager.Instance.MaxHealth)
			{
				P2Text.text = "You Win \n Flawless Victory!";
				P1Text.text = "You Lose";
			}
			else
			{
				P2Text.text = "You Win";
				P1Text.text = "You Lose";
			}
		}
		yield return new WaitForSeconds (5f);
		myRoundText.text = "";
		P1Text.gameObject.SetActive (false);
		P2Text.gameObject.SetActive (false);
		StartCoroutine (HUDP1.GoDown ());
		StartCoroutine (HUDP2.GoDown ());
		OfflineManager.Instance.NewMatchStart ();
		UI.SetActive (true);

		//SceneManager.LoadScene ("offline menu");
	}



	IEnumerator RoundNumberSFX ()
	{
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_Round);
		SoundsController.Instance.PlaySoundFX ("Round", 1.0f);
        
		yield return new WaitForSeconds (0.3f);
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_RoundNumber [OfflineManager.Instance.roundNumber - 1]);
		if (OfflineManager.Instance.roundNumber == 1)
			SoundsController.Instance.PlaySoundFX ("one", 1.0f);
		else if (OfflineManager.Instance.roundNumber == 2)
			SoundsController.Instance.PlaySoundFX ("two", 1.0f);
		else if (OfflineManager.Instance.roundNumber == 3)
			SoundsController.Instance.PlaySoundFX ("three", 1.0f);
	}
}
