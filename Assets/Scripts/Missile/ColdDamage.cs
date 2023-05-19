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
		throw new System.NotImplementedException();
	}

	public override void ApplyEffect(IAffectable affectedObject)
	{
		throw new System.NotImplementedException();
	}
}