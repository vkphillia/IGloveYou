using UnityEngine;
using System.Collections;

public class SpeedDown : PowerUp
{

	public float myTime;
	public float SpeedReduction;

	//for drain effect when slowdown
	private Transform myPooledDrain_FX;
	private Drain_FX Drain_Obj;


	public override void Player1Picked ()
	{
		StartCoroutine (SpeedDrain (OfflineManager.Instance.PlayerHolder2));
	}

	public override void Player2Picked ()
	{
		StartCoroutine (SpeedDrain (OfflineManager.Instance.PlayerHolder1));
	}

	//reduce speed of the other player
	IEnumerator SpeedDrain (PlayerHolderController p)
	{
		SpawnDrain_FX (p.transform);
		//cannot deactivate gameobject as it will kill this coroutine so we disabled sprite and collider
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;

		p.mySpeed -= SpeedReduction;
		SoundsController.Instance.PlaySoundFX ("SpeedDown", 1.0f);
		yield return new WaitForSeconds (myTime);
		p.mySpeed = OfflineManager.Instance.MaxSpeed;
		DeactivatePU ();
	}

	public override void DeactivatePU ()
	{
		//re-enabling th sprite and collider before deactivating gameObject
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;
		base.DeactivatePU ();
	}

	public void SpawnDrain_FX (Transform t)
	{
		myPooledDrain_FX = GameObjectPool.GetPool ("DrainPool").GetInstance ();
		Drain_Obj = myPooledDrain_FX.GetComponent<Drain_FX> ();
		Drain_Obj.transform.position = t.position;
		Drain_Obj.transform.rotation = Quaternion.identity;
		Drain_Obj.transform.SetParent (t);
	}


}
