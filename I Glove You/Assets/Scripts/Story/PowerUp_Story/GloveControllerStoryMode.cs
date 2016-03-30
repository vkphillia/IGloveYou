using UnityEngine;
using System.Collections;

public class GloveControllerStoryMode : MonoBehaviour
{
	/*	public int damageAmount;


	void Start ()
	{
		damageAmount = -1;//default amount
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !other.gameObject.GetComponent <PlayerControlsUniversal> ().hasGlove)
		{
			//try not to use so many getcomponets (not very effecient). Lets cache references in the awake function if used regularly
			Challenge.Instance.player.AlterHealth (damageAmount);
			//other.gameObject.GetComponent<PlayerControlsUniversal>().AlterHealth(damageAmount);
		}
		else if (other.gameObject.layer == 10 && !other.gameObject.GetComponent<Enemy> ().hasGlove)
		{
			Challenge.Instance.enemyHolder.enemy.AlterHealth (damageAmount);
			//other.gameObject.GetComponent<Enemy> ().AlterHealth (damageAmount);
		}
	}*/





	void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;

	}



	public virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !Challenge.Instance.player.hasGlove)
		{
			Challenge.Instance.player.AddGlove ();
			Challenge.Instance.enemyHolder.enemy.LoseGlove ();

			Challenge.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
		else if (other.gameObject.layer == 10 && !Challenge.Instance.enemyHolder.enemy.hasGlove)
		{
			Challenge.Instance.player.LoseGlove ();
			Challenge.Instance.enemyHolder.enemy.AddGlove ();

			Challenge.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
		else if (other.gameObject.layer == 8 && Challenge.Instance.player.hasGlove)
		{
			//Challenge.Instance.player.PunchPUS (this.transform);
			Challenge.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
		else if (other.gameObject.layer == 10 && Challenge.Instance.enemyHolder.enemy.hasGlove)
		{
			//Challenge.Instance.enemyHolder.enemy.PunchPUS (this.transform);
			Challenge.Instance.glovePicked = true;
			gameObject.SetActive (false);
		}
	}

	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
