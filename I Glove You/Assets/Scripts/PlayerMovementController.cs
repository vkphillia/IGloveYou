using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//attach this script to players and set the playerNo
public class PlayerMovementController : MonoBehaviour
{
    public int playerNo;

	void Update ()
	{
		if (GameManager.Instance.currentState == GameState.Playing)
        {
			KeyboardControls ();
            MobileControls();
        }
	}

	void KeyboardControls ()
	{
        //only for two player mode
        if(playerNo==1)
        {
            if (Input.GetButton("movez"))
            {
                MoveClockWise();
            }

            else if (Input.GetButton("movex"))
            {
                MoveAntiClockWise();
            }
        }
        else if(playerNo==2)
        {
            if (Input.GetButton("moven"))
            {
                MoveClockWise();
            }

            else if (Input.GetButton("movem"))
            {
                MoveAntiClockWise();
            }
        }
    }

    void MobileControls()
    {
        if (playerNo == 1)
        {
            int count = Input.touchCount;
            for (int i = 0; i < count; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
                {
                    MoveClockWise();
                }

                else if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
                {
                    MoveAntiClockWise();
                }
            }
        }
        else if (playerNo == 2)
        {
            int count = Input.touchCount;
            for (int i = 0; i < count; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2)
                {
                    MoveAntiClockWise();
                }

                else if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2)
                {
                    MoveClockWise();
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
}
