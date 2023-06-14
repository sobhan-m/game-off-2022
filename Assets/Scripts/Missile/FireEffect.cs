using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MissileEffect
{
	public FireDamage damage { get; private set; }
	public FireEffect(PlayerMissileType type, float effectSeconds, float damagePerSecond)
	{
		this.missileType = type;
		this.secondsRemaining = effectSeconds;
		damage = new FireDamage(damagePerSecond);
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
	}

	public override void EndEffect(IAffectable affectable)
	{

	}
}
