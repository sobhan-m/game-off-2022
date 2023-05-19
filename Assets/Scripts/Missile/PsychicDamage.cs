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
		throw new System.NotImplementedException();
	}

	public override void ApplyEffect(IAffectable affectedObject)
	{
		throw new System.NotImplementedException();
	}
}