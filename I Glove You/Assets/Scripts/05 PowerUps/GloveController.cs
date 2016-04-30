using UnityEngine;
using System.Collections;

public class GloveController : MonoBehaviour
{
	
	void Awake ()
	{
		PlayerHolderController.removeGlove += OnRemoveGlove;
	}


	void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;

	}

	void OnRemoveGlove ()
	{
		GameManager.Instance.glovePicked = true;
		gameObject.SetActive (false);
	}



	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}

	void OnDestroy ()
	{
		PlayerHolderController.removeGlove -= OnRemoveGlove;
	}
}
