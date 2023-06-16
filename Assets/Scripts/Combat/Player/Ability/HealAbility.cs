using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
	public float healAmount { get; private set; }
	private Player player;

	public HealAbility(float healAmount, Player player)
	{
		this.healAmount = healAmount;
		this.player = player;

		this.type = AbilityType.CLERIC;
	}

	public override void Activate()
	{
		player.RetrieveHealth().Heal(healAmount);
	}

	public override void Deactivate()
	{
		// Doesn't need to deactivate anything.
	}

}
