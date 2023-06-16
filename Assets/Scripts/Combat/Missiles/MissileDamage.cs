using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MissileDamage
{
	public static float playerDamageMultiplier;

	public PlayerMissileType damageType { get; protected set; }
	public float damageAmount { get; protected set; }

	abstract public void ApplyDamage(IDamageable damagedObject);

	public static MissileDamage ConstructMissileDamage(PlayerMissileType damageType, float damageAmount)
	{
		switch (damageType)
		{
			case PlayerMissileType.REGULAR:
				return new RegularDamage(damageAmount);
			case PlayerMissileType.FIRE:
				return new FireDamage(damageAmount);
			case PlayerMissileType.PSYCHIC:
				return new PsychicDamage(damageAmount);
			case PlayerMissileType.COLD:
				return new ColdDamage(damageAmount);
			default:
				throw new System.ArgumentException("Must have a valid damage type.");
		}
	}
}
