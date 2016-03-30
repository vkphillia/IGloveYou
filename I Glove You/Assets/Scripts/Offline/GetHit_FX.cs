using UnityEngine;
using System.Collections;

public class GetHit_FX : MonoBehaviour
{
	void OnEnable ()
	{
		StartCoroutine ("RemovePs");
	}


	IEnumerator RemovePs ()
	{
		yield return new WaitForSeconds (.75f);
		GameObjectPool.GetPool ("GetHitPool").ReleaseInstance (transform);
	}
}
