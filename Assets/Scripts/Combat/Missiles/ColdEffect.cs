using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdEffect : MissileEffect
{
	public ColdEffect(MissileType type, float effectSeconds)
	{
		this.missileType = type;
		this.secondsRemaining = effectSeconds;
		this.isSingleUse = true;
		this.hasTriggeredOnce = false;
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
			enemy.Freeze();
		}

		if (mono.TryGetComponent<EnemyAttackManager>(out EnemyAttackManager enemyAttacker))
		{
			enemyAttacker.Freeze();
		}

		// Modifying visuals.
		if (mono.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			spriteRenderer.color = Color.blue;
		}

		this.hasTriggeredOnce = true;
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
			enemy.Unfreeze();
		}

		if (mono.TryGetComponent<EnemyAttackManager>(out EnemyAttackManager enemyAttacker))
		{
			enemyAttacker.Unfreeze();
		}

		// Modifying visuals.
		if (mono.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			spriteRenderer.color = Color.white;
		}
	}
}
