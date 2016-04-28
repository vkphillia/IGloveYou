using UnityEngine;
using System.Collections;

public class ChallengeController : MonoBehaviour
{

	void OnEnable ()
	{
		Console.Log (GameManager.Instance.myChallenge.LevelNum);
	}
}
