using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdEffect : MissileEffect
{
	public ColdEffect(PlayerMissileType type, float effectSeconds)
	{
		this.missileType = type;
		this.secondsRemaining = effectSeconds;
	}

	public override void ApplyEffect(IAffectable affectable)
	{
		MonoBehaviour mono = affectable as MonoBehaviour;
		if (mono == null)
		{
			return;
		}

		if (mono.TryGetComponent<EnemyMovementManager>(out EnemyMovementManager enemy))
		{
			enemy.enabled = false;
		}

		if (mono.TryGetComponent<EnemyAttackManager>(out EnemyAttackManager enemyAttacker))
		{
			enemyAttacker.enabled = false;
		}
	}

	public override void EndEffect(IAffectable affectable)
	{
		MonoBehaviour mono = affectable as MonoBehaviour;
		if (mono == null)
		{
			return;
		}

		if (mono.TryGetComponent<EnemyMovementManager>(out EnemyMovementManager enemy))
		{
			enemy.enabled = true;
		}

		if (mono.TryGetComponent<EnemyAttackManager>(out EnemyAttackManager enemyAttacker))
		{
			enemyAttacker.enabled = true;
		}
	}
}
