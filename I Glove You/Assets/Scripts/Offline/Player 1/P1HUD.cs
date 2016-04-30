using UnityEngine;
using System.Collections;

public class P1HUD : MonoBehaviour
{
	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		StartCoroutine (ShowHUD ());
	}

	public IEnumerator ShowHUD ()
	{
		myAnim.Play ("P1HUD_Up");
		yield return new WaitForSeconds (0.6f);
		SoundsController.Instance.PlaySoundFX ("BoxingBell", 0.8f);
	}

	public IEnumerator GoDown ()
	{
		myAnim.Play ("P1HUD_Down");
		yield return new WaitForSeconds (0.75f);
		gameObject.SetActive (false);
	}
}