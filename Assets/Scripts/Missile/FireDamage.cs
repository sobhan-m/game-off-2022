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
		throw new System.NotImplementedException();
	}

	public override void ApplyEffect(IAffectable affectedObject)
	{
		throw new System.NotImplementedException();
	}
}