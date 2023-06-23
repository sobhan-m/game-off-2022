using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissileEffect
{
	public float secondsRemaining { get; set; }
	public MissileType missileType { get; protected set; }
	public bool isFinished
	{
		get
		{
			return secondsRemaining <= float.Epsilon;
		}
	}
	public bool isSingleUse { get; protected set; }
	public bool hasTriggeredOnce { get; protected set; }

	public abstract void ApplyEffect(IAffectable affectable);
	public abstract void EndEffect(IAffectable affectable);

	public void ReduceRemainingTime(float seconds)
	{
		secondsRemaining -= seconds;
	}

	public static MissileEffect CreateMissileEffect(MissileType type, float duration, float value)
	{
		switch (type)
		{
			case MissileType.COLD:
				return new ColdEffect(type, duration);
			case MissileType.FIRE:
				return new FireEffect(type, duration, value);
			default:
				return null;
		}
	}
}
