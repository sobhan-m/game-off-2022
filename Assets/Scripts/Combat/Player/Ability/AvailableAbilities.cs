using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AvailableAbilities : ScriptableObject
{
	[Header("Heal")]
	public bool hasHeal;
	public float healAmount;
	public float healCooldown;

	[Header("Rage")]
	public bool hasRage;
	public float rageMultiplier;
	public float rageDuration;
	public float rageCooldown;
}
