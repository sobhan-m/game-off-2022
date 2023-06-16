using UnityEngine;

public class PsychicDamage : MissileDamage
{
	public PsychicDamage()
	{
		this.damageAmount = 0;
		this.damageType = PlayerMissileType.PSYCHIC;
	}

	public PsychicDamage(float damageAmount)
	{
		this.damageAmount = damageAmount;
		this.damageType = PlayerMissileType.PSYCHIC;
	}


	public override void ApplyDamage(IDamageable damagedObject)
	{
		MonoBehaviour enemy = damagedObject as MonoBehaviour;
		if (enemy && enemy.TryGetComponent<EnemyMissile>(out EnemyMissile missile))
		{
			Debug.Log("PsychicDamage.ApplyDamage(): Not applying damage.");
			return;
		}


		Health health = damagedObject.RetrieveHealth();
		health.Damage(damageAmount * MissileDamage.playerDamageMultiplier);

		if (health.IsDead())
		{
			damagedObject.Die();
		}
	}
}