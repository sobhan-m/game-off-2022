using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapper
{
	public int currentWeaponIndex { get; private set; }
	public static readonly PlayerMissileType[] PLAYER_MISSILE_TYPES = { PlayerMissileType.REGULAR, PlayerMissileType.FIRE, PlayerMissileType.PSYCHIC };

	public WeaponSwapper()
	{
		currentWeaponIndex = 0;
	}
	public WeaponSwapper(int startingWeaponIndex)
	{
		if (startingWeaponIndex < 0 || startingWeaponIndex >= PLAYER_MISSILE_TYPES.Length)
		{
			throw new System.ArgumentOutOfRangeException("Please ensure startingWeaponIndex is between 0 and the maximum length of the PlayerMissileTypes.");
		}

		currentWeaponIndex = startingWeaponIndex;
	}

	public void RotateRight()
	{
		++currentWeaponIndex;

		// Circle back to the beginning.
		if (currentWeaponIndex >= PLAYER_MISSILE_TYPES.Length)
		{
			currentWeaponIndex = 0;
		}
	}

	public void RotateLeft()
	{
		--currentWeaponIndex;

		// Circle back to the end.
		if (currentWeaponIndex < 0)
		{
			currentWeaponIndex = PLAYER_MISSILE_TYPES.Length - 1;
		}
	}

	public PlayerMissileType GetWeapon()
	{
		return PLAYER_MISSILE_TYPES[currentWeaponIndex];
	}

}
