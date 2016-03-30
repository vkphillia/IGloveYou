using UnityEngine;
using System.Collections;

public class PowerUp_Story : MonoBehaviour
{

	//This is a base call and all Power ups need to be derived from this class

	public int weight;

	public virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !Challenge.Instance.player.hasGlove)
		{
			PlayerPicked ();
		}
		else if (other.gameObject.layer == 10 && !Challenge.Instance.enemyHolder.enemy.hasGlove)
		{
			EnemyPicked ();
		}
		else if (other.gameObject.layer == 8 && Challenge.Instance.player.hasGlove)
		{
			PlayerWithGlovePicked ();
		}
		else if (other.gameObject.layer == 10 && Challenge.Instance.enemyHolder.enemy.hasGlove)
		{
			EnemyWithGlovePicked ();
		}
	}



	public virtual void PlayerPicked ()
	{
		Debug.Log ("PlayerHealth++");
	}

	public virtual void EnemyPicked ()
	{
		Debug.Log ("EnemyHealth++");
	}

	public virtual void PlayerWithGlovePicked ()
	{
		//Challenge.Instance.player.PunchPUS (this.transform);
		DeactivatePU ();
	}

	public virtual void EnemyWithGlovePicked ()
	{
		//Challenge.Instance.enemyHolder.enemy.PunchPUS (this.transform);
		DeactivatePU ();
	}

	public virtual void DeactivatePU ()
	{
		Challenge.Instance.PUPicked = true;
		gameObject.SetActive (false);
	}


}
