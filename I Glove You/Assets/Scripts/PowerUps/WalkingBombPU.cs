using UnityEngine;
using System.Collections;

public class WalkingBombPU : PowerUp
{
	public int damageByBlast;
	public float myTime;
	public GameObject myBlastCol;
	public float mySpeed;

	private Vector3 EnemyPos;
	private Vector3 myPos;
	private Vector3 relativePos;
	private float angle;
	private bool active;
	private bool blasted;

	public override void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<CircleCollider2D> ().enabled = true;
		myBlastCol.GetComponent<SpriteRenderer> ().enabled = false;
		myBlastCol.GetComponent<CircleCollider2D> ().enabled = false;
		base.OnEnable ();

	}

	void Update ()
	{
		//find other player and go towards it
		if (active && !blasted)
		{
			if (OfflineManager.Instance.currentState == GameState.Playing)
			{

				AIFollow ();
				transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.75f, 2.75f), Mathf.Clamp (transform.position.y, -3.7f, 3.7f), 0);
				transform.position += transform.up * Time.deltaTime * mySpeed;

			}
		}

	}

	public override void Player1Picked ()
	{
		if (!active)
		{
			myPS.gameObject.SetActive (false);
			active = true;
			StartCoroutine (ActivateBomb (OfflineManager.Instance.PlayerHolder1));
		}
	}

	public override void Player2Picked ()
	{
		if (!active)
		{
			myPS.gameObject.SetActive (false);
			active = true;
			StartCoroutine (ActivateBomb (OfflineManager.Instance.PlayerHolder2));
		}
	}


	public override void Player1WithGlovePicked ()
	{
		if (!active)
		{
			OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
			DeactivatePU ();
		}
	}

	public override void Player2WithGlovePicked ()
	{
		if (!active)
		{
			OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
			DeactivatePU ();
		}
	}

	public IEnumerator ActivateBomb (PlayerHolderController p)
	{
		//loop this please until blast
		SoundsController.Instance.walkingBomb.Play (); 
		//bomb ticking sound goes here

		myBlastCol.GetComponent<CircleCollider2D> ().enabled = true;
		myBlastCol.GetComponent<CircleCollider2D> ().enabled = true;
		//deactivate its collider to ensure it doesn't get picked again
		GetComponent<CircleCollider2D> ().enabled = false;

		yield return new WaitForSeconds (myTime);
		//blast
		StartCoroutine (BlastNow (p));
	}

	//explosion stuff goes here
	public IEnumerator BlastNow (PlayerHolderController p)
	{
		blasted = true;
		SoundsController.Instance.walkingBomb.Pause (); 
		SoundsController.Instance.PlaySoundFX ("Blast", 1.0f);
		GetComponent<SpriteRenderer> ().enabled = false;
		myBlastCol.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (.5f);
		active = false;
		blasted = false;
		DeactivatePU ();

	}

	public override void DeactivatePU ()
	{

		transform.rotation = Quaternion.Euler (0, 0, 0);
		active = false;
		//disable sprite and blast collider before disabling gameobject
		myBlastCol.GetComponent<SpriteRenderer> ().enabled = false;
		myBlastCol.GetComponent<CircleCollider2D> ().enabled = false;
		//activate its collider and sprite renderer
		GetComponent<CircleCollider2D> ().enabled = true;
		GetComponent<SpriteRenderer> ().enabled = true;
		base.DeactivatePU ();
	}

	void AIFollow ()
	{
		
		//Find playr with GLove and follow
		if (OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			EnemyPos = Camera.main.WorldToScreenPoint (OfflineManager.Instance.PlayerHolder1.transform.position);
		}
		else if (OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			EnemyPos = Camera.main.WorldToScreenPoint (OfflineManager.Instance.PlayerHolder2.transform.position);
		}
	
		myPos = Camera.main.WorldToScreenPoint (this.transform.position);
		relativePos = EnemyPos - myPos;

		//rotate towards player with glove
		angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90)));
	}
}
