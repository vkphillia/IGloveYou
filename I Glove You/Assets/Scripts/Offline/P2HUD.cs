using UnityEngine;
using System.Collections;

public class P2HUD : MonoBehaviour
{
	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		myAnim.Play ("P2HUD_Up");
	}

	public IEnumerator GoDown ()
	{
		myAnim.Play ("P2HUD_Down");
		yield return new WaitForSeconds (0.75f);
		gameObject.SetActive (false);
	}
}