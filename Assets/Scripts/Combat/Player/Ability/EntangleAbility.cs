using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntangleAbility : Ability
{
	public float entangleDuration { get; private set; }
	private List<MonoBehaviour> entangledObjects;
	private Color32 colourChange;

	public EntangleAbility(float entangleDuration, Color32 colourChange)
	{
		this.entangleDuration = entangleDuration;
		this.colourChange = colourChange;
		entangledObjects = new List<MonoBehaviour>();

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
			if (entangledObjects[i] != null)
			{
				RemoveVisualEffect(entangledObjects[i]);
			}
			IFreezable enemy = entangledObjects[i] as IFreezable;
			if (enemy != null)
			{
				enemy.Unfreeze();
			}
		}
		entangledObjects.Clear();
	}

	private void AddVisualEffect(MonoBehaviour enemy)
	{
		if (!enemy.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			return;
		}

		spriteRenderer.color = colourChange;
	}

	private void RemoveVisualEffect(MonoBehaviour enemy)
	{
		if (!enemy.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			return;
		}

		spriteRenderer.color = new Color32(255, 255, 255, 255);
	}
}
