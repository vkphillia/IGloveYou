using UnityEngine;
using System.Collections;

public class Strike : MonoBehaviour
{

	public PUController myController;
	public Transform myChildBlast;

	void Awake ()
	{
		myController = GameObject.FindObjectOfType<PUController> ();
	}

	void OnEnable ()
	{
		StartCoroutine (ActivateBlast ());
	}

	IEnumerator ActivateBlast ()
	{
		myController.SpawnAnything (this.gameObject);
		yield return new WaitForSeconds (0.75f);
		GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		myChildBlast.gameObject.SetActive (true);
		myChildBlast.GetComponent<Animator> ().Play ("blast_strike");
		SoundsController.Instance.PlaySoundFX ("Blast_Strike", 1.0f);
		GetComponent<SpriteRenderer> ().enabled = false;

	}
}