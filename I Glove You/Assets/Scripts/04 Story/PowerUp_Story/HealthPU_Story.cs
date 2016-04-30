using UnityEngine;
using System.Collections;

public class HealthPU_Story : PowerUp
{
	public int HealthUp;

	public override void Player1Picked()
	{
		Challenge.Instance.player.AlterHealth (HealthUp);
		base.DeactivatePU ();
	}

	public override void Player2Picked()
	{
		Challenge.Instance.enemyHolder.enemy.AlterHealth (HealthUp);
		base.DeactivatePU ();
	}
}
