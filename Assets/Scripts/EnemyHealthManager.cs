using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IAffectable, IDamageable
{
	[SerializeField] private float initialMaxHealth;
	public Health health { get; private set; }
	public List<MissileEffect> effects { get; private set; }

	private void Awake()
	{
		health = new Health(initialMaxHealth);
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
		effects.Add(missileEffect);
	}
}
