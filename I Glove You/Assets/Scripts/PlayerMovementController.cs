using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//attach this script to players and set the playerNo
public class PlayerMovementController : MonoBehaviour
{
	private int myNo;

	void Awake ()
	{
		GameManager.GetPlayerNo += getPlayerNo;
	}

	void Update ()
	{
		if (GameManager.Instance.currentState == GameState.Playing)
		{
			KeyboardControls ();
			MobileControls ();
		}
	}

	void KeyboardControls ()
	{
		//only for two player mode
		if (myNo == 0)
		{
			if (Input.GetButton ("movez"))
			{
				MoveClockWise ();
			}
			else if (Input.GetButton ("movex"))
			{
				MoveAntiClockWise ();
			}
		}
		else if (myNo == 1)
		{
			if (Input.GetButton ("moven"))
			{
				MoveClockWise ();
			}
			else if (Input.GetButton ("movem"))
			{
				MoveAntiClockWise ();
			}
		}
	}

	void MobileControls ()
	{
		if (myNo == 0)
		{
			int count = Input.touchCount;
			for (int i = 0; i < count; i++)
			{
				Touch touch = Input.GetTouch (i);

				if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
				{
					MoveClockWise ();
				}
				else if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
				{
					MoveAntiClockWise ();
				}
			}
		}
		else if (myNo == 1)
		{
			int count = Input.touchCount;
			for (int i = 0; i < count; i++)
			{
				Touch touch = Input.GetTouch (i);

				if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2)
				{
					MoveAntiClockWise ();
				}
				else if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2)
				{
					MoveClockWise ();
				}
			}
		}
	}

	void MoveClockWise ()
	{
		transform.Rotate (0, 0, 5);
	}

	void MoveAntiClockWise ()
	{
		transform.Rotate (0, 0, -5);
	}

	void getPlayerNo ()
	{
		myNo = GetComponent<PlayerHolderController> ().playerNo;
	}

	void OnDestroy ()
	{
		GameManager.GetPlayerNo -= getPlayerNo;
	
	}
}
