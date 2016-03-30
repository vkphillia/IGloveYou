using UnityEngine;
using System.Collections;

public class WalkingBombBlastCol : MonoBehaviour
{
	private WalkingBombPU myParentBomb;

	void Awake ()
	{
		myParentBomb = GetComponentInParent<WalkingBombPU> ();
	}

	//if active bombs hits player with glove, reduce its health and swap gloves
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			StopCoroutine (myParentBomb.ActivateBomb (OfflineManager.Instance.PlayerHolder1));
			StartCoroutine (myParentBomb.BlastNow (OfflineManager.Instance.PlayerHolder1));
			OfflineManager.Instance.PlayerHolder1.AlterHealth (myParentBomb.damageByBlast);
			OfflineManager.Instance.PlayerHolder1.getPunched (this.transform);
			OfflineManager.Instance.PlayerHolder1.LoseGlove ();
			OfflineManager.Instance.PlayerHolder2.AddGlove ();
		}
		else if (other.gameObject.layer == 10 && OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			StopCoroutine (myParentBomb.ActivateBomb (OfflineManager.Instance.PlayerHolder2));
			StartCoroutine (myParentBomb.BlastNow (OfflineManager.Instance.PlayerHolder2));
			OfflineManager.Instance.PlayerHolder2.AlterHealth (myParentBomb.damageByBlast);
			OfflineManager.Instance.PlayerHolder2.getPunched (this.transform);
			OfflineManager.Instance.PlayerHolder1.AddGlove ();
			OfflineManager.Instance.PlayerHolder2.LoseGlove ();
		}
	}

}
