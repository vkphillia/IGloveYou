using UnityEngine;
using System.Collections;

public class Drain_FX : MonoBehaviour
{

	void OnEnable ()
	{
		StartCoroutine ("RemovePs");
	}


	IEnumerator RemovePs ()
	{
		yield return new WaitForSeconds (2f);
		transform.parent = null;
		GameObjectPool.GetPool ("DrainPool").ReleaseInstance (transform);
	}
}
