using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MissileEffect
{
	public FireDamage damage { get; private set; }
	private GameObject visualEffectPrefab;
	private GameObject visualEffect;
	public FireEffect(MissileType type, float effectSeconds, float damagePerSecond, GameObject visualEffectPrefab)
	{
		this.missileType = type;
		this.secondsRemaining = effectSeconds;
		damage = new FireDamage(damagePerSecond);
		this.isSingleUse = false;
		this.hasTriggeredOnce = false;
		this.visualEffectPrefab = visualEffectPrefab;
		visualEffect = null;
	}

	public override void ApplyEffect(IAffectable affectable)
	{
		IDamageable enemy = affectable as IDamageable;
		if (enemy == null)
		{
			return;
		}

		Health enemyHealth = enemy.RetrieveHealth();
		enemyHealth.Damage(damage.damageAmount * Time.deltaTime);

		if (enemyHealth.IsDead())
		{
			enemy.Die();
		}

		// Modifying visuals.
		MonoBehaviour mono = affectable as MonoBehaviour;
		if (mono == null)
		{
			return;
		}

		if (visualEffect == null)
		{
			visualEffect = Transform.Instantiate(visualEffectPrefab, mono.transform.position, Quaternion.identity, mono.transform);
		}

		hasTriggeredOnce = true;
	}

	public override void EndEffect(IAffectable affectable)
	{
		if (visualEffect != null)
		{
			Object.Destroy(visualEffect);
			visualEffect = null;
		}
	}
}
