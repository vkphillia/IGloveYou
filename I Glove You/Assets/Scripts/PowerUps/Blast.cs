using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour
{
	public AirStrikePU myParentPU;

	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.layer == 8 && OfflineManager.Instance.PlayerHolder1.hasGlove))
		{
			Debug.Log ("GotBombed");
			OfflineManager.Instance.PlayerHolder1.getPunched (this.transform);
			OfflineManager.Instance.PlayerHolder1.AlterHealth (myParentPU.damage);
			//OfflineManager.Instance.PlayerHolder1.CheckForRoundOver (OfflineManager.Instance.PlayerHolder2.transform);
			OfflineManager.Instance.PlayerHolder1.LoseGlove ();
			OfflineManager.Instance.PlayerHolder2.AddGlove ();
		}
		else if ((other.gameObject.layer == 10 && OfflineManager.Instance.PlayerHolder2.hasGlove))
		{
			Debug.Log ("GotBombed");
			OfflineManager.Instance.PlayerHolder2.getPunched (this.transform);
			OfflineManager.Instance.PlayerHolder2.AlterHealth (myParentPU.damage);
			//OfflineManager.Instance.PlayerHolder2.CheckForRoundOver (OfflineManager.Instance.PlayerHolder1.transform);
			OfflineManager.Instance.PlayerHolder1.AddGlove ();
			OfflineManager.Instance.PlayerHolder2.LoseGlove ();
		}
	}
}
