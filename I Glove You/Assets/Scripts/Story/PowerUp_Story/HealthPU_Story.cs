using UnityEngine;
using System.Collections;

public class HealthPU_Story : PowerUp_Story
{
	public int HealthUp;

	public override void PlayerPicked ()
	{
		Challenge.Instance.player.AlterHealth (HealthUp);
		base.DeactivatePU ();
	}

	public override void EnemyPicked ()
	{
		Challenge.Instance.enemyHolder.enemy.AlterHealth (HealthUp);
		base.DeactivatePU ();
	}
}
