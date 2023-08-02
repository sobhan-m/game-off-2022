using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageAbility : Ability
{
	public float rageMultiplier { get; private set; }
	private float previousMultiplier;
	private GameObject vfxPrefab;
	private GameObject vfxInstance;
	private PlayerAbilityController player;

	public RageAbility(float rageDamageMultiplier, PlayerAbilityController player, GameObject rageVFX)
	{
		this.rageMultiplier = rageDamageMultiplier;
		this.previousMultiplier = 1;
		vfxPrefab = rageVFX;
		vfxInstance = null;

		this.type = AbilityType.BARBARIAN;

		this.player = player;
	}

	public override void Activate()
	{
		previousMultiplier = MissileDamage.playerDamageMultiplier;
		MissileDamage.playerDamageMultiplier = rageMultiplier;

		if (vfxInstance)
		{
			Object.Destroy(vfxInstance);
		}
		vfxInstance = Object.Instantiate(vfxPrefab, player.transform.position, vfxPrefab.transform.rotation, player.transform);
	}

	public override void Deactivate()
	{
		MissileDamage.playerDamageMultiplier = previousMultiplier;

		if (vfxInstance)
		{
			Object.Destroy(vfxInstance);
			vfxInstance = null;
		}
	}
}
