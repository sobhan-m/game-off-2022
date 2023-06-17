using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MissileDamage
{
	public static float playerDamageMultiplier = 1;

	public MissileType damageType { get; protected set; }
	public float damageAmount { get; protected set; }

	abstract public void ApplyDamage(IDamageable damagedObject);

	public static MissileDamage ConstructMissileDamage(MissileType damageType, float damageAmount)
	{
		switch (damageType)
		{
			case MissileType.REGULAR:
				return new RegularDamage(damageAmount);
			case MissileType.FIRE:
				return new FireDamage(damageAmount);
			case MissileType.PSYCHIC:
				return new PsychicDamage(damageAmount);
			case MissileType.COLD:
				return new ColdDamage(damageAmount);
			default:
				throw new System.ArgumentException("Must have a valid damage type.");
		}
	}
}
