using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IAffectable, IDamageable
{
	[SerializeField] private float initialMaxHealth;
	public Health health { get; private set; }
	public Dictionary<PlayerMissileType, MissileEffect> effects { get; private set; }

	private void Awake()
	{
		health = new Health(initialMaxHealth);
		effects = new Dictionary<PlayerMissileType, MissileEffect>();
	}

	public void Die()
	{
		Destroy(gameObject);
	}

	public Health RetrieveHealth()
	{
		return health;
	}

	public void StoreEffect(MissileEffect missileEffect)
	{
		// Replaces the effect instead of stacking them.
		if (effects.ContainsKey(missileEffect.missileType))
		{
			effects[missileEffect.missileType] = missileEffect;
			return;
		}

		effects.Add(missileEffect.missileType, missileEffect);
	}

	private void Update()
	{
		ProcessEffects();
	}

	public void ProcessEffects()
	{
		List<MissileEffect> values = new List<MissileEffect>(effects.Values);
		foreach (MissileEffect missileEffect in values)
		{
			missileEffect.ApplyEffect(this);
			missileEffect.ReduceRemainingTime(Time.deltaTime);
			if (missileEffect.isFinished)
			{
				missileEffect.EndEffect(this);
				effects.Remove(missileEffect.missileType);
			}
		}
	}
}
