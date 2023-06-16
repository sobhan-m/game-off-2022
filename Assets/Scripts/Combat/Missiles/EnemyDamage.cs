using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MissileDamage
{
	public EnemyDamage()
	{
		this.damageAmount = 0;
		this.damageType = PlayerMissileType.REGULAR;
	}

	public EnemyDamage(float damageAmount)
	{
		this.damageAmount = damageAmount;
		this.damageType = PlayerMissileType.REGULAR;
	}


	public override void ApplyDamage(IDamageable damagedObject)
	{
		Health health = damagedObject.RetrieveHealth();
		health.Damage(damageAmount);

		if (health.IsDead())
		{
			damagedObject.Die();
		}
	}
}
