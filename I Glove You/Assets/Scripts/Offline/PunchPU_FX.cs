using UnityEngine;
using System.Collections;

public class PunchPU_FX : MonoBehaviour
{
	void OnEnable ()
	{
		StartCoroutine ("RemovePs");
	}

	IEnumerator RemovePs ()
	{
		yield return new WaitForSeconds (1f);
		GameObjectPool.GetPool ("PunchPUPool").ReleaseInstance (transform);
	}
	
}
