using UnityEngine;
using System.Collections;

public class HealthPU : PowerUp
{
	public int HealthUp;

	public override void Player1Picked ()
	{
		OfflineManager.Instance.PlayerHolder1.AlterHealth (HealthUp);
		base.DeactivatePU ();
	}

	public override void Player2Picked ()
	{
		OfflineManager.Instance.PlayerHolder2.AlterHealth (HealthUp);
		base.DeactivatePU ();
	}
}
