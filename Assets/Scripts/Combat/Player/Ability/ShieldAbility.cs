using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : Ability
{
	private PlayerAbilityController player;
	private GameObject shieldPrefab;
	private GameObject instantiatedShield;

	public ShieldAbility(PlayerAbilityController player, GameObject shieldPrefab)
	{
		this.player = player;
		this.shieldPrefab = shieldPrefab;

		this.type = AbilityType.WIZARD;
	}

	public override void Activate()
	{
		Debug.Log("Shield activated.");
		instantiatedShield = Object.Instantiate(shieldPrefab, player.transform.position, Quaternion.identity, player.transform);
	}

	public override void Deactivate()
	{

	}

}