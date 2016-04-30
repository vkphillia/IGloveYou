using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour
{
	public AirStrikePU myParentPU;

	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.layer == 8 && GameManager.Instance.players [0].hasGlove))
		{
			Debug.Log ("GotBombed");
			GameManager.Instance.players [0].getPunched (this.transform);
			GameManager.Instance.players [0].AlterHealth (myParentPU.damage);
			//OfflineManager.Instance.PlayerHolder1.CheckForRoundOver (OfflineManager.Instance.PlayerHolder2.transform);
			GameManager.Instance.players [0].LoseGlove ();
			GameManager.Instance.players [1].AddGlove ();
		}
		else if ((other.gameObject.layer == 9 && GameManager.Instance.players [1].hasGlove))
		{
			Debug.Log ("GotBombed");
			GameManager.Instance.players [1].getPunched (this.transform);
			GameManager.Instance.players [1].AlterHealth (myParentPU.damage);
			//OfflineManager.Instance.PlayerHolder2.CheckForRoundOver (OfflineManager.Instance.PlayerHolder1.transform);
			GameManager.Instance.players [0].AddGlove ();
			GameManager.Instance.players [1].LoseGlove ();
		}
	}
}
