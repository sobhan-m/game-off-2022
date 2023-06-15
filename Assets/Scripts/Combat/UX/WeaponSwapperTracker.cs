using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwapperTracker : MonoBehaviour
{
	[SerializeField] Image currentWeapon;
	[SerializeField] Image leftWeapon;
	[SerializeField] Image rightWeapon;
	[SerializeField] float opacity = 0.5f;

	private WeaponSwapper weaponSwapper;
	private int previousIndex;

	private void Start()
	{
		weaponSwapper = FindObjectOfType<PlayerAttackController>().weaponSwapper;
		if (weaponSwapper == null)
		{
			throw new MissingReferenceException("No PlayerAttackController in this scene.");
		}
		previousIndex = weaponSwapper.currentWeaponIndex;
		DisableUnusedImages();
		UpdateWeapons();
	}

	private void Update()
	{
		if (weaponSwapper.currentWeaponIndex != previousIndex)
		{
			previousIndex = weaponSwapper.currentWeaponIndex;
			UpdateWeapons();
		}
	}

	private void UpdateMainWeapon()
	{
		GameObject missile = weaponSwapper.GetCurrentWeapon();
		if (missile.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			currentWeapon.sprite = spriteRenderer.sprite;
			currentWeapon.color = spriteRenderer.color;
		}
	}

	private void UpdateLeftWeapon()
	{
		GameObject missile = weaponSwapper.GetLeftWeapon();
		if (missile.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			leftWeapon.sprite = spriteRenderer.sprite;
			Color color = spriteRenderer.color;
			leftWeapon.color = new Color(color.r, color.g, color.b, opacity);
		}
	}

	private void UpdateRightWeapon()
	{
		GameObject missile = weaponSwapper.GetRightWeapon();
		if (missile.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
		{
			rightWeapon.sprite = spriteRenderer.sprite;
			Color color = spriteRenderer.color;
			rightWeapon.color = new Color(color.r, color.g, color.b, opacity);
		}
	}

	public void UpdateWeapons()
	{
		int numOfMissiles = weaponSwapper.availableMissiles.missiles.Count;
		if (numOfMissiles == 1)
		{
			UpdateMainWeapon();
			rightWeapon.enabled = false;
			leftWeapon.enabled = false;
		}
		else if (numOfMissiles == 2)
		{
			UpdateMainWeapon();
			UpdateRightWeapon();
			leftWeapon.enabled = false;
		}
		else
		{
			UpdateMainWeapon();
			UpdateLeftWeapon();
			UpdateRightWeapon();
		}
	}

	public void DisableUnusedImages()
	{
		int numOfMissiles = weaponSwapper.availableMissiles.missiles.Count;
		if (numOfMissiles == 1)
		{
			rightWeapon.enabled = false;
			leftWeapon.enabled = false;
		}
		else if (numOfMissiles == 2)
		{
			leftWeapon.enabled = false;
		}
	}



}
