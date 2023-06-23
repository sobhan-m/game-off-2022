using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntangleAbility : Ability
{
	public float entangleDuration { get; private set; }
	private List<MonoBehaviour> entangledObjects;
	private GameObject visualEffectPrefab;
	private List<GameObject> visualEffects;

	public EntangleAbility(float entangleDuration, GameObject visualEffectPrefab)
	{
		this.entangleDuration = entangleDuration;
		this.visualEffectPrefab = visualEffectPrefab;
		entangledObjects = new List<MonoBehaviour>();
		visualEffects = new List<GameObject>();

		this.type = AbilityType.DRUID;
	}

	public override void Activate()
	{
		EnemyAttackManager[] enemyAttackManagers = Object.FindObjectsOfType<EnemyAttackManager>();
		EnemyMovementManager[] enemyMovementManagers = Object.FindObjectsOfType<EnemyMovementManager>();
		EnemyMissile[] enemyMissiles = Object.FindObjectsOfType<EnemyMissile>();

		foreach (EnemyAttackManager enemy in enemyAttackManagers)
		{
			entangledObjects.Add(enemy);
			enemy.Freeze();
			AddVisualEffect(enemy);
		}

		foreach (EnemyMovementManager enemy in enemyMovementManagers)
		{
			entangledObjects.Add(enemy);
			enemy.Freeze();
			AddVisualEffect(enemy);
		}

		foreach (EnemyMissile enemy in enemyMissiles)
		{
			entangledObjects.Add(enemy);
			enemy.Freeze();
			AddVisualEffect(enemy);
		}
	}

	public override void Deactivate()
	{
		for (int i = 0; i < entangledObjects.Count; ++i)
		{
			IFreezable enemy = entangledObjects[i] as IFreezable;
			if (enemy != null)
			{
				enemy.Unfreeze();
			}
		}
		entangledObjects.Clear();
		RemoveVisualEffects();
	}

	private void AddVisualEffect(MonoBehaviour enemy)
	{
		visualEffects.Add(Object.Instantiate(visualEffectPrefab, enemy.transform.position, Quaternion.identity, enemy.transform));
	}

	private void RemoveVisualEffects()
	{
		for (int i = 0; i < visualEffects.Count; ++i)
		{
			if (visualEffects[i] != null)
			{
				Object.Destroy(visualEffects[i]);
			}
		}
		visualEffects.Clear();
	}
}
