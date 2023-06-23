using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdEffect : MissileEffect
{
	private GameObject visualEffectPrefab;
	private GameObject visualEffect;
	public ColdEffect(MissileType type, float effectSeconds, GameObject visualEffectPrefab)
	{
		this.missileType = type;
		this.secondsRemaining = effectSeconds;
		this.isSingleUse = true;
		this.hasTriggeredOnce = false;
		this.visualEffectPrefab = visualEffectPrefab;
		visualEffect = null;
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

		if (visualEffect == null)
		{
			visualEffect = Transform.Instantiate(visualEffectPrefab, mono.transform.position, Quaternion.identity, mono.transform);
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

		if (visualEffect != null)
		{
			Object.Destroy(visualEffect);
			visualEffect = null;
		}
	}
}
