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


	[SerializeField] float buildUpTime;
	private Meter attackMeter;

	private void Start()
	{
		attackMeter = new Meter(0, buildUpTime);
	}

	private void Awake()
	{
		playerInputActions = new PlayerInputActions();
		attackAction = playerInputActions.Player.Attack;
		weaponChangeAction = playerInputActions.Player.ChangeWeapons;
		attackAction.performed += Attack;
		weaponChangeAction.performed += ChangeWeapon;
	}

	void OnEnable()
	{
		attackAction.Enable();
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
		Debug.Log("test");
		float weaponChange = weaponChangeAction.ReadValue<float>();
		Debug.Log(weaponChange);
	}

	void OnDisable()
	{
		attackAction.Disable();
	}
}
