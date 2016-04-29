using UnityEngine;
using System.Collections;

public class HealthPU : PowerUp
{
	public int HealthUp;

	public override void Player1Picked ()
	{
		GameManager.Instance.players [0].AlterHealth (HealthUp);
		base.DeactivatePU ();
	}

	public override void Player2Picked ()
	{
		GameManager.Instance.players [1].AlterHealth (HealthUp);
		base.DeactivatePU ();
	}
}
