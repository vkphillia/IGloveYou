using UnityEngine;
using System.Collections;

public class WalkingBombBlastCol_Story : MonoBehaviour
{
	private WalkingBombPU_Story myParentBomb;

	void Awake ()
	{
		myParentBomb = GetComponentInParent<WalkingBombPU_Story> ();
	}

	//if active bombs hits player with glove, reduce its health and swap gloves
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && Challenge.Instance.player.hasGlove)
		{
			StopCoroutine (myParentBomb.ActivateBomb ());
			StartCoroutine (myParentBomb.BlastNow ());
			Challenge.Instance.player.AlterHealth (myParentBomb.damageByBlast);
			//Challenge.Instance.player.getPunched (this.transform);
			if (Challenge.Instance.GloveOn)
			{
				Challenge.Instance.player.LoseGlove ();
				Challenge.Instance.enemyHolder.enemy.AddGlove ();

			}
		}
		else if (other.gameObject.layer == 10 && Challenge.Instance.enemyHolder.enemy.hasGlove)
		{
			StopCoroutine (myParentBomb.ActivateBomb ());
			StartCoroutine (myParentBomb.BlastNow ());
			Challenge.Instance.enemyHolder.enemy.AlterHealth (myParentBomb.damageByBlast);
			if (Challenge.Instance.GloveOn)
			{
				Challenge.Instance.player.AddGlove ();
				Challenge.Instance.enemyHolder.enemy.LoseGlove ();
			}
		}
	}
}
