public class RegularDamage : MissileDamage
{
	public RegularDamage()
	{
		this.damageAmount = 0;
		this.damageType = MissileType.REGULAR;
	}

	public RegularDamage(float damageAmount)
	{
		this.damageAmount = damageAmount;
		this.damageType = MissileType.REGULAR;
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