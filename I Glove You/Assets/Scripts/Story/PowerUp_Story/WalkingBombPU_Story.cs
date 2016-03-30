using UnityEngine;
using System.Collections;

public class WalkingBombPU_Story : PowerUp_Story
{
	public int damageByBlast;
	public float myTime;
	public GameObject myBlastCol;
	public float mySpeed;

	private Vector3 TargetPos;
	private Vector3 myPos;
	private Vector3 relativePos;
	private float angle;
	private bool active;

	void Update ()
	{
		//find other player and go towards it
		if (active)
		{
			if (GameTimer.Instance.timerStarted)
			{
				AIFollow ();
				transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.75f, 2.75f), Mathf.Clamp (transform.position.y, -3.7f, 3.7f), 0);
				transform.position += transform.up * Time.deltaTime * mySpeed;

			}
		}
	}

	public override void PlayerPicked ()
	{
		if (!active)
		{
			active = true;
			StartCoroutine (ActivateBomb ());
		}
	}

	public override void EnemyPicked ()
	{
		if (!active)
		{
			active = true;
			StartCoroutine (ActivateBomb ());
		}
	}


	public override void PlayerWithGlovePicked ()
	{
		if (!active)
		{
			//OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
			DeactivatePU ();
		}
	}

	public override void EnemyWithGlovePicked ()
	{
		if (!active)
		{
			//OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
			DeactivatePU ();
		}
	}

	public IEnumerator ActivateBomb ()
	{
		//bomb pickup sound
		//SoundsController.Instance.PlaySoundFX ("BombWalk", 1.0f);
		//bomb ticking sound goes here

		myBlastCol.GetComponent<CircleCollider2D> ().enabled = true;
		myBlastCol.GetComponent<CircleCollider2D> ().enabled = true;
		//deactivate its collider to ensure it doesn't get picked again
		GetComponent<CircleCollider2D> ().enabled = false;

		yield return new WaitForSeconds (myTime);
		//blast
		StartCoroutine (BlastNow ());
	}

	//explosion stuff goes here
	public IEnumerator BlastNow ()
	{
		active = false;
		//SoundsController.Instance.StopSoundFX ("BombWalk", 0f);
		//SoundsController.Instance.PlaySoundFX ("Blast", 1.0f);
		GetComponent<SpriteRenderer> ().enabled = false;
		myBlastCol.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (1f);
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
		if (Challenge.Instance.player.hasGlove)
		{
			TargetPos = Camera.main.WorldToScreenPoint (Challenge.Instance.player.transform.position);
		}
		else if (OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			TargetPos = Camera.main.WorldToScreenPoint (Challenge.Instance.enemyHolder.enemy.transform.position);
		}
	
		myPos = Camera.main.WorldToScreenPoint (this.transform.position);
		relativePos = TargetPos - myPos;

		//rotate towards player with glove
		angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90)));
	}
}
