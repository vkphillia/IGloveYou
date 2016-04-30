using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{

	public static int Player1CharacterID = 0;
	public static int Player2CharacterID = 1;
	public Text P1Text;
	public Text P2Text;

	private bool P1Ready;
	private bool P2Ready;



	void Enable ()
	{
		Player1CharacterID = 0;
		Player2CharacterID = 1;
		P1Text.text = "Fight!";
		P2Text.text = "Fight!";
	}

	void Update ()
	{
		if (P1Ready && P2Ready)
		{
			P1Ready = false;
			P2Ready = false;
			SceneManager.LoadSceneAsync ("offline game");
		}
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Exit ();
		}
	}

	public void P1Fight ()
	{
		P1Ready = true;
		P1Text.text = "Ready!";
	}

	public void P2Fight ()
	{
		P2Ready = true;
		P2Text.text = "Ready!";

	}

	public void Player1Character (int id)
	{
		Player1CharacterID = id;
	}

	public void Player2Character (int id)
	{
		Player2CharacterID = id;
	}

	public void Exit ()
	{
		SceneManager.LoadScene ("main menu");
	}
}
