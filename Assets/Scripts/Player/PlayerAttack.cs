using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] GameObject playerProjectilePrefab;
	private InputAction attackAction;
	private InputAction weaponChangeAction;
	private PlayerInputActions playerInputActions;
	private WeaponSwapper weaponSwapper;
	public PlayerMissileType currentMissile { get; private set; }


	[SerializeField] float buildUpTime;
	public Meter attackMeter { get; private set; }

	private void Awake()
	{
		playerInputActions = new PlayerInputActions();
		attackAction = playerInputActions.Player.Attack;
		weaponChangeAction = playerInputActions.Player.ChangeWeapons;
		attackAction.performed += Attack;
		weaponChangeAction.performed += ChangeWeapon;

		attackMeter = new Meter(0, buildUpTime);
		weaponSwapper = new WeaponSwapper();
		currentMissile = weaponSwapper.GetWeapon();
	}

	void OnEnable()
	{
		attackAction.Enable();
		weaponChangeAction.Enable();
	}

	private void Update()
	{
		attackMeter.FillMeter(Time.deltaTime);
	}

	private void Attack(InputAction.CallbackContext obj)
	{
		if (!attackMeter.IsFull())
		{
			return;
		}

		GameObject projectile = Instantiate(playerProjectilePrefab, transform.position, Quaternion.identity);
		attackMeter.EmptyMeter();
	}

	private void ChangeWeapon(InputAction.CallbackContext obj)
	{
		float weaponChange = weaponChangeAction.ReadValue<float>();
		if (weaponChange > 0)
		{
			weaponSwapper.RotateRight();
		}
		else if (weaponChange < 0)
		{
			weaponSwapper.RotateLeft();
		}
		currentMissile = weaponSwapper.GetWeapon();

		Debug.Log(currentMissile);
	}

	void OnDisable()
	{
		attackAction.Disable();
		weaponChangeAction.Disable();
	}
}
