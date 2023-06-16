public class FireDamage : MissileDamage
{
	public FireDamage()
	{
		this.damageAmount = 0;
		this.damageType = PlayerMissileType.FIRE;
	}

	public FireDamage(float damageAmount)
	{
		this.damageAmount = damageAmount;
		this.damageType = PlayerMissileType.FIRE;
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