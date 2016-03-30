using UnityEngine;
using System.Collections;

public class P2Trophy : MonoBehaviour
{
	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		transform.position = new Vector3 (0, 1.6f, -1);
		StartCoroutine (MakeItFly ());
	}

	IEnumerator MakeItFly ()
	{
		yield return new WaitForSeconds (1f);
		myAnim.Play ("Trophy_Show");
		SoundsController.Instance.PlaySoundFX ("GlovePick", 1f);
		yield return new WaitForSeconds (1f);
		iTween.MoveTo (this.gameObject, iTween.Hash ("position", new Vector3 (2.3f, 4.3f, -1), "time", 1f, "easetype", "linear", "onComplete", "DestoryGO"));

	}

	void DestoryGO ()
	{
		OfflineManager.Instance.PlayerHolder2.myWinText_HUD.text = OfflineManager.Instance.PlayerHolder2.roundWins.ToString ();
		SoundsController.Instance.PlaySoundFX ("CollectPoint", 1f);
		myAnim.Play ("Trophy_Idle");
		gameObject.SetActive (false);
	}
}
