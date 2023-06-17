public class ColdDamage : MissileDamage
{
	public ColdDamage()
	{
		this.damageAmount = 0;
		this.damageType = MissileType.COLD;
	}

	public ColdDamage(float damageAmount)
	{
		this.damageAmount = damageAmount;
		this.damageType = MissileType.COLD;
	}


	public override void ApplyDamage(IDamageable damagedObject)
	{
		Health health = damagedObject.RetrieveHealth();
		health.Damage(damageAmount * MissileDamage.playerDamageMultiplier);

		if (health.IsDead())
		{
			damagedObject.Die();
		}
	}
}