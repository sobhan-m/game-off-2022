using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private PlayerInputActions playerInputActions;
	private Rigidbody2D rb;
	private InputAction movement;

	[SerializeField] float speed = 3f;

	void Awake()
	{
		playerInputActions = new PlayerInputActions();
		rb = GetComponent<Rigidbody2D>();
	}

    void OnEnable()
    {
		movement = playerInputActions.Player.Movement;
		movement.Enable();
	}

    void OnDisable()
	{
		movement.Disable();
	}

	void FixedUpdate()
	{
		Move();
	}

	private void Move()
    {
		Vector2 newVel = movement.ReadValue<Vector2>() * speed;
		rb.velocity = newVel;

		Debug.Log("PlayerController.Move(): " + newVel);
	}
}
