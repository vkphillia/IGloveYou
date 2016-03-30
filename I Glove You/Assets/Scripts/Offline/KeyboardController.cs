using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class KeyboardController : MonoBehaviour
{

	private bool ZDown;
	private bool XDown;
	private bool NDown;
	private bool MDown;

	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
        {
			KeyboardControls ();

			if (ZDown)
            {
				MoveClockWise (OfflineManager.Instance.PlayerHolder1.transform);
			}

            else if (XDown)
            {
				MoveAntiClockWise (OfflineManager.Instance.PlayerHolder1.transform);
			}

			if (NDown)
            {
				MoveClockWise (OfflineManager.Instance.PlayerHolder2.transform);
			}

            else if (MDown)
            {
				MoveAntiClockWise (OfflineManager.Instance.PlayerHolder2.transform);
			}
		}
        else
        {
			ZDown = false;
			XDown = false;
			NDown = false;
			MDown = false;
		}
		
	}


	void KeyboardControls ()
	{
		//PlayerPrefs 1		
		if (Input.GetKeyDown (KeyCode.Z))
        {
			ZDown = true;
			XDown = false;
		}

        else if (Input.GetKeyDown (KeyCode.X))
        {
			XDown = true;	
			ZDown = false;		
		}

        else if (Input.GetKeyUp (KeyCode.X))
        {
			XDown = false;
		}

        else if (Input.GetKeyUp (KeyCode.Z))
        {
			ZDown = false;
		}

		//player 2
		if (Input.GetKeyDown (KeyCode.N))
        {
			NDown = true;
			MDown = false;
		}

        else if (Input.GetKeyDown (KeyCode.M))
        {
			MDown = true;	
			NDown = false;		
		}

        else if (Input.GetKeyUp (KeyCode.N))
        {
			NDown = false;
		}

        else if (Input.GetKeyUp (KeyCode.M))
        {
			MDown = false;
		}
		
	}

	void MoveClockWise (Transform t)
	{
		t.Rotate (0, 0, 5);
	}

	void MoveAntiClockWise (Transform t)
	{
		t.Rotate (0, 0, -5);
	}
}
