public class RegularDamage : MissileDamage
{
	public RegularDamage()
	{
		this.damageAmount = 0;
		this.damageType = PlayerMissileType.REGULAR;
	}

	public RegularDamage(float damageAmount)
	{
		this.damageAmount = damageAmount;
		this.damageType = PlayerMissileType.REGULAR;
	}


	public override void ApplyDamage(IDamageable damagedObject)
	{
		Health health = damagedObject.RetrieveHealth();
		health.Damage(damageAmount);

		if (health.IsDead())
		{
			damagedObject.Die();
		}
	}
}