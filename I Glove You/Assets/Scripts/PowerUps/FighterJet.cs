using UnityEngine;
using System.Collections;

public class FighterJet : MonoBehaviour
{
	public float mySpeed;
	//public Transform strike1;
	private Vector3 myStartPos;
	private Vector3 targetPos;
	private Vector3 myPos;
	private Vector3 relativePos;
	private bool active;

	void OnEnable ()
	{
		StartCoroutine (Disable ());
	}

	void Update ()
	{
		transform.position += transform.up * Time.deltaTime * mySpeed;
	}

	public void AIFollow ()
	{
		//Find distance to target

		targetPos = Camera.main.WorldToScreenPoint (OfflineManager.Instance.PlayerHolder1.transform.position);
		myPos = Camera.main.WorldToScreenPoint (this.transform.position);
		relativePos = targetPos - myPos;

		//rotate towards player with target
		float angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90)));
	}

	IEnumerator Disable ()
	{
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);
	}


}
