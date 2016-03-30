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
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			OfflineManager.Instance.PlayerHolder1.AddGlove ();
			OfflineManager.Instance.PlayerHolder2.LoseGlove ();

			OfflineManager.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
		else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			OfflineManager.Instance.PlayerHolder1.LoseGlove ();
			OfflineManager.Instance.PlayerHolder2.AddGlove ();

			OfflineManager.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
		else if (other.gameObject.layer == 9)
		{
			OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
			OfflineManager.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
		else if (other.gameObject.layer == 11)
		{
			OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
			OfflineManager.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
	}






	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
