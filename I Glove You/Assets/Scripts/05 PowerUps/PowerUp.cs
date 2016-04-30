using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{

	//This is a base call and all Power ups need to be derived from this class

	public int weight;
	public ParticleSystem myPS;

	public virtual void OnEnable ()
	{
		//assign it in story mode also
		myPS.gameObject.SetActive (true);
	}

	public virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8)
		{
			if (GameManager.Instance.currentMode == GameMode.TwoPlayer)
			{
				if (!GameManager.Instance.players [0].hasGlove)
				{
					Player1Picked ();
				}
				else
				{
					Player1WithGlovePicked ();
				}
			}
            //single player mode
            else
			{
				if (!Challenge.Instance.player.hasGlove)
				{
					Player1Picked ();
				}
				else if (Challenge.Instance.player.hasGlove)
				{
					Player1WithGlovePicked ();
				}
			}
		}
		else if (other.gameObject.layer == 9)
		{
			if (GameManager.Instance.currentMode == GameMode.TwoPlayer)
			{
				if (!GameManager.Instance.players [1].hasGlove)
				{
					Player2Picked ();
				}
				else
				{
					Player2WithGlovePicked ();
				}
			}
            //single player mode
            else
			{
				if (!Challenge.Instance.enemyHolder.enemy.hasGlove)
				{
					Player2Picked ();
				}
				else if (Challenge.Instance.player.hasGlove)
				{
					Player2WithGlovePicked ();
				}
			}
		}
	}

	public virtual void Player1Picked ()
	{
		Debug.Log ("Player1Health++");
	}

	public virtual void Player2Picked ()
	{
		Debug.Log ("Player2Health++");
	}

	public virtual void Player1WithGlovePicked ()
	{
		if (GameManager.Instance.currentMode == GameMode.TwoPlayer)
		{
			GameManager.Instance.players [0].PunchPUS (this.transform);
		}
		else
		{
			//Challenge.Instance.player.PunchPUS (this.transform);
		}

		DeactivatePU ();
	}

	public virtual void Player2WithGlovePicked ()
	{
		if (GameManager.Instance.currentMode == GameMode.TwoPlayer)
		{
			GameManager.Instance.players [1].PunchPUS (this.transform);
		}
		else
		{
			//Challenge.Instance.enemyHolder.enemy.PunchPUS (this.transform);
		}
		DeactivatePU ();
	}

	public virtual void DeactivatePU ()
	{
		GameManager.Instance.PUPicked = true;
		gameObject.SetActive (false);
	}


}
