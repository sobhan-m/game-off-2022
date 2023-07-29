using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
	public float healAmount { get; private set; }
	private PlayerHealthManager player;
	private GameObject vfx;

	public HealAbility(float healAmount, PlayerHealthManager player, GameObject healVFX)
	{
		this.healAmount = healAmount;
		this.player = player;

		this.type = AbilityType.CLERIC;
		this.vfx = healVFX;
	}

	public override void Activate()
	{
		player.RetrieveHealth().Heal(healAmount);
		Object.Instantiate(vfx, player.transform.position, vfx.transform.rotation, player.transform);
	}

	public override void Deactivate()
	{
		// Doesn't need to deactivate anything.
	}

}
