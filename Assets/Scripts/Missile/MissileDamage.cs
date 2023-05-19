using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MissileDamage
{
	public PlayerMissileType damageType { get; protected set; }
	public float damageAmount { get; protected set; }

	abstract public void ApplyEffect(IAffectable affectedObject);
	abstract public void ApplyDamage(IDamageable damagedObject);
}
