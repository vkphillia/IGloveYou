using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChallengeDetail : MonoBehaviour
{
	
	//Challenge Specific stuff
	public int LevelNum;
	public int NoOfOpponents;
	public int playerHealth;


	[SerializeField]
	private CanvasGroup canvas;

	void Awake ()
	{

	}

	public void OnChallengeClick ()
	{
		GameManager.Instance.myChallenge = this;
		Console.Log (GameManager.Instance.myChallenge.LevelNum);

		StartCoroutine (LoadingScene ("Story game"));
	}

	IEnumerator LoadingScene (string sceneName)
	{
		float speed = 1;
        
		while (canvas.alpha > 0)
		{
			canvas.alpha -= speed * Time.deltaTime;
			yield return null;
		}
		SceneManager.LoadScene (sceneName);
	}
}

