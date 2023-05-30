using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapper
{
	public int currentWeaponIndex { get; private set; }
	public AvailableMissiles availableMissiles { get; private set; }

	public WeaponSwapper(AvailableMissiles availableMissiles, int currentWeaponIndex = 0)
	{
		Debug.Log(availableMissiles.missiles.Count);
		if (currentWeaponIndex >= availableMissiles.missiles.Count)
		{
			throw new System.ArgumentOutOfRangeException("The current weapon index should be within the list provided.");
		}

		this.currentWeaponIndex = currentWeaponIndex;
		this.availableMissiles = availableMissiles;
	}

	public void RotateRight()
	{
		currentWeaponIndex = Modulo(currentWeaponIndex + 1, availableMissiles.missiles.Count);
	}

	public void RotateLeft()
	{
		currentWeaponIndex = Modulo(currentWeaponIndex - 1, availableMissiles.missiles.Count);
	}

	public GameObject GetCurrentWeapon()
	{
		return availableMissiles.missiles[currentWeaponIndex];
	}

	public GameObject GetRightWeapon()
	{
		return availableMissiles.missiles[Modulo(currentWeaponIndex + 1, availableMissiles.missiles.Count)];
	}

	public GameObject GetLeftWeapon()
	{
		return availableMissiles.missiles[Modulo(currentWeaponIndex - 1, availableMissiles.missiles.Count)];
	}

	private int Modulo(int num, int mod)
	{
		return (num % mod + mod) % mod;
	}

}
