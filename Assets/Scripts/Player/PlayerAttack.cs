using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] GameObject playerProjectilePrefab;
	private InputAction attackAction;
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
	}

	void OnEnable()
	{
		attackAction = playerInputActions.Player.Attack;
		attackAction.Enable();
		attackAction.performed += Attack;
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

	void OnDisable()
	{
		attackAction.Disable();
	}
}
