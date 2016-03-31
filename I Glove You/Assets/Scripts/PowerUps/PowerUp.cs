using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{

	//This is a base call and all Power ups need to be derived from this class

	public int weight;
	public ParticleSystem myPS;

	public virtual void OnEnable ()
	{
		myPS.gameObject.SetActive (true);

	}

	public virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			Player1Picked ();
		}
		else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			Player2Picked ();
		}
		else if (other.gameObject.layer == 9)
		{
			Player1WithGlovePicked ();
		}
		else if (other.gameObject.layer == 11)
		{
			Player2WithGlovePicked ();
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
		OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
		DeactivatePU ();
	}

	public virtual void Player2WithGlovePicked ()
	{
		OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
		DeactivatePU ();
	}

	public virtual void DeactivatePU ()
	{
		
		OfflineManager.Instance.PUPicked = true;
		gameObject.SetActive (false);
	}


}
