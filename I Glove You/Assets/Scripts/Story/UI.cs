using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    
	public void Tower ()
	{
		StoryManager.Instance.currentState = StoryGameState.ChallengeSelect;
		StoryManager.Instance.ChallengeTowerPanel.SetActive (true);
		PlayerData.Instance.Level++;
		PlayerData.Instance.SaveData ();
		gameObject.SetActive (false);
	}

    public void ReloadChallenge()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextChallenge()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToTowerMenu()
    {
        SceneManager.LoadScene("story main");
    }

}
