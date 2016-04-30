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
		if (other.gameObject.layer == 8 && GameManager.Instance.players [0].hasGlove)
		{
			GetComponent<CircleCollider2D> ().enabled = false;
			GetComponent<SpriteRenderer> ().enabled = false;
			StopCoroutine (myParentBomb.ActivateBomb (GameManager.Instance.players [0]));
			StartCoroutine (myParentBomb.BlastNow (GameManager.Instance.players [0]));
			GameManager.Instance.players [0].AlterHealth (myParentBomb.damageByBlast);
			GameManager.Instance.players [0].getPunched (this.transform);
			GameManager.Instance.players [0].LoseGlove ();
			GameManager.Instance.players [1].AddGlove ();
		}
		else if (other.gameObject.layer == 9 && GameManager.Instance.players [1].hasGlove)
		{
			GetComponent<CircleCollider2D> ().enabled = false;
			GetComponent<SpriteRenderer> ().enabled = false;
			StopCoroutine (myParentBomb.ActivateBomb (GameManager.Instance.players [1]));
			StartCoroutine (myParentBomb.BlastNow (GameManager.Instance.players [1]));
			GameManager.Instance.players [1].AlterHealth (myParentBomb.damageByBlast);
			GameManager.Instance.players [1].getPunched (this.transform);
			GameManager.Instance.players [0].AddGlove ();
			GameManager.Instance.players [1].LoseGlove ();
		}
	}

}
