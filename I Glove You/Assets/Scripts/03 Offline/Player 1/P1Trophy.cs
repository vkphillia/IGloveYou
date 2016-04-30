using UnityEngine;
using System.Collections;

public class P1Trophy : MonoBehaviour
{
	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		transform.position = new Vector3 (0, -1.6f, -1);

		StartCoroutine (MakeItFly ());
	}

	IEnumerator MakeItFly ()
	{
		yield return new WaitForSeconds (1f);
		myAnim.Play ("Trophy_Show");
		SoundsController.Instance.PlaySoundFX ("GlovePick", 0.7f);
		yield return new WaitForSeconds (1f);
		iTween.MoveTo (this.gameObject, iTween.Hash ("position", new Vector3 (-2.3f, -4.3f, -1), "time", 1f, "easetype", "linear", "onComplete", "DestoryGO"));
	}

	void DestoryGO ()
	{
		OfflineManager.Instance.winText_HUD [0].text = GameManager.Instance.players [0].roundWins.ToString ();
		SoundsController.Instance.PlaySoundFX ("CollectPoint", 0.5f);
		myAnim.Play ("Trophy_Idle");

		gameObject.SetActive (false);
	}
}
