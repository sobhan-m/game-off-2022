public class ColdDamage : MissileDamage
{
	public ColdDamage()
	{
		this.damageAmount = 0;
		this.damageType = PlayerMissileType.COLD;
	}

	public ColdDamage(float damageAmount)
	{
		this.damageAmount = damageAmount;
		this.damageType = PlayerMissileType.COLD;
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