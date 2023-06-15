using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissileEffect
{
	public float secondsRemaining { get; set; }
	public PlayerMissileType missileType { get; protected set; }
	public bool isFinished
	{
		get
		{
			return secondsRemaining <= float.Epsilon;
		}
	}

	public abstract void ApplyEffect(IAffectable affectable);
	public abstract void EndEffect(IAffectable affectable);

	public void ReduceRemainingTime(float seconds)
	{
		secondsRemaining -= seconds;
	}

	public static MissileEffect CreateMissileEffect(PlayerMissileType type, float duration, float value)
	{
		switch (type)
		{
			case PlayerMissileType.COLD:
				return new ColdEffect(type, duration);
			case PlayerMissileType.FIRE:
				return new FireEffect(type, duration, value);
			default:
				return null;
		}
	}
}
