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

	[Header("Shield")]
	public bool hasShield;
	public GameObject shieldPrefab;
	public float shieldCooldown;

	[Header("Entangle")]
	public bool hasEntangle;
	public GameObject effectPrefab;
	public float entangleDuration;
	public float entangleCooldown;
}
