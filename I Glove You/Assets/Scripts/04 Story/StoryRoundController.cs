using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryRoundController : MonoBehaviour
{
	public Text myRoundText;
	public GameObject UI;

	void OnEnable ()
	{
		ShowRoundPanel ();
	}

	//sets round start and over texts
	public void ShowRoundPanel ()
	{
		gameObject.SetActive (true);

		if (StoryManager.Instance.currentState == StoryGameState.RoundStart)
		{
			StartCoroutine (HideRoundStartText ());
		}
		else if (StoryManager.Instance.currentState == StoryGameState.RoundOver)
		{
			StartCoroutine (HideRoundOverText ());
		}
		else if (StoryManager.Instance.currentState == StoryGameState.MatchOver)
		{
			StartCoroutine (HideMatchOverText ());
		}
	}

	public IEnumerator HideRoundStartText ()
	{
		StoryManager.Instance.currentChallenge.StartNewRound ();
		myRoundText.text = "Round " + StoryManager.Instance.currentChallenge.roundNumber;
		StartCoroutine (RoundNumberSFX ());
		yield return new WaitForSeconds (3f);

		myRoundText.text = "Fight!";
		StoryManager.Instance.currentState = StoryGameState.Fight;
		StorySoundsController.Instance.PlaySoundFX ("Fight", 1.0f);
		StorySoundsController.Instance.PlaySoundFX ("BoxingBell", 0.1f);
		yield return new WaitForSeconds (1f);

		StoryManager.Instance.currentState = StoryGameState.Playing;
		gameObject.SetActive (false);
	}

	public IEnumerator HideRoundOverText ()
	{	
		myRoundText.text = "";
		StorySoundsController.Instance.PlaySoundFX ("RoundEnd", 1.0f);
		yield return new WaitForSeconds (1f);
		myRoundText.text = "Round Over";
		yield return new WaitForSeconds (3f);
		StartCoroutine (HideRoundStartText ());
	}

	//loads offline menu after showing the winner
	public IEnumerator HideMatchOverText ()
	{
		myRoundText.text = "";
		yield return new WaitForSeconds (2f);
		StorySoundsController.Instance.PlaySoundFX ("Win", 1.0f);

		myRoundText.text = "Challenge Complete";
		
		yield return new WaitForSeconds (3f);
		myRoundText.text = "";
		StoryManager.Instance.RoundPanel.gameObject.SetActive (false);
		UI.SetActive (true);
	}

	IEnumerator RoundNumberSFX ()
	{
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_Round);
		StorySoundsController.Instance.PlaySoundFX ("Round", 1.0f);
        
		yield return new WaitForSeconds (0.3f);
		if (StoryManager.Instance.currentChallenge.roundNumber == 1)
			StorySoundsController.Instance.PlaySoundFX ("one", 1.0f);
		else if (StoryManager.Instance.currentChallenge.roundNumber == 2)
			StorySoundsController.Instance.PlaySoundFX ("two", 1.0f);
		else if (StoryManager.Instance.currentChallenge.roundNumber == 3)
			StorySoundsController.Instance.PlaySoundFX ("three", 1.0f);
	}
}
