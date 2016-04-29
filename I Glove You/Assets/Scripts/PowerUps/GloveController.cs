using UnityEngine;
using System.Collections;

public class GloveController : MonoBehaviour
{
	



	void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;

	}



	public virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8)
		{
			if (!GameManager.Instance.players [0].hasGlove)
			{
				GameManager.Instance.players [0].AddGlove ();
				GameManager.Instance.players [1].LoseGlove ();
			}
			else
			{
				GameManager.Instance.players [0].PunchPUS (this.transform);
			}	

			GameManager.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
		else if (other.gameObject.layer == 9)
		{
			if (!GameManager.Instance.players [1].hasGlove)
			{
				GameManager.Instance.players [1].AddGlove ();
				GameManager.Instance.players [0].LoseGlove ();
			}
			else
			{
				GameManager.Instance.players [1].PunchPUS (this.transform);
			}	

			GameManager.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
	}






	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
