using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MissileEffect
{
	public FireDamage damage { get; private set; }
	public FireEffect(MissileType type, float effectSeconds, float damagePerSecond)
	{
		this.missileType = type;
		this.secondsRemaining = effectSeconds;
		damage = new FireDamage(damagePerSecond);
		this.isSingleUse = false;
		this.hasTriggeredOnce = false;
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

		if (mono.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			spriteRenderer.color = Color.red;
		}
	}

	public override void EndEffect(IAffectable affectable)
	{
		// Modifying visuals.
		MonoBehaviour mono = affectable as MonoBehaviour;
		if (mono == null)
		{
			return;
		}

		if (mono.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			spriteRenderer.color = Color.white;
		}
	}
}
