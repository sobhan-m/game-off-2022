using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageAbility : Ability
{
	public float rageMultiplier { get; private set; }
	private float previousMultiplier;

	public RageAbility(float rageDamageMultiplier)
	{
		this.rageMultiplier = rageDamageMultiplier;
		this.previousMultiplier = 1;

		this.type = AbilityType.BARBARIAN;
	}

	public override void Activate()
	{
		previousMultiplier = MissileDamage.playerDamageMultiplier;
		MissileDamage.playerDamageMultiplier = rageMultiplier;
	}

	public override void Deactivate()
	{
		MissileDamage.playerDamageMultiplier = previousMultiplier;
	}
}
