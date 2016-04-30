using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvas;

	public List<Challenge> Challenge_List = new List<Challenge> ();


	void OnEnable ()
	{
		//PlayerData.Instance.ReadData ();
		//Debug.Log (PlayerData.Instance.Level);
		//for (int i = 0; i < Challenge_List.Count; i++)
		//{
		//	if (PlayerData.Instance.Level == i)
		//	{
		//		Debug.Log ("Player is on level " + Challenge_List [i].myLevelNum);
		//		Challenge_List [i].myButton.enabled = true;
		//		StoryManager.Instance.currentChallenge = Challenge_List [i];

		//	}
		//}
	}

	void Start ()
	{
		StartCoroutine (LoadingMyScene ());
	}



    
	public void ClickOnChallenge ()
	{
		StartCoroutine (LoadingScene ("Story game"));
	}

	public void OnMenuClick ()
	{
		SceneManager.LoadScene ("main menu");
	}

	IEnumerator LoadingMyScene ()
	{
		yield return new WaitForSeconds (0.5f);
		float speed = 1;
        
		while (canvas.alpha < 1)
		{
			canvas.alpha += speed * Time.deltaTime;
			yield return null;
		}
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
